namespace RPG_Asn4
{
    public class Item : IInteractable, IInspectable
    {
        public string Name { get; set; }
        public string Description { get; set; }
        
        //There is an IInteractable interface that is implemented by the Item class. 
        // This interface has a method called canInteract that returns
        public bool canInteract => true;
        public bool isStackable;
        public int Quantity { get; set; } = 1;

        public Item(){}

        public Item(string name, string description)
        {
            Name = name;
            Description = description;
        }

        public string GetDescription() => Description;
        
        public void OnInteract(Player player){}

        public virtual bool Use(Player player)
        {
            UI.Narrate($"You can't use the {Name} right now");
            return false;
        }
        
        public void BlockInteraction(int turns) {}
        public void TickInteractionCooldown() {}
        
    }
}