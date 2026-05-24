namespace DekoVenture
{
    public interface IAttackable : IDestructible
    {
        void EnterCombat(Player player);
    }

}