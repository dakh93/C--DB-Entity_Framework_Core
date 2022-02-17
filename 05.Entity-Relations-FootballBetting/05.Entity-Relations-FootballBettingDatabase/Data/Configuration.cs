

namespace _05.Entity_Relations_FootballBettingDatabase.Data
{
    public class Configuration
    {
        private static string path = File.ReadAllLines(@"D:\ConnectionString.txt").First();

        public static string ConnectionString = $@"{path}";
        


    }
}
