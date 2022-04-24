namespace BookStore.Entities
{
    public class Category
    {
        public Category()
        {
            Books = new Hashset<Book>(0);
        }

        public string Title { get; set; }

        public Hashset<Book> Books { get; set; }
    }
}
