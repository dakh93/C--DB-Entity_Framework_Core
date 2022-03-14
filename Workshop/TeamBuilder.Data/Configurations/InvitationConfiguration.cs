
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeamBuilder.Models;

namespace TeamBuilder.Data.Configurations
{
    public class InvitationConfiguration : IEntityTypeConfiguration<Invitation>
    {
        public void Configure(EntityTypeBuilder<Invitation> builder)
        {
            builder.HasKey(e => e.InvitationId);

            builder.HasIndex(e => e.InvitationId)
                .IsUnique();

            builder.HasOne(e => e.InvitedUser)
                .WithMany(u => u.Invitations)
                .HasForeignKey(e => e.InvitedUserId)
                .OnDelete(DeleteBehavior.NoAction);
            

            builder.HasOne(e => e.Team)
                .WithMany(t => t.Invitations)
                .HasForeignKey(e => e.TeamId)
                .OnDelete(DeleteBehavior.NoAction);
            
        }
    }
}
