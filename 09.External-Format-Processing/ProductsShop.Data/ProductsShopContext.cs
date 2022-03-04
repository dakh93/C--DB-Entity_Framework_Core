using Microsoft.EntityFrameworkCore;
using ProductsShop.Data.EntityConfiguration;
using ProductsShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductsShop.Data
{
    public class ProductsShopContext : DbContext
    {
        public ProductsShopContext() { }

        public ProductsShopContext(DbContextOptions options)
            : base(options) 
        {
            //Database.EnsureDeleted();
            //Database.EnsureCreated();
        }


        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<CategoryProduct> CategoryProducts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Configuration.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration<User>(new UserConfiguration());
            modelBuilder.ApplyConfiguration<Category>(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration<Product>(new ProductConfiguration());
            modelBuilder.ApplyConfiguration<CategoryProduct>(new CategoryProductConfiguration());
        }
    }
}
