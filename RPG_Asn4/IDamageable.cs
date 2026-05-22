namespace RPG_Asn4
{
    internal interface IDamageable
    {
        void TakeDamage(int damage);
        int Heal(int amount);
    }
}
