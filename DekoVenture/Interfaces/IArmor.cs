namespace DekoVenture
{
    public interface IArmor
    {
        string Name { get; }
        int DefenseValue { get; }

        //int DamageModifier might be better so some armor does not shield all kinds of damage?
        int CalculateDamageReduction(int incomingDamage);
    }
}