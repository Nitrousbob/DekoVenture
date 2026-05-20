using Microsoft.VisualBasic;

namespace RPG_Asn4
{
    public static class UI
    {
        public static IDisplay Current {get; set;} = new TerminalDisplay();

        //---System and Menu
        public static void ShowWelcomeMessage() => Current.ShowWelcomeMessage();
        public static void ShowMenuChoices(string[] choices) => Current.ShowMenuChoices(choices);
        public static void ShowHelp(string text) => Current.ShowHelp(text);
        public static void ShowError(string message) => Current.ShowError(message);

        //---Game World and Narration
        public static void Narrate(string message) => Current.Narrate(message);
        public static void ShowListItem(string text) => Current.ShowListItem(text);

        //---Character and Interactions
        public static void ShowPlayerAction(string actionText) => Current.ShowPlayerAction(actionText);
        public static void ShowNpcAction(string actionText) => Current.ShowNpcAction(actionText);
        public static void ShowPlayerInfo(Player player) => Current.ShowPlayerInfo(player);
        public static void ShowInventory(List<Item> items) => Current.ShowInventory(items);

        //---Generic fallback text, ya know something
        public static void ShowText(string input) => Current.ShowText(input);
    }
}

