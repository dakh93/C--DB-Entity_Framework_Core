
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P01_BillsPaymentSystem.Data.Models;

namespace P01_BillsPaymentSystem.Data.EntityConfig
{
    public class PaymentMethodConfiguration : IEntityTypeConfiguration<PaymentMethod>
    {
        public void Configure(EntityTypeBuilder<PaymentMethod> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Type)
                .IsRequired();

            builder.Property(e => e.UserId)
                .IsRequired();

            builder.Property(e => e.BankAccountId)
                .IsRequired(false);

            builder.Property(e => e.CreditCardId)
                .IsRequired(false);

            builder.HasOne(e => e.User)
                .WithMany(u => u.PaymentMethods)
                .HasForeignKey(u => u.UserId);

            builder.HasOne(e => e.BankAccount)
                .WithOne(b => b.PaymentMethod)
                .HasForeignKey<PaymentMethod>(e => e.BankAccountId);

            builder.HasOne(e => e.CreditCard)
                .WithOne(b => b.PaymentMethod)
                .HasForeignKey<PaymentMethod>(e => e.CreditCardId);

            builder.HasIndex(e => new
            {
                e.UserId,
                e.BankAccountId,
                e.CreditCardId
            })
                .IsUnique();
        }
    }
}
