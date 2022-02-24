using System;
using System.IO;
using System.Linq;

namespace PhotoShare.Data
{
    internal class ServerConfig
    {
        private static string path = File.ReadAllLines(@"D:\ConnectionString.txt").First();

        //CHANGE THE NAME OF DATABASE
        private static string databaseName = "PhotoShare";

        public static string ConnectionString = $@"{String.Format(path, databaseName)}";
    }
}
