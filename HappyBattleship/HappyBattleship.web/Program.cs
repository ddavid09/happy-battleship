using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System;

namespace HappyBattleship.web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var simulation = new Simulation();
            simulation.Init();
            simulation.Start();

            Console.ReadKey();

            //CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
