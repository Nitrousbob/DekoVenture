namespace RPG_Asn4
{
    public abstract class Actor : IDamageable, IInteractable
    {
        public StateMachine StateMachine { get; private set; } = new StateMachine();

        public string Name { get; private set; }
        public int Health { get; private set; }
        public int interactionCooldown { get; private set; } = 0;
        public bool canInteract
        {
            get { return interactionCooldown <= 0; }
        }

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
            UI.ShowNpcAction($"{Name} took {amount} damage. Remaining health: {Health}");
        }

        public void Heal(int amount)
        {
            Health += amount;
            UI.ShowNpcAction($"{Name} healed {amount} health. Current health: {Health}");
        }
        public void BlockInteraction(int turns)
        {
            interactionCooldown = turns;
        }

        public void TickInteractionCooldown()
        {
            if (interactionCooldown > 0)
            {
                interactionCooldown--;
            }
        }

        public abstract void OnInteract(Player player);
        
    }

        
}

