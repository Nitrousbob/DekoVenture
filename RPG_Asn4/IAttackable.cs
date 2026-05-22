namespace RPG_Asn4
{
    public interface IAttackable : IDestructible
    {
        void EnterCombat(Player player);
    }
    
}