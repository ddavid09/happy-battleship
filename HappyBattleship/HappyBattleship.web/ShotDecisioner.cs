using System;
using System.Collections.Generic;
using System.Linq;

namespace HappyBattleship.web
{
    public class ShotDecisioner
    {
        private Random _random = new();
        public ShipDirection DeductedShipDirection { get; set; } = ShipDirection.Unknown;
        public List<Shoot> SenseShootsBasedOnDecision { get; private set; }
        public List<Shoot> MadeShootsInThisDecision { get; private set; }
        public List<Shoot> NoSenseShoots { get; private set; }

        public ShotDecisioner(Shoot shootDecisionInit, List<Shoot> noSenseShoots)
        {
            NoSenseShoots = noSenseShoots;
            SenseShootsBasedOnDecision = new List<Shoot>();
            MadeShootsInThisDecision = new List<Shoot>();

            MadeShootsInThisDecision.Add(shootDecisionInit);

            if (shootDecisionInit.X + 1 <= 9)
            {
                SenseShootsBasedOnDecision.Add(new Shoot { X = shootDecisionInit.X + 1, Y = shootDecisionInit.Y });
            }

            if (shootDecisionInit.X - 1 >= 0)
            {
                SenseShootsBasedOnDecision.Add(new Shoot { X = shootDecisionInit.X - 1, Y = shootDecisionInit.Y });
            }

            if (shootDecisionInit.Y + 1 <= 9)
            {
                SenseShootsBasedOnDecision.Add(new Shoot { X = shootDecisionInit.X, Y = shootDecisionInit.Y + 1 });
            }

            if (shootDecisionInit.Y - 1 >= 0)
            {
                SenseShootsBasedOnDecision.Add(new Shoot { X = shootDecisionInit.X, Y = shootDecisionInit.Y - 1 });
            }
        }

        public Shoot DecideNewShoot()
        {

            if (DeductedShipDirection == ShipDirection.Unknown)
            {
                if (MadeShootsInThisDecision.Count < 2)
                {
                    var resultShoot = NextSenseShoot();

                    MadeShootsInThisDecision.Add(resultShoot);
                    SenseShootsBasedOnDecision.Remove(resultShoot);

                    return resultShoot;
                }
                else
                {
                    var lastShoot = MadeShootsInThisDecision.Last();
                    var prevLastShoot = MadeShootsInThisDecision.First();

                    if (lastShoot.Result == ShootResult.Hit)
                    {
                        if (lastShoot.X == prevLastShoot.X)
                        {
                            DeductedShipDirection = ShipDirection.Vertical;
                            var toRemove = SenseShootsBasedOnDecision.Where(s => s.X != lastShoot.X).ToList();
                            foreach (var s in toRemove)
                            {
                                SenseShootsBasedOnDecision.Remove(s);
                            }
                        }
                        else
                        {
                            DeductedShipDirection = ShipDirection.Horizontal;
                            var toRemove = SenseShootsBasedOnDecision.Where(s => s.Y != lastShoot.Y).ToList();
                            foreach (var s in toRemove)
                            {
                                SenseShootsBasedOnDecision.Remove(s);
                            }
                        }

                        var resultShoot = NextSenseShoot();

                        MadeShootsInThisDecision.Add(resultShoot);
                        SenseShootsBasedOnDecision.Remove(resultShoot);

                        return resultShoot;

                    }
                    else if (lastShoot.Result == ShootResult.Missed)
                    {
                        var resultShoot = NextSenseShoot();

                        MadeShootsInThisDecision.Add(resultShoot);
                        SenseShootsBasedOnDecision.Remove(resultShoot);

                        return resultShoot;
                    }
                }
            }
            else if (DeductedShipDirection == ShipDirection.Horizontal)
            {
                throw new NotImplementedException();
            }
            else
            {
                throw new NotImplementedException();
            }
            throw new NotImplementedException();
        }

        private Shoot NextSenseShoot()
        {
            var senseShots = (from a in SenseShootsBasedOnDecision
                              join b in NoSenseShoots on new { a.X, a.Y } equals new { b.X, b.Y } into ab
                              from b in ab.DefaultIfEmpty()
                              select new Shoot { X = a.X, Y = a.Y }).ToList();

            var randIndex = _random.Next(senseShots.Count());
            var resultShoot = senseShots[randIndex];
            return resultShoot;
        }
    }
}
