using System.ComponentModel.DataAnnotations;

namespace ProductsShop.Models
{
    public class Category
    {
        public Category()
        {
            this.CategoryProducts = new List<CategoryProduct>();
        }
        public int Id { get; set; }

        [MinLength(3)]
        public string Name { get; set; }

        public ICollection<CategoryProduct> CategoryProducts { get; set; }

    }
}