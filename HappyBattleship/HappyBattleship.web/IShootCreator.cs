using System.Collections.Generic;

namespace HappyBattleship.web
{
    public interface IShootCreator
    {
        List<Shoot> Shot { get; set; }
        Shoot CreateShoot();
    }
}