using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HappyBattleship.web
{
    internal class BattleshipHub : Hub
    {
        public override async Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();

            var boardCreator = new RandomBoardCreator();
            var board = boardCreator.CreateBoard();
            var boardPositions = board.GetFlatBoardPositions();
            var serializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
            var message = JsonConvert.SerializeObject(boardPositions, serializerSettings);

            await Clients.Caller.SendAsync("ReloadBoard", message);
        }

        public async Task SendBoard()
        {
            var boardCreator = new RandomBoardCreator();
            var board = boardCreator.CreateBoard();
            var boardPositions = board.GetFlatBoardPositions();
            var message = JsonConvert.SerializeObject(boardPositions);
            await Clients.Caller.SendAsync("ReloadBoard");
        }

        public async Task LoadBoardShips()
        {
            var boardCreator = new RandomBoardCreator();
            var board = boardCreator.CreateBoard();
            var boardShips = board.GetShipsPositions();

            var message = JsonConvert.SerializeObject(board);
            await Clients.Caller.SendAsync("DrawShips", message);
        }

    }
}