namespace HappyBattleship.web
{
    public interface IShootingStrategy
    {
        void UpdateStrategy(Shoot lastShoot);
        Shoot NewShoot();
    }
}