namespace _05.Entity_Relations_FootballBettingDatabase.Data.Models
{
    public class Game
    {
        public int GameId { get; set; }
        public DateTime DateTime { get; set; }
        public string Result { get; set; }

        public int AwayTeamId { get; set; }
        public Team AwayTeam { get; set; }
        public decimal AwayTeamBetRate { get; set; }
        public int AwayTeamBetGoals { get; set; }


        public decimal DrawBetRate { get; set; }


        public int HomeTeamId { get; set; }
        public Team HomeTeam { get; set; }
        public decimal HomeTeamBetRate { get; set; }
        public int HomeTeamBetGoals { get; set; }

        public ICollection<Bet> Bets { get; set; }
        public ICollection<PlayerStatistic> PlayersStatistics { get; set; }
    }
}