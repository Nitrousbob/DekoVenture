namespace DekoVenture
{
    public class Player : Actor
    {
        //the player has to track its own state 
        public List<Item> Inventory { get; set; } = new List<Item>();

        public Player(string name, int health) : base(name, health) { }

        public void ShowInventory()
        {
            while (true)
            {
                // Map inventory items dynamically every loop so the list updates when items are used
                var displayList = Inventory.Select(i => (item: i, count: i.Quantity)).ToList();

                UI.ShowInventory(displayList);
                if (displayList.Count == 0) return;
                
                UI.Narrate("X. Exit");

                string input = TakeInput.GetString("Your selection: ").Trim().ToLower();
                
                if (input == "x" || input == "exit" || input == "i" || input == "inventory")
                {
                    return;
                }

                if (int.TryParse(input, out int choice) && choice > 0 && choice <= displayList.Count)
                {
                    var selectedItem = displayList[choice - 1].item;
                    
                    UI.Narrate($"\nSelected: <Y>{selectedItem.Name}</Y>");
                    string actionInput = TakeInput.GetString("1. Use \n2. Inspect \nX. Exit").Trim().ToLower();
                    
                    if (actionInput == "1" || actionInput == "use")
                    {
                        bool wasUsed = selectedItem.Use(this);
                        if (wasUsed)
                        {
                            selectedItem.Quantity--;
                            if (selectedItem.Quantity <= 0)
                            {
                                Inventory.Remove(selectedItem);
                            }
                        }
                    }
                    else if (actionInput == "2" || actionInput == "inspect" || actionInput == "look")
                    {
                        UI.ShowItem($"\nInspect: {selectedItem.GetDescription()}\n");
                        TakeInput.GetString("Press Enter to continue...");
                    }
                    else if (actionInput == "x" || actionInput == "cancel" || actionInput == "exit")
                    {
                        // Do nothing, letting the loop continue and redraw the list
                    }
                    // This brings you back to the inventory list!
                    continue;
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
