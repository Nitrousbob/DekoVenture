
namespace DekoVenture
{
    public interface IDisplay
    {
        void ShowWelcomeMessage();
        void ShowMenuChoices(string[] choices);
        void ShowHelp(string text);
        void ShowError(string message);
        void Narrate(string text);
        void ShowListNumber(string text);
        void ShowPlayerAction(string actionText);
        void ShowNpcAction(string actionText);
        void ShowPlayerInfo(Player player);
        void ShowInventory(List<(Item item, int count)> items);
        void ShowText(string input);
        void ShowItem(string text);
        void ShowNpc(string text);
        void ShowCurrencyItem(string text);
        void EnemyDeath(string text);
        void ShowTitle();
        void NarrateLocation(string text);
        void ShowLocationOption(string text);
        void AllMenuOption(string text);
        void NpcTalkText(string text);
        void ShowStatusEffect(string text);
    }
}