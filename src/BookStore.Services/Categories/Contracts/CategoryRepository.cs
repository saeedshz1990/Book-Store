using BookStore.Entities;

namespace BookStore.Services.Categories.Contracts
{
    public interface CategoryRepository
    {
        void Add(Category category);
    }
}
