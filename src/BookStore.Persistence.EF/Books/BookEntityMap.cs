using BookStore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookStore.Persistence.EF.Books
{
    public class BookEntityMap : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.ToTable("Books");

            builder.HasKey(_ => _.Id);
            builder.Property(_ => _.Id).ValueGeneratedOnAdd();

            builder.Property(_ => _.Title)
                .HasMaxLength(50)
                .IsRequired();
            
            builder.Property(_ => _.Author)
                .HasMaxLength(100)
                .IsRequired();
            
            builder.Property(_ => _.Description)
                .HasMaxLength(500)
                .IsRequired();
            
            builder.Property(_ => _.Pages)
                .IsRequired();
        }
    }
    
}
