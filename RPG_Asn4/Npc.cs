namespace RPG_Asn4
{
    internal class Npc : Actor , IInteractable
    {
        public Npc(string name, int health) : base(name, health)
        {

        }

        public void OnInteract()
        {
            Display.Igm($"\n{Name} says: 'Hello'.");
        }
    }
}
