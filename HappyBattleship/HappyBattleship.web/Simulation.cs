using System;
using System.Linq;
using System.Timers;

namespace HappyBattleship.web
{
    public class Simulation : ISimulation
    {
        private IPlayer _leftPlayer;

        private IPlayer _rightPlayer;

        private bool _turnBelongTo;

        private bool _isFinished;

        private Timer _timer;

        public bool IsRunning => _timer.Enabled == true;

        public bool IsFinished => _isFinished;

        public int TurnsInterval { get; set; } = 30;

        public Simulation(IPlayer leftPlayer, IPlayer rightPlayer)
        {
            _leftPlayer = leftPlayer;
            _rightPlayer = rightPlayer;

            _timer = new Timer(TurnsInterval);
            _timer.AutoReset = true;

            InitSimulationGoingLogic();
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

        private void InitSimulationGoingLogic()
        {
            _leftPlayer.NickName = "Left Player";
            _rightPlayer.NickName = "Right Player";

            _leftPlayer.ShootEvent += (s, e) =>
            {
                var shootResult = _rightPlayer.ShootResult(e.Shoot);
                e.Shoot.Result = shootResult;
                _rightPlayer.HandleReceivedShoot(e.Shoot);
                _leftPlayer.TrackFiredShootResult(e.Shoot);
                RaiseAfterTurn(e.Shoot, "right");
                if (shootResult == ShootResult.Missed)
                {
                    _turnBelongTo = !_turnBelongTo;
                }
            };

            _rightPlayer.ShootEvent += (s, e) =>
            {
                var shootResult = _leftPlayer.ShootResult(e.Shoot);
                e.Shoot.Result = shootResult;
                _leftPlayer.HandleReceivedShoot(e.Shoot);
                _rightPlayer.TrackFiredShootResult(e.Shoot);
                RaiseAfterTurn(e.Shoot, "left");
                if (shootResult == ShootResult.Missed)
                {
                    _turnBelongTo = !_turnBelongTo;
                }
            };

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

            var rand = new Random();

            _turnBelongTo = Convert.ToBoolean(rand.Next(2));

            RaiseOnInitiallised();
        }

        private void RaiseAfterTurn(Shoot shoot, string raiseOnSide)
        {
            var args = new TurnEventArgs();
            var positionToUpdate = new Position(shoot.X, shoot.Y);

            var shootResult = shoot.Result;

            if (shootResult == ShootResult.Hit || shootResult == ShootResult.HitDestroyed)
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

        public event EventHandler<TurnEventArgs> AfterTurn;

        public event EventHandler<SimInitialisedEventArgs> Initialised;

        protected virtual void OnAfterTurn(TurnEventArgs e)
        {
            AfterTurn?.Invoke(this, e);
        }

        protected virtual void OnInitilised(SimInitialisedEventArgs e)
        {
            Initialised?.Invoke(this, e);
        }


    }
}
