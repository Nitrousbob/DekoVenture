namespace RPG_Asn4
{
public class SecretNpc : Npc, ISecretKeeper
    {
        private readonly ISecretKeeper _secretBehavior; //a property for a secretKeeper

        public string RequiredItemName { get; set; }
        public string Secret { get; set; }

        public SecretNpc(string name, int health, string requiredItemName, string secret) : base(name, health, true)
        {
            RequiredItemName = requiredItemName;
            Secret = secret;
        }

        public bool TryRevealSecret(Player player, Item? offeredItem = null)
        {
            return _secretBehavior.TryRevealSecret(player, offeredItem);
        }
    }

    //this SecretMiser wants something for their secrets
    public class SecretMiser : ISecretKeeper
    {
        public string RequiredItemName {get; set; }
        public string Secret {get; set;}

        public bool TryRevealSecret(Player player, Item? offeredItem = null)
        {
            if (offeredItem == null || offeredItem.Name != RequiredItemName)
            {
                UI.Narrate("They squint at you like information has a market price.");
                return false;
            }

            UI.Narrate($"{player.Name} leans closer.... ");
            UI.Narrate(Secret);
            return true;
        }
    }
}