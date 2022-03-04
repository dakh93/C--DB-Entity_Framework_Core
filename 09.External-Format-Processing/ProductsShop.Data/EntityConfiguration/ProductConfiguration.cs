
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductsShop.Models;

namespace ProductsShop.Data.EntityConfiguration
{
    internal class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Name)
                .IsRequired()
                .IsUnicode();

            builder.Property(e => e.Price)
                .IsRequired()
                .HasColumnType("DECIMAL")
                .HasPrecision(15, 2);

            builder.Property(e => e.BuyerId)
                .IsRequired(false);

            builder.Property(e => e.SellerId)
                .IsRequired();

            builder.HasOne(e => e.Buyer)
                .WithMany(b => b.BoughtProducts)
                .HasForeignKey(e => e.BuyerId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(e => e.Seller)
                .WithMany(s => s.SoldProducts)
                .HasForeignKey(e => e.SellerId)
                .OnDelete(DeleteBehavior.NoAction);

        }
    }
}
