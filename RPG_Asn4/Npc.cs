namespace RPG_Asn4
{
    public class Npc : Actor , IInteractable
    {
        public Npc(string name, int health) : base(name, health)
        {

        }

        public void OnInteract(Player player)
        {
            string greeting = HumanDialogFactory.GetRandomGreeting(this);
            Display.Igm($"\n{Name} says: '{greeting}'");
            //make a way to track greeting amounts so that the greeting changes after 5 attempts

            //start dialog method
            HumanDialogFactory.Dialogger(this, player);  //Enters the dialog
        }
    }
}
