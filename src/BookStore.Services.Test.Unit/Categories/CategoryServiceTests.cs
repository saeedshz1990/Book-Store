using System.Collections.Generic;
using BookStore.Entities;
using BookStore.Infrastructure.Application;
using BookStore.Infrastructure.Test;
using BookStore.Persistence.EF;
using BookStore.Persistence.EF.Categories;
using BookStore.Services.Categories;
using BookStore.Services.Categories.Contracts;
using FluentAssertions;
using Xunit;

namespace BookStore.Services.Test.Unit.Categories
{
    public class CategoryServiceTests
    {
        private readonly EFDataContext _context;
        private readonly CategoryRepository _categoryRepository;
        private readonly UnitOfWork _unitOfWork;
        private readonly CategoryService _sut;

        public CategoryServiceTests()
        {
            _context = new EFInMemoryDatabase()
                    .CreateDataContext<EFDataContext>();
            _unitOfWork = new EFUnitOfWork(_context);
            _categoryRepository = new EFCategoryRepository(_context);
            _sut = new CategoryAppService(_categoryRepository, _unitOfWork);
        }

        [Fact]
        public void Add_adds_Category_Properly()
        {
            AddCategoryDto dto = GenerateAddCategoryDto();
            var category = new List<Category>
            {
                new Category{Title = "Test1"},
                new Category{Title = "Test2"},
                new Category{Title = "Test3"}
            };
            _context.Manipulate(_ =>
                _.Categories.AddRange(category));

            _sut.Add(dto);

            _context.Categories.Should()
                .Contain(_ => _.Title == dto.Title);
        }


        [Fact]
        public void GetAll_categories_returns_all()
        {
            var categories = new List<Category>
            {
                new Category { Title = "dummy1"},
                new Category { Title = "dummy2"},
                new Category { Title = "dummy3"}
            };
            _context.Manipulate(_ =>
                _.Categories.AddRange(categories));

            var expected = _sut.GetAll();

            expected.Should().HaveCount(3);
            expected.Should().Contain(_ => _.Title == "dummy1");
            expected.Should().Contain(_ => _.Title == "dummy2");
            expected.Should().Contain(_ => _.Title == "dummy3");

        }


        
        private static AddCategoryDto GenerateAddCategoryDto()
        {
            return new AddCategoryDto
            {
                Title = "dummy"
            };
        }
    }

}

