using System.Collections.Generic;
using BookStore.Entities;
using BookStore.Infrastructure.Application;

namespace BookStore.Services.Categories.Contracts
{
    public interface CategoryRepository :Repository
    {
        void Add(Category category);
        IList<GetCategoryDto> GetAll();
        void Update(Category category,int id);
        GetCategoryDto FindById(int id);
        void Delete(int id);

    }
}
