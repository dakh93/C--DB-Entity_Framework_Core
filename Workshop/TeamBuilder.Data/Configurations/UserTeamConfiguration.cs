
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeamBuilder.Models;

namespace TeamBuilder.Data.Configurations
{
    public class UserTeamConfiguration : IEntityTypeConfiguration<UserTeam>
    {
        public void Configure(EntityTypeBuilder<UserTeam> builder)
        {
            builder.HasKey(e => new { e.UserId, e.TeamId });

            builder.HasOne(e => e.User)
                .WithMany(u => u.Teams)
                .HasForeignKey(u => u.UserId)
                .OnDelete(DeleteBehavior.NoAction);
            

            builder.HasOne(e => e.Team)
                .WithMany(t => t.Users)
                .HasForeignKey(e => e.TeamId)
                .OnDelete(DeleteBehavior.NoAction);
            
        }
    }
}
