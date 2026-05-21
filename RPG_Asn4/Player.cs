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
            // Map our inventory items directly to the tuple format using the new Quantity property
            var displayList = Inventory.Select(i => (item: i, count: i.Quantity)).ToList();

            UI.ShowInventory(displayList);
            if (displayList.Count == 0) return;
            
            UI.Narrate("0. Back");
            int choice = TakeInput.PromptIntRange("Select an item to use (or 0 to go back):",0, displayList.Count);
            if (choice > 0)
            {
                var selectedItem = displayList[choice -1].item;
                selectedItem.Use(this);
            }
        }
        
        public override void OnInteract(Player player)
        {
            throw new NotImplementedException();
        }
    }
}
