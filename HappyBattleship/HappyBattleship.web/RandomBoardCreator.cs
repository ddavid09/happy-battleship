using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HappyBattleship.web
{
    public class RandomBoardCreator : IBoardCreator
    {
        private Random _random = new();
        private Board _board = new();

        public Board CreateBoard()
        {
            foreach (ShipClass shipClass in Enum.GetValues(typeof(ShipClass)))
            {
                Ship randomShip;
                do
                {
                    randomShip = GenerateRandomPositionShip(shipClass);

                } while (_board.CanPostShip(randomShip) != true);

                _board.PostShip(randomShip);
            }

            return _board;
        }

        private Ship GenerateRandomPositionShip(ShipClass shipClass)
        {
            Position beginPosition;
            Position endPosition;

            var beginPositionX = _random.Next(1, 11);
            var beginPositionY = _random.Next(1, 11);

            beginPosition = new Position(beginPositionX, beginPositionY);

            var direction = _random.Next(2);

            var shipSize = Utils.InferShipSize(shipClass);

            if (direction == 0)
            {
                endPosition = new Position(beginPositionX + shipSize, beginPositionY);
            }
            else
            {
                endPosition = new Position(beginPositionX, beginPositionY + shipSize);
            }

            NormalizePositions(beginPosition, endPosition);

            var shipPositions = ShipPositions(beginPosition, endPosition);

            return new Ship(shipClass, shipPositions);
        }

        private void NormalizePositions(Position beginPosition, Position endPosition)
        {
            if (endPosition.X > 10)
            {
                var offset = endPosition.X - 10;
                endPosition.X -= offset;
                beginPosition.X -= offset;
            }

            if (endPosition.Y > 10)
            {
                var offset = endPosition.Y - 10;
                endPosition.Y -= offset;
                beginPosition.Y -= offset;
            }
        }

        private List<Position> ShipPositions(Position begin, Position end)
        {
            var result = new List<Position>();

            for (var x = begin.X; x <= end.X; x++)
            {
                for (var y = begin.Y; x < end.Y; x++)
                {
                    result.Add(new Position(x, y));
                }
            }

            return result;
        }
    }
}
