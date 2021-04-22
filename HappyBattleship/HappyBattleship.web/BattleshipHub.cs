using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Threading.Tasks;

namespace HappyBattleship.web
{
    internal class BattleshipHub : Hub
    {
        private Simulation _battleShipSimulation;

        public override async Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();
        }

        public async Task StartSimulation()
        {
            do
            {
                _battleShipSimulation = new Simulation();
                _battleShipSimulation.NewTurn += async (s, e) =>
                {
                    var settings = new JsonSerializerSettings
                    {
                        ContractResolver = new CamelCasePropertyNamesContractResolver()
                    };
                    var bothBoardsJson = JsonConvert.SerializeObject(e.FlatBoardsPositions, settings);
                    var leftBoardJson = JsonConvert.SerializeObject(e.FlatLeftBoardPosition, settings);
                    var rightBoardJson = JsonConvert.SerializeObject(e.FlatRightBoardPosition, settings);
                    await Clients.Caller.SendAsync("updateBoardsState", leftBoardJson, rightBoardJson, bothBoardsJson);
                    Console.WriteLine("Sent board state");
                };
                _battleShipSimulation.Init();
                _battleShipSimulation.Start();
                await Task.Run(() => { while (_battleShipSimulation.IsRunning) ; });

            } while (true);
        }

    }
}