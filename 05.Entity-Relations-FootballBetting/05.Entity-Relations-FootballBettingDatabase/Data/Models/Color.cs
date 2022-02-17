
namespace _05.Entity_Relations_FootballBettingDatabase.Data.Models
{
    public class Color
    {
        public int ColorId { get; set; }
        public string Name { get; set; }

        public ICollection<Team> PrimaryKitColorTeams { get; set; }
        public ICollection<Team> SecondaryKitColorTeams { get; set; }
    }
}
