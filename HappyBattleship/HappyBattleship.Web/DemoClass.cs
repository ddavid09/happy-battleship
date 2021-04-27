using HappyBattleship.Library;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace HappyBattleship.Web
{
    public static class DemoClass
    {
        public static async void DemoMethodAsync()
        {
            var services = Startup.ServiceProvider;

            var simulation = services.GetService<ISimulation>();

            simulation.Init();
            simulation.Start();

            await Task.Run(() =>
            {
                while (simulation.IsRunning)
                {

                }
            });
        }
    }
}
