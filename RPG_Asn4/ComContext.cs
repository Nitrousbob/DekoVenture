namespace RPG_Asn4
{
    public class ComContext
    {
        public Player Player { get; }
        public IInteractable? CurrentTarget { get; }
        public bool EndInteration { get; set; } = false;
        //this will allow the interaction to end immediately if it has to in the case of an attack

        public ComContext(Player player, IInteractable? currentTarget)
        {
            this.Player = player;
            this.CurrentTarget = currentTarget;
        }
    }
}
