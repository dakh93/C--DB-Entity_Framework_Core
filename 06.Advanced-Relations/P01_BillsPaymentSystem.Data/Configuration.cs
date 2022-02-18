namespace P01_BillsPaymentSystem.Data
{
    public class Configuration
    {
        private static string path = File.ReadAllLines(@"D:\ConnectionString.txt").First();
        private static string databaseName = "BillsPaymentSystem";

        public static string ConnectionString = $@"{String.Format(path,databaseName)}";
    }
}