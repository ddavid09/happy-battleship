using System;
using System.Collections.Generic;
using System.Linq;

namespace HappyBattleship.web
{
    public class Board
    {
        private List<Ship> _ships;
        private readonly Position[,] _positions;

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

        public Position[] GetShipsPositions()
        {
            return _ships.SelectMany(ship => ship.Coordinates).ToArray();
        }

        public Position[] GetFlatBoardPositions()
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

        private List<Position> ShipNeighbourPositions(Ship ship)
        {
            var output = new List<Position>();
            var flatBoard = GetFlatBoardPositions();

            foreach (var position in ship.Coordinates)
            {
                var positionNeighbours = flatBoard.Where(p =>
                (p.X >= position.X - 1 && p.Y >= position.Y - 1) &&
                (p.X <= position.X + 1 && p.Y <= position.Y + 1));

                output.AddRange(positionNeighbours);
            }

            return output.Distinct().ToList();
        }

        public void TrackShoot(Shoot shoot, PositionState resultState)
        {
            _positions[shoot.X, shoot.Y].State = resultState;
        }

        public void TrackShoot(Shoot shoot)
        {
            switch (shoot.Result)
            {
                case ShootResult.Missed:
                    _positions[shoot.X, shoot.Y].State = PositionState.Missed;
                    break;
                case ShootResult.Hit:
                case ShootResult.HitDestroyed:
                    _positions[shoot.X, shoot.Y].State = PositionState.Hit;
                    break;
                default:
                    throw new InvalidOperationException("Shoot you want to track, wasn't handled (has NotHandled state)");
            }
        }

        public ShootResult HandleShoot(Shoot shoot)
        {
            var targetState = _positions[shoot.X, shoot.Y].State;

            if (targetState == PositionState.Covered)
            {
                _positions[shoot.X, shoot.Y].State = PositionState.Hit;

                var shipOnPosition = GetShipOnPosition(_positions[shoot.X, shoot.Y]);

                if (shipOnPosition.Destroyed == true)
                {
                    shoot.Result = ShootResult.HitDestroyed;
                }
                else
                {
                    shoot.Result = ShootResult.Hit;
                }
            }
            else if (targetState == PositionState.Initial)
            {
                _positions[shoot.X, shoot.Y].State = PositionState.Missed;
                shoot.Result = ShootResult.Missed;
            }
            else
            {
                throw new InvalidOperationException($"Position {shoot.X}, {shoot.Y} was handled before");
            }

            return shoot.Result;
        }

        private Ship GetShipOnPosition(Position position)
        {
            return _ships.Where(s => s.Coordinates.Contains(position)).First();
        }
    }
}
