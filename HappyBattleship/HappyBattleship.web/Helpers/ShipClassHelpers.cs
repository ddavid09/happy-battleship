using System;

namespace HappyBattleship.web
{
    public static class ShipClassHelpers
    {
        public static int InferShipSize(ShipClass shipClass)
        {
            switch (shipClass)
            {
                case ShipClass.Carrier:
                    return 5;
                case ShipClass.Battleship:
                    return 4;
                case ShipClass.Destroyer:
                case ShipClass.Submarine:
                    return 3;
                case ShipClass.PatrolBoat:
                    return 2;
                default:
                    throw new ArgumentException($"{shipClass} ship class not exist");
            }
        }
    }
}
