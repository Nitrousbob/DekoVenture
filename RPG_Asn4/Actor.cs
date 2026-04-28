namespace RPG_Asn4
{
    public abstract class Actor : IDamageable
    {
        public string Name { get; private set; }
        public int Health { get; private set; }

        protected Actor(string name, int health)
        {
            Name = name;
            Health = health;
        }

        protected Actor(string name)
        {
            Name = name;
        }

        public void TakeDamage(int amount)
        {
            Health -= amount;
            if (Health < 0) Health = 0;
            Console.WriteLine($"{Name} took {amount} damage. Remaining health: {Health}");
        }

        public void Heal(int amount)
        {
            Health += amount;
            Console.WriteLine($"{Name} healed {amount} health. Current health: {Health}");
        }
    }
}
