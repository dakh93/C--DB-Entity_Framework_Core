using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P01_BillsPaymentSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P01_BillsPaymentSystem.Data.EntityConfig
{
    public class CreditCardConfiguration : IEntityTypeConfiguration<CreditCard>
    {
        public void Configure(EntityTypeBuilder<CreditCard> builder)
        {
            builder.HasKey(e => e.CreditCardId);

            builder.Ignore(e => e.LimitLeft);

            builder.Property(e => e.Limit)
                .IsRequired()
                .HasColumnType("DECIMAL")
                .HasPrecision(15, 2);

            builder.Property(e => e.MoneyOwed)
                .IsRequired()
                .HasColumnType("DECIMAL")
                .HasPrecision(15, 2);

            builder.Property(e => e.ExpirationDate)
                .IsRequired();

        }
    }
}
