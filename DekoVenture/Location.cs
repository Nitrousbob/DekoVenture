using System.Collections.Generic;

namespace DekoVenture
{
    public class Location
    {
        public string Name { get; set; }
        public string Description { get; set; }
        
        // Replaces the "Locals" list from Scene
        public List<IInteractable> Interactables { get; set; }
        
        // A dynamic map of exits. 
        // Key = "north", "enter shop", "board ship". Value = The destination Location.
        public Dictionary<string, Location> Exits { get; private set; }

        public Location(string name, string description)
        {
            Name = name;
            Description = description;
            Interactables = new List<IInteractable>();
            Exits = new Dictionary<string, Location>();
        }

        public void AddExit(string command, Location destination)
        {
            Exits[command.ToLower()] = destination;
        }
    }
}