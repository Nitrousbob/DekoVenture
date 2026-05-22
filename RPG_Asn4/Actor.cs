namespace RPG_Asn4
{
    public abstract class Actor : IDamageable, IInteractable
    {
        public StateMachine StateMachine { get; private set; } = new StateMachine();
        public string Name { get; private set; }
        public HealthManager Vitals {get; set;}  //not sure I like vitals yet
        public int Health => Vitals?.CurrentHealth ?? 0;  //A wrapper for our original Health
        public bool IsAlive => Health > 0;
        public int Gold {get; set;} = 0;
        public int interactionCooldown { get; private set; } = 0;
        public bool canInteract
        {
            get { return interactionCooldown <= 0; }
        }
        public IArmor? EquippedArmor {get; set;}
        public IWeapon? EquippedWeapon {get; set;}
        protected Actor(string name, int health)
        {
            Name = name;
            Vitals = new HealthManager(health);
        }

        protected Actor(string name)
        {
            Name = name;
        }
        public virtual void TakeDamage(int amount)
        {
            if (EquippedArmor != null)
            {
                int originalAmount = amount;
                amount = EquippedArmor.CalculateDamageReduction(amount);

                int blocked = originalAmount - amount;
                if(blocked > 0)
                {
                    UI.Narrate($"{Name}'s {EquippedArmor.Name} blocked {blocked} damage!");
                }
            }
            
            Vitals.TakeDamage(amount);
            UI.ShowNpcAction($"{Name} took {amount} damage. Remaining health: {Health}");
        }
        public int Heal(int amount)
        {
            int healed = Vitals.Heal(amount);
            UI.ShowNpcAction($"{Name} healed {amount} health. Current health: {Health}");
            return healed;
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
