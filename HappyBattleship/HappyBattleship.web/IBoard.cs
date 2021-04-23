namespace HappyBattleship.web
{
    public interface IBoard
    {
        bool CanPostShip(Ship ship);
        void PostShip(Ship ship);
        ShootResult ShootResult(Shoot Shoot);
        void MarkReceivedShoot(Shoot shoot);
        bool AllShipsDestroyed { get; }
        Position[] GetBoardPositionsFlatted();
    }
}