using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Threading.Tasks;

namespace HappyBattleship.Library
{
    internal class BattleshipHub : Hub
    {
        private JsonSerializerSettings _serilizeSettings;

        private ISimulation _battleShipSimulation;

        public BattleshipHub(ISimulation battleShipSimulation)
        {
            _battleShipSimulation = battleShipSimulation;

            _serilizeSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

            _battleShipSimulation.Initialised += async (s, e) =>
            {
                var leftBoardjson = JsonConvert.SerializeObject(e.LeftBoardToDraw, _serilizeSettings);
                var rightBoardjson = JsonConvert.SerializeObject(e.RightBoardToDraw, _serilizeSettings);
                await Clients.Caller.SendAsync("InitBoards", leftBoardjson, rightBoardjson);
            };

            _battleShipSimulation.AfterTurn += async (s, e) =>
            {
                var turnChangePositionjson = JsonConvert.SerializeObject(e.PositionToUpdate, _serilizeSettings);
                var boardToUpdateSide = e.UpdateAtPlayer;
                await Clients.Caller.SendAsync("HandleNewTurn", boardToUpdateSide, turnChangePositionjson);
            };
        }

        public override async Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();

            _battleShipSimulation.Init();
        }

        public void StartSimulation()
        {
            _battleShipSimulation.Start();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            _battleShipSimulation.Stop();
            await base.OnDisconnectedAsync(exception);
            Dispose();
        }
    }
}