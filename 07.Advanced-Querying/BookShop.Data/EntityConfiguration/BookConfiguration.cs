﻿using BookShop.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookShop.Data.EntityConfiguration
{
    public class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.HasKey(e => e.BookId);

            builder.Property(e => e.Title)
                .IsRequired(false)
                .IsUnicode()
                .HasMaxLength(50);

            builder.Property(e => e.Description)
               .IsRequired(false)
               .IsUnicode()
               .HasMaxLength(1000);

            builder.Property(e => e.ReleaseDate)
               .IsRequired(false);

            builder.Property(e => e.Price)
               .HasColumnType("DECIMAL")
               .HasPrecision(15, 2);

            builder.HasOne(e => e.Author)
                .WithMany(a => a.Books)
                .HasForeignKey(e => e.AuthorId);
        }
    }
}
