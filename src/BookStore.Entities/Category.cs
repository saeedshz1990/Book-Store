using System.Collections.Generic;

namespace BookStore.Entities
{
    public class Category :EntityBase
    {
        public Category()
        {
            Books = new HashSet<Book>();
        }

        public string Title { get; set; }

        public HashSet<Book> Books { get; set; }
    }
}
