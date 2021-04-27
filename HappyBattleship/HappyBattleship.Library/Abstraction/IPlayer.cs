using System;

namespace HappyBattleship.Library
{
    public interface IPlayer
    {
        string NickName { get; set; }
        void SetFleetOnBoard();
        Shot Shoot();
        ShotResult ShotResult(Shot shoot);
        void HandleReceivedShot(Shot shoot);
        void TrackFiredShotResult(Shot shoot);
        Position[] GetPrimaryBoardFlatted();

        event EventHandler<ShotEventArgs> ShootEvent;

        event EventHandler Loser;
    }
}