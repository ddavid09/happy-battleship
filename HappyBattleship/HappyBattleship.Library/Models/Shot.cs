using System;

namespace HappyBattleship.Library
{
    public class Shot
    {
        private bool _xWasSet;

        private bool _yWasSet;

        private bool _resultWasSet;

        private int _x;

        private int _y;

        private ShotResult _result;

        public Shot(int x, int y)
        {
            X = x;
            Y = y;
            _result = ShotResult.NotHandled;
        }

        public int X
        {
            get => _x;
            set
            {
                if (_xWasSet == false)
                {
                    _x = value;
                    _xWasSet = true;
                }
                else
                {
                    throw new InvalidOperationException("Shoot is immutable - target X can be assigned only once");
                }
            }
        }
        public int Y
        {
            get => _y;
            set
            {
                if (_yWasSet == false)
                {
                    _y = value;
                    _yWasSet = true;
                }
                else
                {
                    throw new InvalidOperationException("Shoot is immutable - target Y can be assigned only once");
                }
            }
        }
        public ShotResult Result
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