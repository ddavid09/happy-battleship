using System;
using System.Collections.Generic;
using System.Linq;

namespace HappyBattleship.web
{
    public class Board : IBoard
    {
        private readonly Position[,] _positions;
        private List<Ship> _ships;

        public bool AllShipsDestroyed { get => _ships.Where(ship => ship.Destroyed == true).Count() == Enum.GetNames(typeof(ShipClass)).Length; }

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

        public Position[] GetBoardPositionsFlatted()
        {
            var size = _positions.Length;
            Position[] result = new Position[size];

            int index = 0;
            for (var x = 0; x <= _positions.GetUpperBound(0); x++)
            {
                for (var y = 0; y <= _positions.GetUpperBound(1); y++)
                {
                    result[index++] = _positions[x, y];
                }
            }

            return result;
        }

        public void PostShip(Ship ship)
        {
            for (var i = 0; i < ship.Coordinates.Count; i++)
            {
                var shipPosition = ship.Coordinates[i];
                var positionToCover = _positions[shipPosition.X, shipPosition.Y];

                positionToCover.State = PositionState.Covered;
                ship.Coordinates[i] = positionToCover;
            }
            _ships.Add(ship);
        }

        public bool CanPostShip(Ship ship)
        {
            var neighbours = ShipNeighbourPositions(ship);

            foreach (var position in ship.Coordinates)
            {
                var positionToCover = _positions[position.X, position.Y];

                if (positionToCover.State == PositionState.Covered ||
                    neighbours.Any(p => p.State == PositionState.Covered))
                {
                    return false;
                }
            }

            return true;
        }

        public ShootResult ShootResult(Shoot shoot)
        {
            var prevState = _positions[shoot.X, shoot.Y].State;

            if (prevState == PositionState.Initial)
            {
                return web.ShootResult.Missed;
            }
            else if (prevState == PositionState.Covered)
            {
                var shipOnPosition = GetShipOnPosition(_positions[shoot.X, shoot.Y]);

                if (shipOnPosition.Coordinates.Where(p => p.State == PositionState.Covered).Count() == 1)
                {
                    return web.ShootResult.HitDestroyed;
                }

                return web.ShootResult.Hit;
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        public void MarkReceivedShoot(Shoot shoot)
        {
            var result = ShootResult(shoot);

            switch (result)
            {
                case web.ShootResult.Missed:
                    _positions[shoot.X, shoot.Y].State = PositionState.Missed;
                    break;
                case web.ShootResult.Hit:
                case web.ShootResult.HitDestroyed:
                    _positions[shoot.X, shoot.Y].State = PositionState.Hit;
                    break;
                case web.ShootResult.NotHandled:
                case web.ShootResult.NoSense:
                default:
                    throw new InvalidOperationException();
            }
        }

        private List<Position> ShipNeighbourPositions(Ship ship)
        {
            var output = new List<Position>();
            var flatBoard = GetBoardPositionsFlatted();

            foreach (var position in ship.Coordinates)
            {
                var positionNeighbours = flatBoard.Where(p =>
                (p.X >= position.X - 1 && p.Y >= position.Y - 1) &&
                (p.X <= position.X + 1 && p.Y <= position.Y + 1));

                output.AddRange(positionNeighbours);
            }

            return output.Distinct().ToList();
        }

        private Ship GetShipOnPosition(Position position)
        {
            return _ships.Where(s => s.Coordinates.Contains(position)).First();
        }
    }
}
