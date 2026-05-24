namespace DekoVenture
{
    public static class UI
    {
        public static IDisplay Current { get; set; } = new TerminalDisplay();

        //---System and Menu
        public static void ShowWelcomeMessage() => Current.ShowWelcomeMessage();
        public static void ShowMenuChoices(string[] choices) => Current.ShowMenuChoices(choices);
        public static void ShowHelp(string text) => Current.ShowHelp(text);
        public static void ShowError(string message) => Current.ShowError(message);

        //---Game World and Narration
        public static void Narrate(string message) => Current.Narrate(message);
        public static void ShowListNumber(string text) => Current.ShowListNumber(text);
        public static void ShowItem(string text) => Current.ShowItem(text);
        public static void ShowNpc(string text) => Current.ShowNpc(text);
        public static void ShowCurrencyItem(string text) => Current.ShowCurrencyItem(text);
        public static void EnemyDeath(string text) => Current.EnemyDeath(text);
        public static void ShowLocation(string text) => Current.ShowLocation(text);
        public static void SayLocation(string text) => Current.SayLocation(text);
        public static void ShowTitle() => Current.ShowTitle();
        public static void AllMenuOption(string text) => Current.AllMenuOption(text);

        //---Character and Interactions
        public static void ShowPlayerAction(string actionText) => Current.ShowPlayerAction(actionText);
        public static void ShowNpcAction(string actionText) => Current.ShowNpcAction(actionText);
        public static void ShowPlayerInfo(Player player) => Current.ShowPlayerInfo(player);
        public static void ShowInventory(List<(Item item, int count)> items) => Current.ShowInventory(items);

        //---Generic fallback text, ya know something
        public static void ShowText(string input) => Current.ShowText(input);
    }
}

