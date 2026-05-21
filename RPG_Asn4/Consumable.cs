namespace RPG_Asn4
{
    public class Consumable : Item
    {
        public int HealAmount {get;set;}
        public List <EffectType> Cures {get;set;} = new List<EffectType>();
        
        public Consumable(){}

        public Consumable(string name, string description, int healAmount = 0, params EffectType[] cures) : base(name, description)
        {
            HealAmount = healAmount;
            Cures.AddRange(cures);
            isStackable = true;
        }

        public override void Use(Player player)
        {
            UI.ShowPlayerAction($"You consume the {Name}.");

            if(HealAmount > 0)
            {
                player.Heal(HealAmount);
            }

            foreach (var effect in Cures)
            {
                UI.Narrate($"You have been cured of the {effect} effect!");
            }

            // Consume and reduce quantity
            Quantity--;
            if (Quantity <= 0)
            {
                player.Inventory.Remove(this);
            }
        }
    }
}