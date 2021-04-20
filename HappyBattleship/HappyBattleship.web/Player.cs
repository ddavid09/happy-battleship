using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HappyBattleship.web
{
    public class Player
    {
        public string NickName { get; set; }
        public Board PrimaryBoard { get; private set; }
        public Board TrackingBoard { get; private set; }
        public List<Shoot> Shot { get; private set; }

        public void ArrangeShips()
        {

        }

        public Shoot Shoot()
        {
            throw new NotImplementedException();
        }

        public PositionState HandleReceivedShoot(Shoot shoot)
        {
            throw new NotImplementedException();
        }


    }
}
