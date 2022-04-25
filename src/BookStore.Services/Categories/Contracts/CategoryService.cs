using System.Collections.Generic;
using BookStore.Infrastructure.Application;

namespace BookStore.Services.Categories.Contracts
{
    public interface CategoryService :Service
    {
        void Add(AddCategoryDto dto);
        IList<GetCategoryDto> GetAll();
        void Update(UpdateCategoryDto dto,int id);
        void Delete(int id);
    }
}
