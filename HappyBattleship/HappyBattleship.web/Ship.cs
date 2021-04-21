using System.Collections.Generic;
using System.Linq;

namespace HappyBattleship.web
{
    public class Ship
    {
        public int Size { get; set; }
        public ShipClass Class { get; set; }
        public bool Destroyed
        {
            get => Coordinates.Where(p => p.State == PositionState.Hit).Count() == Size;
        }
        public List<Position> Coordinates { get; set; }

        public Ship(ShipClass shipClass, List<Position> coordinates)
        {
            Class = shipClass;
            Coordinates = coordinates;
            Size = Utils.InferShipSize(shipClass);
        }

    }
}


