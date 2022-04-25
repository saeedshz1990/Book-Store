using System;
using System.Linq;
using BookStore.Infrastructure.Application;
using BookStore.Infrastructure.Test;
using BookStore.Persistence.EF;
using BookStore.Persistence.EF.Books;
using BookStore.Services.Books;
using BookStore.Services.Books.Contracts;
using FluentAssertions;
using BookStore.Services.Books.Exceptions;
using BookStore.Test.Tools;
using Xunit;

namespace BookStore.Services.Test.Unit.Book
{
    public class BookServiceTests
    {
        private readonly EFDataContext _context;
        private readonly BookRepository _bookRepository;
        private readonly UnitOfWork _unitOfWork;
        private readonly BookService _sut;


        public BookServiceTests()
        {
            _context = new EFInMemoryDatabase()
                .CreateDataContext<EFDataContext>();
            _unitOfWork = new EFUnitOfWork(_context);
            _bookRepository = new EFBookRepository(_context);
            _sut = new BookAppService(_bookRepository, _unitOfWork);
        }

        [Fact]
        public void Add_All_Books_WithCategory_Properly()
        {
            var category = CreateFactory.Create("Dummy");
            _context.Manipulate(_ => _.Categories.Add(category));

            var dto = new AddBookDto
            {
                Title = "Dummy",
                Description = "Dummy",
                Author = "Dummy",
                Pages = 465,
                CategoryId = category.Id,
            };

            _sut.Add(dto);
            var expected = _context.Books.FirstOrDefault();
            expected.Title.Should().Be(dto.Title);
            expected.Author.Should().Be(dto.Author);
            expected.Pages.Should().Be(dto.Pages);
            expected.Description.Should().Be(dto.Description);
            expected.Category.Id.Should().Be(dto.CategoryId);
        }

        [Fact]
        public void Update_updates_Book_properly()
        {
            var category = CreateFactory.Create("Dummy");
            _context.Manipulate(_ => _.Categories.Add(category));
            var book = new Entities.Book
            {
                Author = "EditedDummy",
                Description = "EditedDummy",
                Pages = 465,
                Title = "EditedDummy",
                CategoryId = category.Id
            };
            UpdateBookDto dto = GenerateUpdateBookDto();

            _context.Manipulate(_ => _.Books.Add(book));
            _sut.Update(dto, book.Id);
            
            var expected = _context.Books.FirstOrDefault();
            expected.Title.Should().Be(dto.Title);
            expected.Author.Should().Be(dto.Author);
            expected.Pages.Should().Be(dto.Pages);
            expected.Description.Should().Be(dto.Description);
            expected.Category.Id.Should().Be(dto.CategoryId);
        }
        
        [Fact]
        public void ThrowUpdates_Exception_whenId_IsNotFound()
        {
            var testId = 15420;
            var category = CreateFactory.Create("Dummy");
            _context.Manipulate(_ => _.Categories.Add(category));

            var dto = GenerateUpdateBookDto();

            Action expected = () => _sut.Update(dto, testId);

            expected.Should().ThrowExactly<BookNotFoundException>();
        }

        [Fact]
        public void Delete_Book_Properly()
        {
            var category = CreateFactory.Create("Dummy");
            _context.Manipulate(_ => _.Categories.Add(category));
            var book = new Entities.Book
            {
                Author = "Dummy",
                Description = "Dummy",
                Pages = 465,
                Title = "Dummy",
                CategoryId = category.Id
            };
            _context.Manipulate(_ => _.Books.Add(book));
            _sut.Delete(book.Id);
            _context.Books.Should().HaveCount(0);
        }


        private static UpdateBookDto GenerateUpdateBookDto()
        {
            return new UpdateBookDto
            {
                Title = "Dummy",
                Description = "Dummy",
                Author = "Dummy",
                Pages = 1000,
                CategoryId = 1
            };
        }
    }
}
