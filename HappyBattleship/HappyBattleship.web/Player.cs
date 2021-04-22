using System;
using System.Collections.Generic;
using System.Linq;

namespace HappyBattleship.web
{
    public class Player
    {
        public string NickName { get; set; }
        public Board PrimaryBoard { get; private set; }
        public Board TrackingBoard { get; private set; }
        public List<Shoot> ShootHistory { get; private set; } = new List<Shoot>();

        private IShootCreator _shootCreator;

        private IBoardCreator _boardCreator;

        public Player()
        {
            _boardCreator = new RandomBoardCreator();
            _shootCreator = new SmartShootCreator
            {
                CreatedShoots = ShootHistory
            };
        }

        public Player(IBoardCreator boardCreator, IShootCreator shootCreator)
        {
            _boardCreator = boardCreator;
            _shootCreator = shootCreator;
            _shootCreator.CreatedShoots = ShootHistory;
        }

        public void ArrangeBoards()
        {
            PrimaryBoard = _boardCreator.CreateBoard();
            TrackingBoard = new Board();
        }

        public Shoot Shoot()
        {
            var shoot = _shootCreator.CreateShoot();
            ShootHistory.Add(shoot);

            var shootEventArgs = new ShootEventArgs
            {
                Shoot = shoot
            };
            OnShoot(shootEventArgs);
            return shoot;
        }

        public void TrackShootResult(Shoot lastShoot)
        {
            TrackingBoard.TrackShoot(lastShoot);
        }

        public void TrackShootResult()
        {
            var lastShoot = ShootHistory.Last();
            TrackingBoard.TrackShoot(lastShoot);
        }

        public ShootResult HandleReceivedShoot(Shoot shoot)
        {
            return PrimaryBoard.HandleShoot(shoot);
        }

        public bool Lose
        {
            get => PrimaryBoard.AllShipsDestroyed;
        }

        protected virtual void OnShoot(ShootEventArgs e)
        {
            if (Lose == false)
            {
                ShootEvent?.Invoke(this, e);
            }
            else
            {
                OnLose(e);
            }
        }

        protected virtual void OnLose(EventArgs e)
        {
            LostEvent?.Invoke(this, e);
        }

        public event EventHandler LostEvent;

        public event EventHandler<ShootEventArgs> ShootEvent;


    }
}
