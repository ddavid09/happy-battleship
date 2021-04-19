using System;
using System.Collections.Generic;
using System.Linq;

namespace HappyBattleship.web
{
    public class Board
    {
        private List<Ship> _ships;
        private Position[,] _positions;

        public Board()
        {
            _positions = new Position[10, 10];

            for (var x = 0; x < 10; x++)
            {
                for (var y = 0; y < 10; y++)
                {
                    _positions[x, y] = new Position(x, y);
                }
            }

            _ships = new List<Ship>();
        }

        public void PostShip(Ship ship)
        {
            for (var i = 0; i < ship.Coordinates.Count; i++)
            {
                var shipPosition = ship.Coordinates[i];
                var positionToCover = _positions[shipPosition.X, shipPosition.Y];

                if (positionToCover.State != PositionState.Initial)
                {
                    throw new InvalidOperationException($"Position {positionToCover.X}, {positionToCover.Y} is already covered, check if you can post ship before invoke PostShip method");
                }

                positionToCover.State = PositionState.Covered;
                ship.Coordinates[i] = positionToCover;
            }
            _ships.Add(ship);
        }

        public bool CanPostShip(Ship ship)
        {
            foreach (var position in ship.Coordinates)
            {
                var positionToCover = _positions[position.X, position.Y];
                if (positionToCover.State != PositionState.Initial)
                {
                    return false;
                }
            }

            return true;
        }


        public PositionState HandleShoot(Shoot shoot)
        {
            var targetState = _positions[shoot.targetX, shoot.targetY].State;

            if (targetState == PositionState.Covered)
            {
                targetState = PositionState.Hit;
            }
            else if (targetState == PositionState.Initial)
            {
                targetState = PositionState.Missed;
            }
            else
            {
                throw new InvalidOperationException($"Position {shoot.targetX}, {shoot.targetY} was handled before");
            }

            return targetState;
        }
    }
}
