using System.Collections.Generic;

namespace HappyBattleship.web
{
    public interface IShootCreator
    {
        List<Shoot> CreatedShoots { get; set; }
        Shoot CreateShoot();
    }
}