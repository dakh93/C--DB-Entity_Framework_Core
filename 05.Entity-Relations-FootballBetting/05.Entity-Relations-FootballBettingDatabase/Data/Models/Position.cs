
namespace _05.Entity_Relations_FootballBettingDatabase.Data.Models
{
    public  class Position
    {
        public int PositionId { get; set; }
        public string Name { get; set; }

        public ICollection<Player> Players { get; set; }
    }
}
