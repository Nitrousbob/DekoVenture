namespace RPG_Asn4
{
    public class ComContext
    {
        public Player player { get; }
        public IInteractable? CurrentTarget { get; }

        public ComContext(Player player, IInteractable? currentTarget)
        {
            this.player = player;
            this.CurrentTarget = currentTarget;
        }
    }
}
