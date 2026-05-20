namespace RPG_Asn4
{
    public class TerminalDisplay : IDisplay
    {
        public void ShowWelcomeMessage()
        {
            Console.Title = "RPG Game";
            Clr.W("Welcome to the RPG Game!");
        }

        public void ShowMenuChoices(string[] choices)
        {
            Clr.W("\n");
            for(int i = 0; i < choices.Length; i++)
            {
                Clr.W($"{i + 1}. {choices[i]}");
            }
        }

        public void ShowHelp(string text)
        {
            Clr.W(text + "/n");
        }

        public void ShowError(string message)
        {
            Clr.R("Error: " + message +"\n");
        }

        public void Narrate(string message)
        {
            Clr.Y(message + "\n");
        }

        public void ShowListItem(string text)
        {
            Clr.DY(text + "\n");
        }

        public void ShowPlayerAction(string actionText)
        {
            Clr.LB(actionText + "\n");
        }

        public void ShowNpcAction(string actionText)
        {
            Clr.LB(actionText + "\n");
        }

        public void ShowPlayerInfo(Player player)
        {
            Clr.G($"Player Name: {player.Name}, Health: {player.Health}\n");
        }
        public void ShowInventory(List<Item> items)
        {
            if (items.Count == 0)
            {
                Narrate("\nYour inventory is empty.");
                return;
            }
            Narrate("\n--- Inventory ---");
            foreach (var item in items)
            {
                ShowListItem($"- {item.Name}: {item.Description}");
            }
        }

        public void ShowText(string input)
        {
            Clr.W(input + "\n");
        }
    }
        
}