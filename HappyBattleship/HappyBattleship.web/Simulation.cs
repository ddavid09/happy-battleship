using System;
using System.Linq;
using System.Timers;

namespace HappyBattleship.web
{
    public class Simulation
    {
        private Player _leftPlayer;

        private Player _rightPlayer;

        private Timer _timer;

        private int _turn;

        public bool IsRunning { get; private set; }

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
                RaiseNewTurn();
            };

            _rightPlayer.ShootEvent += (s, e) =>
            {
                Console.WriteLine($"{_rightPlayer.NickName} shot x:{e.Shoot.targetX} y:{e.Shoot.targetY}");
                var shootResult = _leftPlayer.HandleReceivedShoot(e.Shoot);
                Console.WriteLine($"Shot result: {Enum.GetName(typeof(PositionState), shootResult)}");
                _rightPlayer.TrackShootResult(e.Shoot, shootResult);
                RaiseNewTurn();
            };

            _leftPlayer.LostEvent += FinishHandler;
            _rightPlayer.LostEvent += FinishHandler;

            _timer = new Timer(500);
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
            IsRunning = false;
            var player = (Player)sender;
            Console.WriteLine($"Player {player.NickName} Lost");
        }

        private void RaiseNewTurn()
        {
            var boardsState = _leftPlayer.PrimaryBoard.GetFlatBoardPositions().Concat(_rightPlayer.PrimaryBoard.GetFlatBoardPositions()).ToArray();
            var args = new TurnEventArgs
            {
                FlatBoardsPositions = boardsState,
                FlatLeftBoardPosition = _leftPlayer.PrimaryBoard.GetFlatBoardPositions(),
                FlatRightBoardPosition = _rightPlayer.PrimaryBoard.GetFlatBoardPositions()
            };
            OnNewTurn(args);
        }

        public void Init()
        {
            _leftPlayer.ArrangeBoards();
            _rightPlayer.ArrangeBoards();
            RaiseNewTurn();
        }

        public void Start()
        {
            IsRunning = true;
            _timer.Start();
        }

        public event EventHandler<TurnEventArgs> NewTurn;

        protected virtual void OnNewTurn(TurnEventArgs e)
        {
            NewTurn?.Invoke(this, e);
        }

    }
}
