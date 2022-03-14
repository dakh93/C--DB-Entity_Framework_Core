
using System.ComponentModel.DataAnnotations;
using TeamBuilder.Models.Contracts;

namespace TeamBuilder.Models
{
    public class Team : ITeam
    {
        public int TeamId { get; set; }
        
        [MaxLength(25)]
        public string Name { get; set; }

        [MaxLength(32)]
        public string Description { get; set; }

        public string Acronym { get; set; }

        public int CreatorId { get; set; }
        public User Creator { get; set; }

        public bool IsDeleted { get; set; }
        public ICollection<EventTeam> Events { get; set; } = new List<EventTeam>();
        public ICollection<UserTeam> Users { get; set; } = new List<UserTeam>();
        public ICollection<Invitation> Invitations { get; set; } = new List<Invitation>();

    }
}
