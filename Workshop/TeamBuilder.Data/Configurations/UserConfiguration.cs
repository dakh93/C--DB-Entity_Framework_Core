
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeamBuilder.Models;

namespace TeamBuilder.Data.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(e => e.UserId);

            builder.HasIndex(e => e.Username)
                .IsUnique();

            builder.Property(e => e.Username)
                .IsRequired();

            builder.Property(e => e.FirstName)
                .HasMaxLength(25);

            builder.Property(e => e.LastName)
                .HasMaxLength(25);

            builder.Property(e => e.IsLogged)
                .HasDefaultValue(false);

        }
    }
}
