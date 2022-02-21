

namespace BookShop.Models
{
    public class Category
    {
        public Category()
        {
            this.BooksCategories = new List<BookCategory>();
        }
        public int CategoryId { get; set; }
        public string Name { get; set; }

        public ICollection<BookCategory> BooksCategories { get; set; }
    }
}
