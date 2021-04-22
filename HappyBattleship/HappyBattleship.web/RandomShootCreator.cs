﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace HappyBattleship.web
{
    public class RandomShootCreator : IShootCreator
    {
        private Random _random = new();
        public List<Shoot> CreatedShoots { get; set; }

        public Shoot CreateShoot()
        {
            Shoot shoot;
            do
            {
                var randX = _random.Next(10);
                var randY = _random.Next(10);

                shoot = new Shoot
                {
                    TargetX = randX,
                    TargetY = randY
                };
            } while (AlreadyCreated(shoot) == true);

            return shoot;
        }

        private bool AlreadyCreated(Shoot shoot)
        {
            return CreatedShoots.Where(created => created.TargetX == shoot.TargetX && created.TargetY == shoot.TargetY).Count() > 0;
        }
    }
}
