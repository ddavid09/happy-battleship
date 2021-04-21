using System;

namespace HappyBattleship.web
{
    public class TurnEventArgs : EventArgs
    {
        public Position[] FlatBoardsPositions;

        public Position[] FlatLeftBoardPosition;

        public Position[] FlatRightBoardPosition;
    }
}