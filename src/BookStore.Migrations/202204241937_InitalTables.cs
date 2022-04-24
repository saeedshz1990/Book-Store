using FluentMigrator;

namespace BookStore.Migrations
{
    [Migration(202204241937)]
    public class _202204241937_InitalTables : Migration
    {
        public override void Up()
        {
            CreateCategory();
            CreateBooks();
        }

        public override void Down()
        {
            Delete.Table("Books");
            Delete.Table("Categories");
        }

        private void CreateBooks()
        {
            Create.Table("Books")
                            .WithColumn("Id").AsInt32().PrimaryKey().NotNullable().Identity()
                            .WithColumn("Title").AsString(50).NotNullable()
                            .WithColumn("Pages").AsInt32().NotNullable()
                            .WithColumn("Description").AsString(500).Nullable()
                            .WithColumn("Author").AsString(100).NotNullable()
                            .WithColumn("CategoryId").AsInt32().NotNullable()
                            .ForeignKey("FK_Books_Categories", "Categories", "Id")
                            .OnDelete(System.Data.Rule.None);
        }

        private void CreateCategory()
        {
            Create.Table("Categories")
                            .WithColumn("Id").AsInt32().PrimaryKey().NotNullable().Identity()
                            .WithColumn("Title").AsString(50).NotNullable();
        }

    }
}
