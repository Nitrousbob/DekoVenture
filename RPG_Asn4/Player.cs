namespace RPG_Asn4
{
    public class Player : Actor
    {
        //the player has to track its own state 
        public List<Item> Inventory { get; set; } = new List<Item>();

        public Player(string name, int health) : base(name, health)
        {
            
        }

        public void ShowInventory()
        {
            if(Inventory.Count  == 0)
            {
                UI.Narrate("Your inventory is empty.");
                return;
            }

            UI.Narrate("Your Inventory:");
            foreach (var item in Inventory)
            {
                UI.ShowInventory(Inventory);
            }
        }
        public override void OnInteract(Player player)
        {
            throw new NotImplementedException();
        }
    }
}
