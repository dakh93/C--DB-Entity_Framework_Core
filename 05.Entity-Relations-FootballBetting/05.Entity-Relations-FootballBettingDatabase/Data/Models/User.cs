
namespace _05.Entity_Relations_FootballBettingDatabase.Data.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public decimal Balance { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Username { get; set; }

      
        public ICollection<Bet> Bets { get; set; }

    }
}
