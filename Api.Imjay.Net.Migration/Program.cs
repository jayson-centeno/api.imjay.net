using DbUp;
using System;
using System.Reflection;
using System.Linq;
using Microsoft.Extensions.Configuration;

namespace Api.Imjay.Net.Migration
{
    class Program
    {
        static int Main(string[] args)
        {
            IConfiguration config = new ConfigurationBuilder()
                                    .AddJsonFile("appsettings.json", true, true)
                                    .Build();

            var connectionString = config["ConnectionStrings:DefaultConnection"];

            var upgrader =
                DeployChanges.To
                    .SqlDatabase(connectionString)
                    .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
                    .LogToConsole()
                    .Build();

            var result = upgrader.PerformUpgrade();

            if (!result.Successful)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(result.Error);
                Console.ResetColor();
                #if DEBUG
                    Console.ReadLine();
                #endif
                return -1;
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Success!");
            Console.ResetColor();
            return 0;
        }
    }
}
