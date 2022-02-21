
namespace BookShop.Data
{
    public class Configuration
    {
        private static string path = File.ReadAllLines(@"D:\ConnectionString.txt").First();

        //CHANGE THE NAME OF DATABASE
        private static string databaseName = "BookShop";

        public static string ConnectionString = $@"{String.Format(path, databaseName)}";
    }
}
