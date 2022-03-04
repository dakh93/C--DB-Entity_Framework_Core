
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductsShop.Models;

namespace ProductsShop.Data.EntityConfiguration
{
    internal class CategoryProductConfiguration : IEntityTypeConfiguration<CategoryProduct>
    {
        public void Configure(EntityTypeBuilder<CategoryProduct> builder)
        {
            builder.HasKey(e => new {e.CategoryId, e.ProductId});

            builder.HasOne(e => e.Category)
                .WithMany(c => c.CategoryProducts)
                .HasForeignKey(e => e.CategoryId);

            builder.HasOne(e => e.Product)
                .WithMany(p => p.CategoryProducts)
                .HasForeignKey(e => e.ProductId);
        }
    }
}
