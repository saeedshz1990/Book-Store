using BookStore.Entities;
using BookStore.Persistence.EF.Categories;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Persistence.EF
{
    public class EFDataContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CategoryEntityMap).Assembly);
        }
    }
}
