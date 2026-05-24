namespace DekoVenture
{
    public class TerminalDisplay : IDisplay
    {
        public void ShowWelcomeMessage()
        {
            Console.Title = "Deko Venture";
            ShowTitle();
            Clr.W("            ----------            Welcome to the future!              ----------\n");
        }

        public void ShowTitle()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            // This method can be used to show the title again if needed, or to show a different title for different zones or chapters.
            Clr.DB("██████╗ ███████╗██╗  ██╗ ██████╗     ██╗   ██╗███████╗███╗   ██╗████████╗██╗   ██╗██████╗ ███████╗\n");
            Clr.DB("██╔══██╗██╔════╝██║ ██╔╝██╔═══██╗    ██║   ██║██╔════╝████╗  ██║╚══██╔══╝██║   ██║██╔══██╗██╔════╝\n");
            Clr.DB("██║  ██║█████╗  █████╔╝ ██║   ██║    ██║   ██║█████╗  ██╔██╗ ██║   ██║   ██║   ██║██████╔╝█████╗\n");
            Clr.B("██║  ██║██╔══╝  ██╔═██╗ ██║   ██║    ╚██╗ ██╔╝██╔══╝  ██║╚██╗██║   ██║   ██║   ██║██╔══██╗██╔══╝\n");
            Clr.B("██████╔╝███████╗██║  ██╗╚██████╔╝     ╚████╔╝ ███████╗██║ ╚████║   ██║   ╚██████╔╝██║  ██║███████╗\n");
            Clr.LB("╚═════╝ ╚══════╝╚═╝  ╚═╝ ╚═════╝       ╚═══╝  ╚══════╝╚═╝  ╚═══╝   ╚═╝    ╚═════╝ ╚═╝  ╚═╝╚══════╝\n");
        }
        public void ShowMenuChoices(string[] choices)
        {
            Clr.W("\n");
            for (int i = 0; i < choices.Length; i++)
            {
                Clr.W($"{i + 1}. {choices[i]}");
            }
        }
        public void EnemyDeath(string text)
        {
            Clr.DR(text + "\n");
        }
        public void ShowHelp(string text)
        {
            Clr.W(text + "\n");
        }
        public void ShowError(string message)
        {
            Clr.R("Error: " + message + "\n");
        }
        public void Narrate(string message)
        {
            Clr.Gr(message + "\n");
        }
        public void ShowItem(string text)
        {
            Clr.O(text + "\n");
        }
        public void ShowListNumber(string text)
        {
            Clr.W(text);  //this is only the number in the Lists anywhere in the game
        }
        public void ShowNpc(string text)
        {
            Clr.LB(text + "\n");
        }
        public void ShowCurrencyItem(string text)
        {
            Clr.LGr(text + "\n");
        }
        public void ShowPlayerAction(string actionText)
        {
            Clr.DB(actionText + "\n");
        }
        public void ShowNpcAction(string actionText)
        {
            Clr.P(actionText + "\n");
        }
        public void ShowPlayerInfo(Player player)
        {
            Clr.B($"Player Name: {player.Name}, Health: {player.Health}\n");

            if (player.EquippedWeapon != null)
            {
                Clr.DB($"Weapon: {player.EquippedWeapon.Name} ({player.EquippedWeapon.MinDamage}-{player.EquippedWeapon.MaxDamage} damage)\n");
            }
            if (player.EquippedArmor != null)
            {
                Clr.DB($"Armor: {player.EquippedArmor.Name} ({player.EquippedArmor.DefenseValue} Def)\n");
            }
        }
        public void ShowInventory(List<(Item item, int count)> items)
        {
            if (items.Count == 0)
            {
                Narrate("\nYour inventory is empty.");
                return;
            }
            Narrate("\n--- Inventory ---");
            for (int i = 0; i < items.Count; i++)
            {
                string quantity = items[i].item.isStackable && items[i].count > 1 ? $" ({items[i].count})" : "";
                ShowItem($"{i + 1}. {items[i].item.Name}{quantity}: {items[i].item.Description}");
                
                //TODO Make a helper method to show each line item so this ShowInventory doesnt grow too much
                //Implement the different color schemes for the different Items
            }
        }
        public void ShowText(string input)
        {
            Clr.W(input + "\n");
        }
        public void ShowLocation(string text)
        {
            Clr.B(text);
        }
        public void SayLocation(string text)
        {
            Clr.B(text + "\n");
        }
        public void AllMenuOption(string text)
        {
            Clr.W(text);
        }
    
    }

}