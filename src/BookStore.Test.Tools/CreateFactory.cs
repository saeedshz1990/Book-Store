using System;
using BookStore.Entities;
using BookStore.Services.Categories.Contracts;

namespace BookStore.Test.Tools
{
    public static class CreateFactory
    {
        public static Category Create(string title)
        {
            return new Category
            {
                Title = title
            };
        }
    }
}
