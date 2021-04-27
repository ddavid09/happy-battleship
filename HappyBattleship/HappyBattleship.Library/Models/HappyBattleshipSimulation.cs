using System;
using System.Linq;
using System.Timers;

namespace HappyBattleship.Library
{
    public class HappyBattleshipSimulation : ISimulation
    {
        private IPlayer _leftPlayer;

        private IPlayer _rightPlayer;

        private bool _turnBelongTo;

        private bool _isFinished;

        private Timer _timer;

        public bool IsRunning => _timer.Enabled == true;

        public bool IsFinished => _isFinished;

        public int TurnsInterval { get; set; } = 30;

        public HappyBattleshipSimulation(IPlayer leftPlayer, IPlayer rightPlayer)
        {
            _leftPlayer = leftPlayer;
            _rightPlayer = rightPlayer;

            _timer = new Timer(TurnsInterval);
            _timer.AutoReset = true;
        }

        public void Start()
        {
            _timer.Start();
        }

        public void Pause()
        {
            _timer.Stop();
        }

        public void Stop()
        {
            _timer.Stop();
        }

        public void Init()
        {
            InitSimulationGoingLogic();
            InitTimer();
            InitRandomBeginingPlayer();
            InitFinishSimulationLogic();
            RaiseOnInitiallised();
        }

        private void InitRandomBeginingPlayer()
        {
            var rand = new Random();

            _turnBelongTo = Convert.ToBoolean(rand.Next(2));
        }

        private void InitTimer()
        {
            _timer.Elapsed += (s, e) =>
            {
                if (_turnBelongTo)
                {
                    _leftPlayer.Shoot();
                }
                else
                {
                    _rightPlayer.Shoot();
                }
            };
        }

        private void InitSimulationGoingLogic()
        {
            _leftPlayer.NickName = "Left Player";
            _rightPlayer.NickName = "Right Player";

            _leftPlayer.ShootEvent += (s, e) =>
            {
                var shotResult = _rightPlayer.ShotResult(e.Shot);
                e.Shot.Result = shotResult;
                _rightPlayer.HandleReceivedShot(e.Shot);
                _leftPlayer.TrackFiredShotResult(e.Shot);
                RaiseAfterTurn(e.Shot, "right");
                if (shotResult == ShotResult.Missed)
                {
                    _turnBelongTo = !_turnBelongTo;
                }
            };

            _leftPlayer.SetFleetOnBoard();

            _rightPlayer.ShootEvent += (s, e) =>
            {
                var shotResult = _leftPlayer.ShotResult(e.Shot);
                e.Shot.Result = shotResult;
                _leftPlayer.HandleReceivedShot(e.Shot);
                _rightPlayer.TrackFiredShotResult(e.Shot);
                RaiseAfterTurn(e.Shot, "left");
                if (shotResult == ShotResult.Missed)
                {
                    _turnBelongTo = !_turnBelongTo;
                }
            };

            _rightPlayer.SetFleetOnBoard();
        }

        private void InitFinishSimulationLogic()
        {
            EventHandler finishAction = (s, e) =>
            {
                Stop();
                _isFinished = true;
                RaiseFinished();
            };

            _leftPlayer.Loser += finishAction;
            _rightPlayer.Loser += finishAction;
        }

        private void RaiseAfterTurn(Shot shoot, string raiseOnSide)
        {
            var args = new TurnEventArgs();
            var positionToUpdate = new Position(shoot.X, shoot.Y);

            var shotResult = shoot.Result;

            if (shotResult == ShotResult.Hit || shotResult == ShotResult.HitDestroyed)
            {
                positionToUpdate.State = PositionState.Hit;
            }
            else
            {
                positionToUpdate.State = PositionState.Missed;
            }

            args.UpdateAtPlayer = raiseOnSide;
            args.PositionToUpdate = positionToUpdate;

            OnAfterTurn(args);
        }

        private void RaiseOnInitiallised()
        {
            var args = new SimInitialisedEventArgs();

            args.LeftBoardToDraw = _leftPlayer.GetPrimaryBoardFlatted();
            args.RightBoardToDraw = _rightPlayer.GetPrimaryBoardFlatted();

            args.PlayerBegining = _turnBelongTo ? _leftPlayer.NickName : _rightPlayer.NickName;

            OnInitilised(args);
        }

        private void RaiseFinished()
        {
            OnFinished();
        }

        public event EventHandler<TurnEventArgs> AfterTurn;

        public event EventHandler<SimInitialisedEventArgs> Initialised;

        public event EventHandler Finished;

        protected virtual void OnAfterTurn(TurnEventArgs e)
        {
            AfterTurn?.Invoke(this, e);
        }

        protected virtual void OnInitilised(SimInitialisedEventArgs e)
        {
            Initialised?.Invoke(this, e);
        }

        protected virtual void OnFinished()
        {
            Finished?.Invoke(this, EventArgs.Empty);
        }


    }
}
