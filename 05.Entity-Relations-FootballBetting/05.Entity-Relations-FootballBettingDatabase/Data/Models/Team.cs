
namespace _05.Entity_Relations_FootballBettingDatabase.Data.Models
{
    public class Team
    {
        public int TeamId { get; set; }
        public string Name { get; set; }
        public decimal Budget { get; set; }
        public string Initials { get; set; }
        public string LogoUrl { get; set; }

        public int TownId { get; set; }
        public Town Town { get; set; }

        public int PrimaryKitColorId { get; set; }
        public Color PrimaryKitColor { get; set; }

        public int SecondaryKitColorId { get; set; }
        public Color SecondaryKitColor { get; set; }

        public ICollection<Game> HomeGames { get; set; }
        public ICollection<Game> AwayGames { get; set; }
        public ICollection<Player> Players { get; set; }
    }
}
