using System;

namespace HappyBattleship.Library
{
    public class TurnEventArgs : EventArgs
    {
        public string UpdateAtPlayer { get; set; }
        public Position PositionToUpdate { get; set; }

    }
}