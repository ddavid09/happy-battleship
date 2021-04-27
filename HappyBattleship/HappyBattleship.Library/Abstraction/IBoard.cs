namespace HappyBattleship.Library
{
    public interface IBoard
    {
        bool CanPostShip(Ship ship);
        void PostShip(Ship ship);
        ShotResult ShotResult(Shot Shoot);
        void MarkReceivedShoot(Shot shoot);
        bool AllShipsDestroyed { get; }
        Position[] GetBoardPositionsFlatted();
    }
}