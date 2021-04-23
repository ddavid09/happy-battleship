using System;

namespace HappyBattleship.web
{
    public class TurnEventArgs : EventArgs
    {
        public string UpdateAtPlayer { get; set; }
        public Position PositionToUpdate { get; set; }

    }
}