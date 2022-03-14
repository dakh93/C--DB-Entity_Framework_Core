
using TeamBuilder.Models.Contracts;

namespace TeamBuilder.Models
{
    public class UserTeam : IUserTeam
    {
        public int UserId { get; set; }
        public User User { get; set; }

        public int TeamId { get; set; }
        public Team Team { get; set; }
    }
}
