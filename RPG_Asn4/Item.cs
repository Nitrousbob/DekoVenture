namespace RPG_Asn4
{
    public class Item : IInteractable, IInspectable
    {
        public string Name { get; set; }
        public string Description { get; set; }
        
        //There is an IInteractable interface that is implemented by the Item class. 
        // This interface has a method called canInteract that returns
        public bool canInteract => true;

        public Item(){}

        public Item(string name, string description)
        {
            Name = name;
            Description = description;
        }

        public string GetDescription() => Description;
        
        public void OnInteract(Player player)
        {}

        public void BlockInteraction(int turns) {}
        public void TickInteractionCooldown() {}
        
    }
}