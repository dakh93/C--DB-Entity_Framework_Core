using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P01_HospitalDatabase.Data
{
    public class Configuration
    {
        private static string path = File.ReadAllLines(@"D:\ConnectionString.txt").First();

        public static string ConnectionString = $@"{path}";

    }
}
