using System.Collections.Generic;
using BookStore.Entities;
using BookStore.Infrastructure.Application;
using BookStore.Services.Books.Contracts;
using BookStore.Services.Books.Exceptions;
using BookStore.Services.Categories.Contracts;

namespace BookStore.Services.Books
{
    public class BookAppService : BookService
    {
        private readonly BookRepository _bookRepository;
        private readonly UnitOfWork _unitOfWork;
        private readonly CategoryRepository _categoryRepository;

        public BookAppService(BookRepository bookRepository,
            UnitOfWork unitOfWork, CategoryRepository categoryRepository)
        {
            _bookRepository = bookRepository;
            _unitOfWork = unitOfWork;
            _categoryRepository = categoryRepository;
        }

        public void Add(AddBookDto dto)
        {
            var category = _categoryRepository.FindById(dto.CategoryId);
            if (category != null)
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
            else
            {
                throw new BookCategoryNotExistsIndatabaseException();
            }
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
            if (_bookRepository.FindById(id) != null)
            {
                _bookRepository.Delete(id);
                _unitOfWork.Commit();
            }
            else
            {
                throw new BookNotFoundException();
            }

        }

        public IList<GetBookDto> GetAll()
        {
            return _bookRepository.GetAll();
        }
    }
}
