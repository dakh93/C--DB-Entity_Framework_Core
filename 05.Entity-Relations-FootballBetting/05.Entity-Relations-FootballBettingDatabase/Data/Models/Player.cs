
namespace _05.Entity_Relations_FootballBettingDatabase.Data.Models
{
    public class Player
    {
        public int PlayerId { get; set; }
        public string Name { get; set; }
        public bool IsInjured { get; set; }
        public int SquadNumber { get; set; }

        public int TeamId { get; set; }
        public Team Team{ get; set; }

        public int PositionId { get; set; }
        public Position Position { get; set; }

        public ICollection<PlayerStatistic> PlayersStatistics { get; set; }
    }
}
