
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeamBuilder.Models;

namespace TeamBuilder.Data.Configurations
{
    public class EventTeamConfiguration : IEntityTypeConfiguration<EventTeam>
    {
        public void Configure(EntityTypeBuilder<EventTeam> builder)
        {
            builder.HasKey(e => new { e.EventId, e.TeamId });

            builder.HasOne(e => e.Team)
                .WithMany(t => t.Events)
                .HasForeignKey(e => e.TeamId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(e => e.Event)
               .WithMany(t => t.Teams)
               .HasForeignKey(e => e.EventId)
                .OnDelete(DeleteBehavior.NoAction);

        }
    }
}
