
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeamBuilder.Models;

namespace TeamBuilder.Data.Configurations
{
    public class EventConfiguration : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            builder.HasKey(e => e.EventId);

            builder.HasIndex(e => e.EventId)
                .IsUnique();

            builder.Property(e => e.Name)
                .IsRequired()
                .IsUnicode()
                .HasMaxLength(25);

            builder.Property(e => e.Description)
                .IsRequired()
                .IsUnicode()
                .HasMaxLength(250);

            builder.Property(e => e.StartDate)
                .HasColumnType("DATETIME2");

            builder.Property(e => e.EndDate)
               .HasColumnType("DATETIME2");

            builder.HasOne(e => e.Creator)
                .WithMany(c => c.Events)
                .HasForeignKey(e => e.CreatorId);

        }
    }
}
