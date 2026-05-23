namespace DekoVenture
{
    internal interface IDamageable
    {
        void TakeDamage(int damage);
        int Heal(int amount);
    }
}
