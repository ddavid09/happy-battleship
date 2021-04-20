using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HappyBattleship.web
{
    internal class BattleshipHub : Hub
    {
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