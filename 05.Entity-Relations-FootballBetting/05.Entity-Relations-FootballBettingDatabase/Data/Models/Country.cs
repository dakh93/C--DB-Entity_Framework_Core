
namespace _05.Entity_Relations_FootballBettingDatabase.Data.Models
{
    public class Country
    {
        public int CountryId { get; set; }
        public string Name { get; set; }

        public ICollection<Town> Towns { get; set; }
    }
}
