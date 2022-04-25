﻿using System.Collections.Generic;
using BookStore.Entities;
using BookStore.Infrastructure.Application;
using BookStore.Services.Categories.Contracts;

namespace BookStore.Services.Categories
{
    public class CategoryAppService : CategoryService
    {
        private readonly CategoryRepository _categoryRepository;
        private readonly UnitOfWork _unitOfWork;

        public CategoryAppService(CategoryRepository categoryRepository, UnitOfWork unitOfWork)
        {
            _categoryRepository = categoryRepository;
            _unitOfWork = unitOfWork;
        }

        public void Add(AddCategoryDto dto)
        {
            var category = new Category
            {
                Title = dto.Title
            };
            _categoryRepository.Add(category);
            _unitOfWork.Commit();
        }

        public IList<GetCategoryDto> GetAll()
        {
            return _categoryRepository.GetAll();
        }

        public void Update(UpdateCategoryDto dto, int id)
        {
            var category = _categoryRepository.FindById(id);
            if (category != null)
            {
                category.Title = dto.Title;

                _unitOfWork.Commit();
            }
        }

        public void Delete(int id)
        {
            _categoryRepository.Delete(id);
            _unitOfWork.Commit();
        }
    }
}
