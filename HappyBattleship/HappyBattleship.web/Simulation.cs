using System;
using System.Timers;

namespace HappyBattleship.web
{
    public class Simulation
    {
        private Player _leftPlayer;

        private Player _rightPlayer;

        private Timer _timer;

        private int _turn;

        public Simulation()
        {
            _leftPlayer = new Player();
            _leftPlayer.NickName = "LeftComandor";

            _rightPlayer = new Player();
            _rightPlayer.NickName = "RightCommandor";

            _leftPlayer.ShootEvent += (s, e) =>
            {
                Console.WriteLine($"{_leftPlayer.NickName} shot x:{e.Shoot.targetX} y:{e.Shoot.targetY}");
                var shootResult = _rightPlayer.HandleReceivedShoot(e.Shoot);
                Console.WriteLine($"Shot result: {Enum.GetName(typeof(PositionState), shootResult)}");
                _leftPlayer.TrackShootResult(e.Shoot, shootResult);
            };

            _rightPlayer.ShootEvent += (s, e) =>
            {
                Console.WriteLine($"{_rightPlayer.NickName} shot x:{e.Shoot.targetX} y:{e.Shoot.targetY}");
                var shootResult = _leftPlayer.HandleReceivedShoot(e.Shoot);
                Console.WriteLine($"Shot result: {Enum.GetName(typeof(PositionState), shootResult)}");
                _rightPlayer.TrackShootResult(e.Shoot, shootResult);
            };

            _leftPlayer.LostEvent += FinishHandler;
            _rightPlayer.LostEvent += FinishHandler;

            _timer = new Timer(100);
            _timer.AutoReset = true;

            _timer.Elapsed += (s, e) =>
            {
                if (_turn == 0)
                {
                    _leftPlayer.Shoot();
                    _turn = 1;
                }
                else
                {
                    _rightPlayer.Shoot();
                    _turn = 0;
                }
            };
        }

        private void FinishHandler(object sender, EventArgs e)
        {
            _timer.Stop();
            var player = (Player)sender;
            Console.WriteLine($"Player {player.NickName} Lost");
        }

        public void Init()
        {
            _leftPlayer.ArrangeBoards();
            _rightPlayer.ArrangeBoards();
        }

        public void Start()
        {
            _timer.Start();
        }

    }
}
