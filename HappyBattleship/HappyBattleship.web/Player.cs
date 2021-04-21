using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HappyBattleship.web
{
    public class Player
    {
        public string NickName { get; set; }
        public Board PrimaryBoard { get; private set; }
        public Board TrackingBoard { get; private set; }
        public List<Shoot> Shot { get; private set; }

        private IShootCreator _shootCreator;

        private IBoardCreator _boardCreator;

        public Player(IBoardCreator boardCreator, IShootCreator shootCreator)
        {
            _boardCreator = boardCreator;
            _shootCreator = shootCreator;
            Shot = new List<Shoot>();
            _shootCreator.Shot = Shot;
        }

        public void ArrangeBoards()
        {
            PrimaryBoard = _boardCreator.CreateBoard();
            TrackingBoard = new Board();
        }

        public Shoot Shoot()
        {
            var shoot = _shootCreator.CreateShoot();
            Shot.Add(shoot);

            var shootEventArgs = new ShootEventArgs
            {
                Shoot = shoot
            };
            OnShoot(shootEventArgs);
            return shoot;
        }

        public void TrackLastShootResult(PositionState result)
        {
            var lastShoot = Shot.Last();
            TrackingBoard.TrackShoot(lastShoot, result);
        }

        public PositionState HandleReceivedShoot(Shoot shoot)
        {
            return PrimaryBoard.HandleShoot(shoot);

        }

        public bool Lose
        {
            get => PrimaryBoard.GetShips().Select(ship => ship.Destroyed == true).Count() == Enum.GetNames(typeof(ShipClass)).Length;
        }

        protected virtual void OnShoot(ShootEventArgs e)
        {
            ShootEvent?.Invoke(this, e);
        }

        protected virtual void OnLose(EventArgs e)
        {
            LostEvent?.Invoke(this, e);
        }

        public event EventHandler LostEvent;

        public event EventHandler<ShootEventArgs> ShootEvent;


    }
}
