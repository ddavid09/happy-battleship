namespace HappyBattleship.Library
{
    public interface IShootingStrategy
    {
        void UpdateStrategy(Shot lastShoot);
        Shot NewShoot();
    }
}