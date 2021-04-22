using System;

namespace HappyBattleship.web
{
    public class Shoot
    {
        private bool _xWasSet;

        private bool _yWasSet;

        private bool _resultWasSet;

        private int _targetX;

        private int _targetY;

        private ShootResult _result;

        public int TargetX
        {
            get => _targetX;
            set
            {
                if (_xWasSet == false)
                {
                    _targetX = value;
                    _xWasSet = true;
                }
                else
                {
                    throw new InvalidOperationException("Shoot is immutable - target X can be assigned only once");
                }
            }
        }
        public int TargetY
        {
            get => _targetY;
            set
            {
                if (_yWasSet == false)
                {
                    _targetY = value;
                    _yWasSet = true;
                }
                else
                {
                    throw new InvalidOperationException("Shoot is immutable - target Y can be assigned only once");
                }
            }
        }
        public ShootResult Result
        {
            get => _result; set
            {
                if (_resultWasSet == false)
                {
                    _result = value;
                    _resultWasSet = true;
                }
                else
                {
                    throw new InvalidOperationException("Shoot is immutable - shoot result can be assigned only once");
                }
            }
        }
    }
}