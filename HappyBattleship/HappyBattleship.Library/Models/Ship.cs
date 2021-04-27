using System.Collections.Generic;
using System.Linq;

namespace HappyBattleship.Library
{
    public class Ship
    {
        public int Size { get; private set; }
        public ShipClass Class { get; private set; }
        public ShipDirection Direction { get; private set; }
        public bool Destroyed
        {
            get => Coordinates.Where(p => p.State == PositionState.Hit).Count() == Size;
        }
        public List<Position> Coordinates { get; private set; }
        public int StartX { get; private set; }
        public int StartY { get; private set; }
        public int EndX { get; private set; }
        public int EndY { get; private set; }

        public Ship(ShipClass shipClass, int beginX, int beginY, int endX, int endY)
            : this(shipClass, SpecifyPositions(beginX, beginY, endX, endY))
        {

        }

        public Ship(ShipClass shipClass, List<Position> coordinates)
        {
            Class = shipClass;
            Coordinates = coordinates;
            Size = ShipClassHelpers.InferShipSize(shipClass);

            StartX = Coordinates.First().X;
            StartY = Coordinates.First().Y;
            EndX = Coordinates.Last().X;
            EndY = Coordinates.Last().Y;

            Direction = Coordinates.All(p => p.X == StartX) ? ShipDirection.Vertical : ShipDirection.Horizontal;
        }

        static List<Position> SpecifyPositions(int beginX, int beginY, int endX, int endY)
        {
            var positions = new List<Position>();

            if (beginX == endX)
            {
                for (var y = beginY; y < endY; y++)
                {
                    positions.Add(new Position(beginX, y));
                }
            }
            else if (beginY == endY)
            {
                for (var x = beginX; x < endX; x++)
                {
                    positions.Add(new Position(x, beginY));
                }
            }

            return positions;
        }

    }
}


