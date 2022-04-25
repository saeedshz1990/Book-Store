using System.Collections.Generic;
using BookStore.Entities;
using BookStore.Infrastructure.Application;
using BookStore.Services.Books.Contracts;
using BookStore.Services.Books.Exceptions;

namespace BookStore.Services.Books
{
    public class BookAppService : BookService
    {
        private readonly BookRepository _bookRepository;
        private readonly UnitOfWork _unitOfWork;

        public BookAppService(BookRepository bookRepository, UnitOfWork unitOfWork)
        {
            _bookRepository = bookRepository;
            _unitOfWork = unitOfWork;
        }

        public void Add(AddBookDto dto)
        {
            var book = new Book
            {
                Title = dto.Title,
                Author = dto.Author,
                Pages = dto.Pages,
                Description = dto.Description,
                CategoryId = dto.CategoryId
            };
            _bookRepository.Add(book);
            _unitOfWork.Commit();

        }

        public void Update(UpdateBookDto dto, int id)
        {
            var book = _bookRepository.FindById(id);
            if (book != null)
            {
                book.Title = dto.Title;
                book.Author = dto.Author;
                book.Pages = dto.Pages;
                book.Description = dto.Description;

                _unitOfWork.Commit();
            }
            else
            {
                throw new BookNotFoundException();
            }
        }

        public void Delete(int id)
        {
            _bookRepository.Delete(id);
            _unitOfWork.Commit();
        }

        public IList<Book> GetAll()
        {
            return _bookRepository.GetAll();
        }
    }
}
