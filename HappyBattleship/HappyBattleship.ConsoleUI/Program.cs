using Autofac;
using HappyBattleship.Library;
using Microsoft.Extensions.Configuration;
using Serilog;
using System;

namespace HappyBattleship.ConsoleUI
{
    class Program
    {
        private static IContainer Container { get; set; }

        private static ILogger Logger { get; set; }

        static void Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            var builder = new ContainerBuilder();

            builder.RegisterType<Player>().As<IPlayer>();
            builder.RegisterType<RandomShootingStrategy>().As<IShootingStrategy>();
            builder.RegisterType<RandomFleetCreator>().As<IFleetCreator>();
            builder.RegisterType<Board>().As<IBoard>();
            builder.RegisterType<HappyBattleshipSimulation>().As<ISimulation>();

            builder.Register<ILogger>((c, p) =>
            {
                return new LoggerConfiguration()
                  .ReadFrom.Configuration(configuration)
                  .CreateLogger();
            }).SingleInstance();

            Container = builder.Build();

            Logger = Container.Resolve<ILogger>();

            try
            {
                Logger.Information("Happy Battleship Simulation ConsoleUI Starting Up");
                using (var simScope = Container.BeginLifetimeScope())
                {
                    var battleshipSim = simScope.Resolve<ISimulation>();
                    battleshipSim.Init();
                    battleshipSim.Start();
                    while (battleshipSim.IsRunning)
                    {

                    }
                    Logger.Information("Simulation Finished");
                }
            }
            catch (Exception ex)
            {
                Logger.Fatal(ex, "Exception occured during starting Happy Battleship Simulation ConsoleUI");
            }
            finally
            {

            }

        }
    }
}
