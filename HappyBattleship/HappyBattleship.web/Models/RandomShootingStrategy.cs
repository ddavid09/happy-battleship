using System;
using System.Collections.Generic;

namespace HappyBattleship.web
{
    public class RandomShootingStrategy : IShootingStrategy
    {
        private List<Shoot> _possibleShoots;

        private Random _random;

        public RandomShootingStrategy()
        {
            _random = new Random();
            InitPossibleShootsArray();
        }

        private void InitPossibleShootsArray()
        {
            _possibleShoots = new List<Shoot>();

            for (var x = 0; x < 10; x++)
            {
                for (var y = 0; y < 10; y++)
                {
                    _possibleShoots.Add(new Shoot(x, y));
                }
            }
        }

        public Shoot NewShoot()
        {
            var randomShootIndex = _random.Next(_possibleShoots.Count);
            var newShoot = _possibleShoots[randomShootIndex];
            _possibleShoots.Remove(newShoot);
            return newShoot;
        }

        public void UpdateStrategy(Shoot lastShoot)
        {

        }
    }
}
