using BookStore.Entities;
using BookStore.Services.Categories.Contracts;

namespace BookStore.Persistence.EF.Categories
{
    public class EFCategoryRepository :CategoryRepository
    {
        private readonly EFDataContext _context;

        public EFCategoryRepository(EFDataContext context)
        {
            _context = context;
        }

        public void Add(Category category)
        {
            _context.Add(category);
        }
        
    }
}
