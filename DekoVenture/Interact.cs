namespace DekoVenture
{
    public interface IInteractable
    {
        string Name { get; }
        bool canInteract { get; }
        void OnInteract(Player player);
        void BlockInteraction(int turns);  // This method will be called to block interactions for a certain number of turns
        void TickInteractionCooldown();  // This method will be called each turn to manage interaction cooldowns
    }
}
