
using System.Text;
using TeamBuilder.Models.Contracts;

namespace TeamBuilder.Models
{
    public class Event : IEvent
    {
        public int EventId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public int CreatorId { get; set; }
        public User Creator { get; set; }
        public ICollection<EventTeam> Teams { get; set; } = new List<EventTeam>();


    }
}
