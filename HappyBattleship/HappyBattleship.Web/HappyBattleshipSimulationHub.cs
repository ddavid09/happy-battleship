using HappyBattleship.Library;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HappyBattleship.Web
{
    public class HappyBattleshipSimulationHub : Hub
    {
        private ISimulationRepository _repository;

        private IServiceProvider _serviceProvider;

        public HappyBattleshipSimulationHub(ISimulationRepository repository, IServiceProvider serviceProvider)
        {
            _repository = repository;
            _serviceProvider = serviceProvider;
        }

        public override async Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();
        }

        public async Task InitSimulation(string simulationClientId)
        {
            var simulation = _repository.Simulations.FirstOrDefault(s => s.ClientId == simulationClientId);

            if (simulation is null)
            {
                simulation = (HappyBattleshipWebSimulation)_serviceProvider.GetService(typeof(ISimulation));
                simulation.ClientId = simulationClientId;
                _repository.Simulations.Add(simulation);
            }
            await Groups.AddToGroupAsync(Context.ConnectionId, simulationClientId);

            await Task.Run(() => simulation.Init());
        }

        public async Task StartSimulation(string simulationClientId)
        {
            var simulation = _repository.Simulations.FirstOrDefault(s => s.ClientId == simulationClientId);
            await Task.Run(() =>
            {
                simulation.Start();
                while (simulation.IsRunning)
                {

                }
            });
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            return base.OnDisconnectedAsync(exception);
        }
    }
}
