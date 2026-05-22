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

        //the new version of Use check to see if the item did anything and then if it was needed says it was used.
        public override bool Use(Player player)
        {
            bool used = false;
            
            if(HealAmount > 0)
            {
                int healed = player.Heal(HealAmount);
                
                if(healed > 0)
                {
                    UI.ShowPlayerAction($"You consume the {Name} and recover {healed} health.");
                    used = true;
                }
            }

            foreach (var effect in Cures)
            {
                bool hasEffect = player.Vitals.ActiveEffects.Any(e=> e.Type == effect);

                if (hasEffect)
                {
                    player.Vitals.CureEffect(effect);
                    UI.Narrate($"You have been cured of the {effect} effect!");
                    used = true;
                }
            }

            if (!used)
            {
                int choice = Random.Shared.Next(3);
                string[] notUsedResponse = {
                
                $"You don't need to use the {Name} right now.",
                $"Just going to keep that {Name} around for later.",
                $"Just testing bro, I'm gonna hold on to that {Name}."
                };
                UI.Narrate(notUsedResponse[choice]);
            }

            return used;
        }
    }
}