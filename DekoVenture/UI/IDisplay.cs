
namespace DekoVenture
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
        void ShowInventory(List<(Item item, int count)> items);
        void ShowText(string input);
    }
}