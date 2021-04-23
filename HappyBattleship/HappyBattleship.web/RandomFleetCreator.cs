using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HappyBattleship.web
{
    public class RandomFleetCreator : IFleetCreator
    {
        private Random _random;

        public RandomFleetCreator()
        {
            _random = new Random();
        }

        public Ship CreateShip(ShipClass shipClass)
        {

            var beginX = _random.Next(10);
            var beginY = _random.Next(10);
            int endX;
            int endY;

            var direction = (ShipDirection)_random.Next(2);

            var shipSize = Helpers.InferShipSize(shipClass);

            if (direction == ShipDirection.Horizontal)
            {
                endX = beginX + shipSize;
                endY = beginY;
            }
            else
            {
                endX = beginX;
                endY = beginY + shipSize;
            }

            NormalizePositions(ref beginX, ref beginY, ref endX, ref endY);

            return new Ship(shipClass, beginX, beginY, endX, endY);
        }

        private void NormalizePositions(ref int beginX, ref int beginY, ref int endX, ref int endY)
        {
            if (endX >= 10)
            {
                var offset = endX - 9;
                endX -= offset;
                beginX -= offset;
            }

            if (endY >= 10)
            {
                var offset = endY - 9;
                endY -= offset;
                beginY -= offset;
            }
        }
    }
}
