using System;
using System.Collections.Generic;
using System.Linq;
using BookStore.Entities;
using BookStore.Infrastructure.Application;
using BookStore.Infrastructure.Test;
using BookStore.Persistence.EF;
using BookStore.Persistence.EF.Books;
using BookStore.Persistence.EF.Categories;
using BookStore.Services.Books;
using BookStore.Services.Books.Contracts;
using BookStore.Services.Books.Exceptions;
using BookStore.Services.Categories.Contracts;
using BookStore.Test.Tools;
using FluentAssertions;
using Xunit;

namespace BookStore.Services.Test.Unit.Books
{
    public class BookServiceTests
    {
        private readonly EFDataContext _context;
        private readonly BookRepository _bookRepository;
        private readonly UnitOfWork _unitOfWork;
        private readonly BookService _sut;
        private readonly CategoryRepository _categoryRepository;
        
        public BookServiceTests()
        {
            _context = new EFInMemoryDatabase()
                .CreateDataContext<EFDataContext>();
            _unitOfWork = new EFUnitOfWork(_context);
            _bookRepository = new EFBookRepository(_context);
            _categoryRepository = new EFCategoryRepository(_context);
            _sut = new BookAppService(_bookRepository, _unitOfWork, _categoryRepository);
        }

        [Fact]
        public void Add_All_Books_WithCategory_Properly()
        {
            var category = CreateFactory.Create("Dummy");
            _context.Manipulate(_ => _.Categories.Add(category));

            var dto = CreateOneDto(category);

            _sut.Add(dto);
            var expected = _context.Books.FirstOrDefault();
            expected.Title.Should().Be(dto.Title);
            expected.Author.Should().Be(dto.Author);
            expected.Pages.Should().Be(dto.Pages);
            expected.Description.Should().Be(dto.Description);
            expected.Category.Id.Should().Be(dto.CategoryId);
        }

        [Fact]
        public void Add_throws_BookCategoryDoesNotExist_when_book_given_category_id_doesNotExist()
        {
            var category = CreateFactory.Create("Dummy Category");
            AddBookDto dto = CreateOneDto(category);

            Action expected = () => _sut.Add(dto);

            expected.Should().ThrowExactly<BookCategoryNotExistsIndatabaseException>();
        }

        [Fact]
        public void Update_updates_Book_properly()
        {
            var category = CreateFactory.Create("Dummy");
            _context.Manipulate(_ => _.Categories.Add(category));
            var book = CreateOneBook(category);
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
            var book = CreateOneBook(category);
            _context.Manipulate(_ => _.Books.Add(book));
            _sut.Delete(book.Id);
            _context.Books.Should().HaveCount(0);
        }

        [Fact]
        public void Delete_throws_BookNotFound_when_bookDoesNotExist()
        {
           
            Action expected = () => _sut.Delete(12);

            expected.Should().ThrowExactly<BookNotFoundException>();
        }

        [Fact]
        public void GetAll_books_WithCategory()
        {
            var category = CreateFactory.Create("Dummy");
            _context.Manipulate(_ => _.Categories.Add(category));
            var book = CreateListBook(category);
            _context.Manipulate(_ => _.Books.AddRange(book));
            var expected = _sut.GetAll();
            expected.Should().HaveCount(2);
            expected.Should().Contain(_ => _.Title == "CleanCode");
            expected.Should().Contain(_ => _.Title == "CleanArchitecture");
        }

        private static List<Book> CreateListBook(Category category)
        {
            var book = new List<Entities.Book>
            {
                new Entities.Book
                {
                    Title = "CleanCode",
                    Author = "Robert C. Martin",
                    Pages = 465,
                    Description = "Clean Code is a handbook of guidelines for writing good software",
                    CategoryId = category.Id
                },
                new Entities.Book
                {
                    Title = "CleanArchitecture",
                    Author = "Saeed Ansari",
                    Pages = 465,
                    Description = "Clean Architecture is a handbook of guidelines for writing good software",
                    CategoryId = category.Id
                }
            };
            return book;
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

        private static AddBookDto CreateOneDto(Category category)
        {
            var dto = new AddBookDto
            {
                Title = "Dummy",
                Description = "Dummy",
                Author = "Dummy",
                Pages = 465,
                CategoryId = category.Id,
            };
            return dto;
        }

        private static Book CreateOneBook(Category category)
        {
            var book = new Entities.Book
            {
                Author = "Dummy",
                Description = "Dummy",
                Pages = 465,
                Title = "Dummy",
                CategoryId = category.Id
            };
            return book;
        }
    }
}
