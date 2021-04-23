using System;
using System.Collections.Generic;
using System.Linq;

namespace HappyBattleship.web
{
    public class Player : IPlayer
    {
        private IBoard _primaryBoard;

        private IShootingStrategy _shootingStrategy;

        private IFleetCreator _fleetCreator;

        public string NickName { get; set; }

        public Player(IBoard primaryBoard, IShootingStrategy shootingStrategy, IFleetCreator fleetCreator)
        {
            _primaryBoard = primaryBoard;
            _shootingStrategy = shootingStrategy;
            _fleetCreator = fleetCreator;

            SetFleetOnBoard();
        }

        private void SetFleetOnBoard()
        {
            foreach (ShipClass shipClass in Enum.GetValues(typeof(ShipClass)))
            {
                Ship ship;
                do
                {
                    ship = _fleetCreator.CreateShip(shipClass);

                } while (_primaryBoard.CanPostShip(ship) == false);

                _primaryBoard.PostShip(ship);
            }
        }

        public Shoot Shoot()
        {
            var shoot = _shootingStrategy.NewShoot();

            RaiseOnShoot(shoot);
            return shoot;
        }

        public ShootResult ShootResult(Shoot shoot)
        {
            return _primaryBoard.ShootResult(shoot);
        }

        public void HandleReceivedShoot(Shoot shoot)
        {
            _primaryBoard.MarkReceivedShoot(shoot);

            if (_primaryBoard.AllShipsDestroyed)
            {
                RaiseOnLoser();
            }
        }

        public void TrackFiredShootResult(Shoot shoot)
        {
            _shootingStrategy.UpdateStrategy(shoot);
        }

        public Position[] GetPrimaryBoardFlatted()
        {
            return _primaryBoard.GetBoardPositionsFlatted();
        }

        public event EventHandler<ShootEventArgs> ShootEvent;

        public event EventHandler Loser;

        private void RaiseOnLoser()
        {
            OnLoser();
        }

        private void RaiseOnShoot(Shoot shoot)
        {
            var args = new ShootEventArgs
            {
                Shoot = shoot
            };

            OnShoot(args);
        }

        protected void OnShoot(ShootEventArgs e)
        {
            ShootEvent?.Invoke(this, e);
        }

        protected void OnLoser()
        {
            Loser?.Invoke(this, EventArgs.Empty);
        }
    }
}
