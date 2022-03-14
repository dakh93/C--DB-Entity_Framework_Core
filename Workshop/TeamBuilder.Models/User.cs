using System.ComponentModel.DataAnnotations;
using TeamBuilder.Models.Contracts;
using TeamBuilder.Models.Enums;

namespace TeamBuilder.Models
{
    public class User : IUser
    {

        
        public int UserId { get; set; }

        [MinLength(3)]
        [MaxLength(25)]
        public string Username { get; set; }

        [MinLength(6)]
        [MaxLength(30)]
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public Gender Gender { get; set; }
        public bool IsDeleted { get; set; }

        public bool IsLogged { get; set; }

        public ICollection<Team> CreatedTeams { get; set; } = new List<Team>();
        public ICollection<Event> Events { get; set; } = new List<Event>();
        public ICollection<UserTeam> Teams { get; set; } = new List<UserTeam>();
        public ICollection<Invitation> Invitations { get; set; } = new List<Invitation>();

    }
}