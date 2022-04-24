using System.Collections.Generic;
using BookStore.Entities;
using BookStore.Infrastructure.Application;

namespace BookStore.Services.Categories.Contracts
{
    public interface CategoryRepository :Repository
    {
        void Add(Category category);
        IList<GetCategoryDto> GetAll();
    }
}
