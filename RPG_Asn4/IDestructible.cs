namespace RPG_Asn4
{
    //I got to be able to destroy doors, trees, inanimate objects and shit
    public interface IDestructible
    {
        bool IsAlive { get; } // For an inanimate object, this just means "Is Intact"
        void TakeDamage(int damage);
    }
}
