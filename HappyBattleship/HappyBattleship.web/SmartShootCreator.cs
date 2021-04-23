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
        private ShotDecisioner Decisioner { get; set; }

        public SmartShootCreator()
        {
            NoSenseShoots = new List<Shoot>();
        }

        public Shoot CreateShoot()
        {
            Shoot lastShoot;
            if (CreatedShoots.Any())
            {

                lastShoot = CreatedShoots.Last();
            }
            else
            {
                lastShoot = null;
            }


            UpdateNoSenseShoots();

            Shoot shoot;

            if (lastShoot is not null)
            {
                if (lastShoot.Result == ShootResult.HitDestroyed ||
                    (lastShoot.Result == ShootResult.Missed && Decisioner == null))
                {
                    Decisioner = null;
                    shoot = NewRandomShoot();
                }
                else if (Decisioner is not null)
                {
                    shoot = Decisioner.DecideNewShoot();
                }
                else if (lastShoot.Result == ShootResult.Hit == Decisioner is null)
                {
                    Decisioner = new ShotDecisioner(lastShoot, NoSenseShoots);
                    shoot = Decisioner.DecideNewShoot();
                }
                else
                {
                    throw new InvalidOperationException("Last shoot created by SmartShootCreator has invalid result");
                }
            }
            else
            {
                shoot = NewRandomShoot();
            }

            return shoot;
        }

        private Shoot NewRandomShoot()
        {
            Shoot shoot;
            do
            {
                var randX = _random.Next(10);
                var randY = _random.Next(10);

                shoot = new Shoot
                {
                    X = randX,
                    Y = randY
                };

            } while (AlreadyCreated(shoot) == true || IsSenseShoot(shoot) == false);
            return shoot;
        }

        private void UpdateNoSenseShoots()
        {
            var lastShoot = CreatedShoots.Any() ? CreatedShoots.Last() : null;

            if (lastShoot is not null)
            {
                if (lastShoot.Result == ShootResult.Hit)
                {
                    NoSenseShoots.Add(new Shoot { X = lastShoot.X - 1, Y = lastShoot.Y - 1, Result = ShootResult.NoSense });
                    NoSenseShoots.Add(new Shoot { X = lastShoot.X + 1, Y = lastShoot.Y - 1, Result = ShootResult.NoSense });
                    NoSenseShoots.Add(new Shoot { X = lastShoot.X + 1, Y = lastShoot.Y + 1, Result = ShootResult.NoSense });
                    NoSenseShoots.Add(new Shoot { X = lastShoot.X - 1, Y = lastShoot.Y + 1, Result = ShootResult.NoSense });
                }

                if (lastShoot.Result == ShootResult.HitDestroyed)
                {
                    NoSenseShoots.Add(new Shoot { X = lastShoot.X - 1, Y = lastShoot.Y - 1, Result = ShootResult.NoSense });
                    NoSenseShoots.Add(new Shoot { X = lastShoot.X + 1, Y = lastShoot.Y - 1, Result = ShootResult.NoSense });
                    NoSenseShoots.Add(new Shoot { X = lastShoot.X + 1, Y = lastShoot.Y + 1, Result = ShootResult.NoSense });
                    NoSenseShoots.Add(new Shoot { X = lastShoot.X - 1, Y = lastShoot.Y + 1, Result = ShootResult.NoSense });
                    NoSenseShoots.Add(new Shoot { X = lastShoot.X, Y = lastShoot.Y - 1, Result = ShootResult.NoSense });
                    NoSenseShoots.Add(new Shoot { X = lastShoot.X, Y = lastShoot.Y + 1, Result = ShootResult.NoSense });
                    NoSenseShoots.Add(new Shoot { X = lastShoot.X - 1, Y = lastShoot.Y, Result = ShootResult.NoSense });
                    NoSenseShoots.Add(new Shoot { X = lastShoot.X + 1, Y = lastShoot.Y, Result = ShootResult.NoSense });
                }
            }
        }

        private bool IsSenseShoot(Shoot shoot)
        {
            return !(NoSenseShoots.Where(created => created.X == shoot.X && created.Y == shoot.Y).Count() > 0);
        }

        private bool AlreadyCreated(Shoot shoot)
        {
            return CreatedShoots.Where(created => created.X == shoot.X && created.Y == shoot.Y).Count() > 0;
        }
    }
}
