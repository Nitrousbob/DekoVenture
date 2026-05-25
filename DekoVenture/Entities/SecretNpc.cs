namespace DekoVenture
{
    public class SecretNpc : Npc, ISecretKeeper
    {
        public string Secret { get; set; }

        public SecretNpc(string name, int health, string secret) : base(name, health, true)
        {
            Secret = secret;
        }

        public virtual bool TryRevealSecret(Player player, Item? offeredItem = null)
        {
            UI.Narrate($"You ask about the secret... ");  //can this display the subject?
            UI.Narrate(Secret);
            return true;
        }
    }

    //this SecretMiser wants something for their secrets
    public class SecretMiser : SecretNpc
    {
        public string RequiredItemName { get; set; }

        public SecretMiser(string name, int health, string secret, string requiredItemName) : base(name, health, secret)
        {
            RequiredItemName = requiredItemName;
        }

        public override bool TryRevealSecret(Player player, Item? offeredItem = null)
        {
            if (offeredItem == null || offeredItem.Name != RequiredItemName)
            {
                bool hasItem = player.Inventory.Any(i => i.Name.Equals(RequiredItemName, StringComparison.OrdinalIgnoreCase));
                if (hasItem)
                {
                    UI.Narrate($"They notice the {RequiredItemName} in your belongings. \"Give me that, and I'll tell you what I know.\"");
                }
                else
                {
                    UI.Narrate("They squint at you like information has a market price.");
                }
                return false;
            }

            UI.Narrate($"{player.Name} leans closer.... ");
            UI.Narrate(Secret);
            return true;
        }
    }
}