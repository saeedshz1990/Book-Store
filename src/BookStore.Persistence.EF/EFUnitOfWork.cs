using BookStore.Infrastructure.Application;

namespace BookStore.Persistence.EF
{
    public class EFUnitOfWork :UnitOfWork
    {
        private readonly EFDataContext _context;

        public EFUnitOfWork(EFDataContext context)
        {
            _context = context;
        }

        public void Commit()
        {
            _context.SaveChanges();
        }
    }
}
