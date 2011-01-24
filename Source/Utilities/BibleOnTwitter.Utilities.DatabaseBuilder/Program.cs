using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BibleOnTwitter.Infrastructure.Configuration;
using BibleOnTwitter.Infrastructure.DataAccess;

namespace BibleOnTwitter.Utilities.DatabaseBuilder
{
    public class Program
    {
        static void Main(string[] args)
        {
            var Config = NHibernateConfiguration.Create();
            Console.WriteLine("Got Configuration");
            Config.CreateAndDropDatabase();
            Console.WriteLine("Generated Schema");
            Console.Read();
        }
    }
}
