namespace DekoVenture
{
    public class Player : Actor
    {
        //the player has to track its own state 
        public List<Item> Inventory { get; set; } = new List<Item>();

        public Player(string name, int health) : base(name, health){}

        public void ShowInventory()
        {
            // Map our inventory items directly to the tuple format using the new Quantity property
            var displayList = Inventory.Select(i => (item: i, count: i.Quantity)).ToList();

            UI.ShowInventory(displayList);
            if (displayList.Count == 0) return;
            UI.Narrate("I or 0. Back");
            while (true)
            {
                string input = TakeInput.GetString("Your selection adventurer: ").Trim().ToLower();
                if (input == "0" || input == "i" || input == "inventory" || input == "back")
                {
                    return;
                }

                if(int.TryParse(input, out int choice) && choice > 0 && choice <= displayList.Count)
                    {
                    var selectedItem = displayList[choice -1].item;
                    bool wasUsed = selectedItem.Use(this);

                    if (wasUsed)
                    {
                        selectedItem.Quantity--;
                        if (selectedItem.Quantity <= 0)
                        {
                            Inventory.Remove(selectedItem);
                        }
                    }
                    return;
                }
                UI.ShowError("Invalid Choice.");
            }    
        }
        
        public override void OnInteract(Player player)
        {
            throw new NotImplementedException();
        }
    }
}
