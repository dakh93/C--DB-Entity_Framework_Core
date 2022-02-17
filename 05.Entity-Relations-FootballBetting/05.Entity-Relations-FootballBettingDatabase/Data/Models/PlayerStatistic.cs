
namespace _05.Entity_Relations_FootballBettingDatabase.Data.Models
{
    public class PlayerStatistic
    {
        public int PlayerId { get; set; }
        public Player Player { get; set; }

        public int GameId { get; set; }
        public Game Game { get; set; }

        public int MinutesPlayed { get; set; }
        public int ScoredGoals { get; set; }
    }
}
