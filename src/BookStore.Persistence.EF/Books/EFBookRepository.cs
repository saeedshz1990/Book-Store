using BookStore.Entities;
using BookStore.Services.Books.Contracts;

namespace BookStore.Persistence.EF.Books
{
    public class EFBookRepository : BookRepository
    {
        private readonly EFDataContext _context;

        public EFBookRepository(EFDataContext context)
        {
            _context = context;
        }

        public void Add(Book book)
        {
            _context.Books.Add(book);
        }

        public void Update(int id, Book book)
        {

        }

        public Book FindById(int id)
        {
            return _context.Books.Find(id);
        }

        public void Delete(int id)
        {
            var book = _context.Books.Find(id);
            _context.Remove(book);
        }
    }
}
