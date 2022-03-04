using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ProductsShop.Data;
using ProductsShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductsShop.StartUp.Core
{
    internal class Engine
    {
        private ProductsShopContext context;

        public Engine(ProductsShopContext context)
        {
            this.context = context;
        }
        public void Run()
        {
            //Console.WriteLine(ImportUsersFromJson(context));
            //Console.WriteLine(ImportCategoriesFromJson(context));
            //Console.WriteLine(ImportProductsFromJson(context));
            //SetCategories();
            //Console.WriteLine(GetProductsInRange());
            //GetSuccessfullySoldProducts();
            //GetCategoriesByProductCount();
            //GetUsersAndProducts();

        }

        private void GetUsersAndProducts()
        {
            using (var context = new ProductsShopContext())
            {
                var users = context.Users
                    .Where(u => u.SoldProducts.Count > 0)
                    .OrderByDescending(u => u.SoldProducts.Count)
                    .Select(u => new
                    {
                        u.FirstName,
                        u.LastName,
                        u.Age,
                        Products = u.SoldProducts.Select(sp => new
                        {
                            sp.Name,
                            sp.Price
                        })
                    })
                    .ToList();


                var jsonString = JsonConvert.SerializeObject(
                    users, 
                    Formatting.Indented, 
                    new JsonSerializerSettings
                    {
                        DefaultValueHandling = DefaultValueHandling.Ignore,
                    });

                File.WriteAllText("../../../Files/UsersAndProducts.json", jsonString);
            }
        }
        private void GetCategoriesByProductCount()
        {
            using (var context = new ProductsShopContext())
            {
                var categories = this.context.Categories
                .Include(c => c.CategoryProducts)
                .ThenInclude(cp => cp.Product)
                .Select(c => new
                {
                    CategoryName = c.Name,
                    ProductCount = c.CategoryProducts.Count(),
                    AveragePrice = c.CategoryProducts.Average(p => p.Product.Price),
                    TotalRevenue = c.CategoryProducts.Sum(p => p.Product.Price)
                })
                .OrderByDescending(c => c.CategoryName)
                .ToList();



                var jsonString = JsonConvert.SerializeObject(categories, Formatting.Indented);

                File.WriteAllText("../../../Files/CategoriesByProduct.json", jsonString);

            }
        }
        private void GetSuccessfullySoldProducts()
        {
            using (var context = new ProductsShopContext())
            {
                var users = context.Users
                    .Where(u => u.SoldProducts.Count > 0 &&
                           u.SoldProducts.Any(sp => sp.Buyer != null))
                    .OrderBy(p => p.LastName)
                    .ThenBy(p => p.FirstName)
                    .Select(p => new
                    {
                        p.FirstName,
                        p.LastName,
                        SoldProducts = p.SoldProducts.Select(sp => new
                        {
                            Name = sp.Name,
                            Price = sp.Price,
                            BuyerFirstName = sp.Buyer.FirstName,
                            BuyerLastName = sp.Buyer.LastName,
                        }).ToList()
                    })
                    .ToList();

                var jsonString = JsonConvert.SerializeObject(
                    users,
                    Formatting.Indented,
                    new JsonSerializerSettings
                    {
                        DefaultValueHandling = DefaultValueHandling.Ignore,
                    });

                File.WriteAllText("../../../Files/SuccessfullySoldProducts.json", jsonString);
            }
        }
        private string GetProductsInRange()
        {
            var filename = "../../../Files/PricesInRange.json";
            using (var cotnext = new ProductsShopContext())
            {
                var products = context.Products
                    .Where(p => p.Price >= 500 && p.Price <= 1000)
                    .Select(p => new
                    {
                        Name = p.Name,
                        Price = p.Price,
                        Seller = $"{p.Seller.FirstName} {p.Seller.LastName}"
                    })
                    .OrderBy(p => p.Price)
                    .ToList();

                var jsonString = JsonConvert.SerializeObject(products, Formatting.Indented);

                File.WriteAllText(filename, jsonString);
            }

            return $"Products were save to {filename}";
        }

        private void SetCategories()
        {
            var productIds = context.Products
                .Select(p => p.Id)
                .ToArray();

            var categoryIds = context.Categories
                .Select(c => c.Id)
                .ToArray();

            Random rnd = new Random();

            var categoryProducts = new List<CategoryProduct>();
            foreach (var p in productIds)
            {
                var selectedCategoriesIndexes = new List<int>();
                for (int i = 0; i < 3; i++)
                {

                    int index = rnd.Next(0, categoryIds.Length);

                    while (selectedCategoriesIndexes.Contains(index))
                    {
                        index = rnd.Next(0, categoryIds.Length);
                    }

                    var catProduct = new CategoryProduct()
                    {
                        ProductId = p,
                        CategoryId = categoryIds[index]
                    };

                    categoryProducts.Add(catProduct);
                    selectedCategoriesIndexes.Add(index);
                }
            }
            context.CategoryProducts.AddRange(categoryProducts);
            context.SaveChanges();
        }

        private string ImportProductsFromJson(ProductsShopContext context)
        {
            string path = "../../../Files/products.json";

            Product[] products = ImportJson<Product>(path);

            Random random = new Random();

            var userIds = context.Users
                .Select(u => u.Id)
                .ToArray();


            foreach (var p in products)
            {
                int index = random.Next(0, userIds.Length);
                int sellerId = userIds[index];

                int? buyerId = sellerId;
                while (buyerId == sellerId)
                {
                    int buyerIndex = random.Next(0, userIds.Length);

                    buyerId = userIds[buyerIndex];
                }

                if (buyerId - sellerId < 5 && buyerId - sellerId > 0)
                {
                    buyerId = null;
                }

                p.SellerId = sellerId;
                p.BuyerId = buyerId;
            }

            context.Products.AddRange(products);
            context.SaveChanges();

            return $"{products.Length} products were imported from: {path}";
        }

        private string ImportCategoriesFromJson(ProductsShopContext context)
        {
            string path = "../../../Files/categories.json";

            Category[] categories = ImportJson<Category>(path);


            context.Categories.AddRange(categories);
            context.SaveChanges();

            return $"{categories.Length} categories were imported from: {path}";
        }

        private string ImportUsersFromJson(ProductsShopContext context)
        {
            string path = "../../../Files/users.json";
            User[] users = ImportJson<User>(path);

            context.Users.AddRange(users);
            context.SaveChanges();

            return $"{users.Length} users were imported from: {path}";
        }

        private T[] ImportJson<T>(string path)
        {
            string jsonString = File.ReadAllText(path);

            T[] objects = JsonConvert.DeserializeObject<T[]>(jsonString);

            return objects;
        }
    }
}
