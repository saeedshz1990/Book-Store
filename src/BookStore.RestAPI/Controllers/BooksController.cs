using System.Collections.Generic;
using BookStore.Services.Books.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.RestAPI.Controllers
{
    [Route("api/books")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly BookService _bookService;

        public BooksController(BookService bookService)
        {
            _bookService = bookService;
        }

        [HttpPost]
        public void Add(AddBookDto dto)
        {
            _bookService.Add(dto);
        }

        [HttpGet]
        public IList<GetBookDto> GetAll()
        {
            return _bookService.GetAll();
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _bookService.Delete(id);
        }

        [HttpPut("{id}")]
        public void Update([FromRoute] int id, [FromBody] UpdateBookDto dto)
        {
            _bookService.Update(dto, id);

        }
    }
}
