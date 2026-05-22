namespace RPG_Asn4
{
    public abstract class BaseArmor : Item, IArmor
    {
                public int DefenseValue {get; protected set;}
        public BaseArmor(string name, string description, int defenseValue) : base(name, description)
        {
            DefenseValue = defenseValue;
        }

        public virtual int CalculateDamageReduction(int incomingDamage)
        {
            int reducedDamage = incomingDamage - DefenseValue;
            return reducedDamage < 0 ? 0 : reducedDamage; //damage cannot be negative, or can it?
        }
    }
}
