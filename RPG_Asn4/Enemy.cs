namespace RPG_Asn4
{
    public class Enemy : Actor, IAttackable, IInspectable
    {
        public string DisplayName => IsAlive ? Name : $"{Name} (loot)";
        public List<Item> LootItems {get; set;} = new List<Item>();
        public int GoldReward {get; set;}
        public bool HasLoot()
        {
            return GoldReward > 0 || LootItems.Count > 0;
        }  
        public Enemy(string name, int health) : base(name, health) {}

        public override void OnInteract(Player player)
        {
            
            UI.Narrate($"You see the {Name} in front of you.");
            bool interacting = true;
            while (interacting && IsAlive && player.IsAlive)
            {
                interacting = DialogFactory.HandleDialogTurn(this, player);
            }

            if (!IsAlive)
            {
                Loot(player);                
                return;
            }
        }

        public void Loot(Player player)
        {
            
            if (HasLoot())
            {

                if (GoldReward > 0)
                {
                    player.Gold += GoldReward;
                    UI.ShowPlayerAction($"You loot {GoldReward} gold from {Name}.");
                    GoldReward = 0;  // Set to 0 to indicate it's been looted
                }

                if (LootItems.Count > 0)
                {
                    foreach (Item item in LootItems)
                    {
                        Item? existingItem = player.Inventory.FirstOrDefault(i => i.Name == item.Name && i.isStackable);
                        if (existingItem != null)
                        {
                            existingItem.Quantity += item.Quantity;
                        }
                        else
                        {
                            player.Inventory.Add(item);  // Add to inventory
                        }

                        UI.ShowPlayerAction($"You loot {item.Name}. ");
                        LootItems.Remove(item); // Remove from loot after adding to inventory
                        if (LootItems.Count == 0)
                        {
                            LootItems.Clear();
                            break;
                        }
                    }
                }
            }
        }
        public override void TakeDamage(int damage)
        {
            Vitals.TakeDamage(damage);
            UI.Narrate($"You attack the {Name}! (Health: {Health})");
            
            if (!IsAlive)
            {
                UI.Narrate($"The {Name} is defeated!");
            }
        }

        public void EnterCombat(Player player)
        {
            UI.Narrate($"The {Name} comes at you bro.");
            int damage = EquippedWeapon != null ? EquippedWeapon.GetDamage() : Random.Shared.Next(1, 4);
            UI.ShowNpcAction($"{Name} attacks {player.Name} for {damage} damage!");
            player.TakeDamage(damage);
        }

        public string GetDescription()
        {
            if (IsAlive)
            {
                return $"You see {Name}. It looks hostile.";
            }

            return $"The defeated {Name} lies still.";
        }


    
    }
}
