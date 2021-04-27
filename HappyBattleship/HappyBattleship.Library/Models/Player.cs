using Microsoft.Extensions.Logging;
using System;

namespace HappyBattleship.Library
{
    public class Player : IPlayer
    {
        private IBoard _primaryBoard;

        private IShootingStrategy _shootingStrategy;

        private IFleetCreator _fleetCreator;

        private ILogger<Player> _logger;

        public string NickName { get; set; }

        public Player(IBoard primaryBoard, IShootingStrategy shootingStrategy, IFleetCreator fleetCreator, ILogger<Player> logger)
        {
            _primaryBoard = primaryBoard;
            _shootingStrategy = shootingStrategy;
            _fleetCreator = fleetCreator;
            _logger = logger;
        }

        public void SetFleetOnBoard()
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

            _logger.LogInformation($"Player {NickName} is ready battle");
        }

        public Shot Shoot()
        {
            var shot = _shootingStrategy.NewShoot();

            RaiseOnShoot(shot);

            _logger.LogInformation($"Player {NickName} fired shot at x:{shot.X}, y:{shot.Y}");

            return shot;
        }

        public ShotResult ShotResult(Shot shot)
        {
            return _primaryBoard.ShotResult(shot);
        }

        public void HandleReceivedShot(Shot shot)
        {
            _primaryBoard.MarkReceivedShoot(shot);

            _logger.LogInformation($"Player {NickName} received shot (x:{shot.X}, y:{shot.Y}) - result: {ShotResult(shot)}");

            if (_primaryBoard.AllShipsDestroyed)
            {
                _logger.LogInformation($"All fleet of Player {NickName} is destroyed!, {NickName} lost");

                RaiseOnLoser();
            }
        }

        public void TrackFiredShotResult(Shot shoot)
        {
            _shootingStrategy.UpdateStrategy(shoot);
        }

        public Position[] GetPrimaryBoardFlatted()
        {
            return _primaryBoard.GetBoardPositionsFlatted();
        }

        public event EventHandler<ShotEventArgs> ShootEvent;

        public event EventHandler Loser;

        private void RaiseOnLoser()
        {
            OnLoser();
        }

        private void RaiseOnShoot(Shot shoot)
        {
            var args = new ShotEventArgs
            {
                Shot = shoot
            };

            OnShoot(args);
        }

        protected void OnShoot(ShotEventArgs e)
        {
            ShootEvent?.Invoke(this, e);
        }

        protected void OnLoser()
        {
            Loser?.Invoke(this, EventArgs.Empty);
        }
    }
}
