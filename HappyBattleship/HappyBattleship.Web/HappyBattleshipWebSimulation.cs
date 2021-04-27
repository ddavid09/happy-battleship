using HappyBattleship.Library;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;

namespace HappyBattleship.Web
{
    public class HappyBattleshipWebSimulation : HappyBattleshipSimulation
    {
        public string ClientId { get; set; }

        private IHubContext<HappyBattleshipSimulationHub> _hubContext;

        public HappyBattleshipWebSimulation(IPlayer leftPlayer, IPlayer rightPlayer, IHubContext<HappyBattleshipSimulationHub> hubContext) : base(leftPlayer, rightPlayer)
        {
            _hubContext = hubContext;

            Initialised += async (s, e) =>
            {
                var leftBoardJson = JsonConvert.SerializeObject(e.LeftBoardToDraw);
                var rightBoardJson = JsonConvert.SerializeObject(e.RightBoardToDraw);
                var sideBegining = e.PlayerBegining;

                await _hubContext.Clients.Group(ClientId).SendAsync("SimulationInitialised", leftBoardJson, rightBoardJson, sideBegining);
            };

            AfterTurn += async (s, e) =>
            {
                var positionToUpdateJson = JsonConvert.SerializeObject(e.PositionToUpdate);
                var sideToUpdate = e.UpdateAtPlayer;

                await _hubContext.Clients.Group(ClientId).SendAsync("HandleNewTurn", positionToUpdateJson, sideToUpdate);
            };

            Finished += async (s, e) =>
            {
                await _hubContext.Clients.Group(ClientId).SendAsync("FinishSimulation");
            };
        }
    }
}
