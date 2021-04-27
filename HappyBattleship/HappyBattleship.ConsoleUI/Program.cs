using Microsoft.Extensions.Configuration;
using Serilog;
using System;

namespace HappyBattleship.ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .CreateLogger();

            try
            {
                Log.Information("Happy Battleship Simulation ConsoleUI Starting Up");
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Exception occured during starting Happy Battleship Simulation ConsoleUI");
            }
            finally
            {
                Log.CloseAndFlush();
            }

            Console.WriteLine("Hello World!");
        }
    }
}
