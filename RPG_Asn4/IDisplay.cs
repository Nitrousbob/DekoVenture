namespace RPG_Asn4
{
    public interface IDisplay
    {
        void ShowWelcomeMessage();
        void ShowMenuChoices(string[] choices);
        void ShowHelp(string text);
        void ShowError(string message);
        void Narrate(string message);
        void ShowListItem(string text);
        void ShowPlayerAction(string actionText);
        void ShowNpcAction(string actionText);
        void ShowPlayerInfo(Player player);
        void ShowInventory(List<Item> items);
        void ShowText(string input);
    }
}