using System.Collections.Generic;
using BookStore.Services.Categories.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.RestAPI.Controllers
{
    [Route("api/categories")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly CategoryService _categoryService;

        public CategoriesController(CategoryService categoryService)
        {
            _categoryService = categoryService;
        }


        [HttpPost]
        public void Add(AddCategoryDto dto)
        {
            _categoryService.Add(dto);
        }

        [HttpGet]
        public IList<GetCategoryDto> GetAll()
        {
            return _categoryService.GetAll();
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _categoryService.Delete(id);
        }
        [HttpPut("{id}")]
        public void Update([FromRoute]int id, [FromBody]UpdateCategoryDto dto)
        {
            _categoryService.Update(dto,id);
        }
    }
}
