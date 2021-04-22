using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HappyBattleship.web
{
    public class SmartShootCreator : IShootCreator
    {
        private Random _random = new();
        public List<Shoot> CreatedShoots { get; set; }
        private List<Shoot> NoSenseShoots { get; set; }

        private Shoot _lastShoot { get; set; }

        public SmartShootCreator()
        {
            NoSenseShoots = new List<Shoot>();
        }

        public Shoot CreateShoot()
        {
            Shoot shoot;

            UpdateNoSenseShoots();

            do
            {
                var randX = _random.Next(10);
                var randY = _random.Next(10);

                shoot = new Shoot
                {
                    TargetX = randX,
                    TargetY = randY
                };

            } while (AlreadyCreated(shoot) == true || IsSenseShoot(shoot) == false);

            _lastShoot = shoot;
            return shoot;
        }

        private void UpdateNoSenseShoots()
        {
            if (_lastShoot is not null)
            {
                if (_lastShoot.Result == ShootResult.Hit || _lastShoot.Result == ShootResult.HitDestroyed)
                {
                    NoSenseShoots.Add(new Shoot { TargetX = _lastShoot.TargetX - 1, TargetY = _lastShoot.TargetY - 1, Result = ShootResult.NoSense });
                    NoSenseShoots.Add(new Shoot { TargetX = _lastShoot.TargetX + 1, TargetY = _lastShoot.TargetY - 1, Result = ShootResult.NoSense });
                    NoSenseShoots.Add(new Shoot { TargetX = _lastShoot.TargetX + 1, TargetY = _lastShoot.TargetY + 1, Result = ShootResult.NoSense });
                    NoSenseShoots.Add(new Shoot { TargetX = _lastShoot.TargetX - 1, TargetY = _lastShoot.TargetY + 1, Result = ShootResult.NoSense });
                }
            }
        }

        private bool IsSenseShoot(Shoot shoot)
        {
            return !(NoSenseShoots.Where(created => created.TargetX == shoot.TargetX && created.TargetY == shoot.TargetY).Count() > 0);
        }

        private bool AlreadyCreated(Shoot shoot)
        {
            return CreatedShoots.Where(created => created.TargetX == shoot.TargetX && created.TargetY == shoot.TargetY).Count() > 0;
        }
    }
}
