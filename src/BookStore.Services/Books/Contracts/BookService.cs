using System.Collections.Generic;
using BookStore.Entities;

namespace BookStore.Services.Books.Contracts
{
    public interface BookService
    {
        void Add(AddBookDto dto);
        void Update(UpdateBookDto dto,int id);
        void Delete(int id);
        IList<GetBookDto> GetAll();
    }
}
