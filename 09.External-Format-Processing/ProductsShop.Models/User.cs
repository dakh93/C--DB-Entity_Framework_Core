using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductsShop.Models
{
    public class User
    {
        public User()
        {
            this.BoughtProducts = new List<Product>();
            this.SoldProducts = new List<Product>();
        }

        public int Id { get; set; }
        public int? Age { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public ICollection<Product> BoughtProducts { get; set; }
        public ICollection<Product> SoldProducts { get; set; } 
    }
}
