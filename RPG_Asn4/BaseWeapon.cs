namespace RPG_Asn4
{
    public abstract class BaseWeapon : Item, IWeapon
    {
        public int MinDamage {get; protected set;}
        public int MaxDamage {get; protected set;}

        public BaseWeapon(string name, string description, int minDamage, int maxDamage): base(name, description)
        {
            MinDamage = minDamage;
            MaxDamage = maxDamage;
        }

        public virtual int GetDamage()
        {
            return Random.Shared.Next(MinDamage, MaxDamage +1);
        }
    }
}