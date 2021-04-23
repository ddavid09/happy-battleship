using System;

namespace HappyBattleship.web
{
    public interface IPlayer
    {
        string NickName { get; set; }
        Shoot Shoot();
        ShootResult ShootResult(Shoot shoot);
        void HandleReceivedShoot(Shoot shoot);
        void TrackShootResult(Shoot shoot);
        Position[] GetPrimaryBoardFlatted();

        event EventHandler<ShootEventArgs> ShootEvent;
    }
}