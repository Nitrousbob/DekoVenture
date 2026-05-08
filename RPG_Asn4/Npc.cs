namespace RPG_Asn4
{
    public class Npc : Actor, IInteractable, IInspectable
    {
        public bool HasEyes { get; private set; }
        public Npc(string name, int health, bool hasEyes) : base(name, health)
        {
            HasEyes = hasEyes;
        }

        public string GetDescription()
        {
            return $"You see {Name}, a character in this world. But does {Name} have character?";
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
