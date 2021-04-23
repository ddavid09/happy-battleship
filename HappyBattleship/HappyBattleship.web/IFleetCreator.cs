namespace HappyBattleship.web
{
    public interface IFleetCreator
    {
        Ship CreateShip(ShipClass shipClass);
    }
}