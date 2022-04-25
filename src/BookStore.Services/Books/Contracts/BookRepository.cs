using BookStore.Entities;

namespace BookStore.Services.Books.Contracts
{
    public interface BookRepository
    {
        void Add (Book book);
        void Update(int id, Book book);
        Book FindById(int id);
        void Delete(int id);
    }
}
