using System.Collections.Generic;

namespace RPG_Asn4
{
    public static class WorldBuilder
    {
        public static Zone CreateStartingZone()
        {
            var startNpcs = new List<IInteractable>();

            for (int i = 0; i < 2; i++)
            {
                startNpcs.Add(HumanNpcFactory.GetStandardTier(1));
            }

            startNpcs.Add(AnimalNpcFactory.GetStandardTier(1));
            
            Location clearing = new Location("Small Clearing", "You stand in a quiet clearing surrounded by a dense forest");
            clearing.Interactables.AddRange(startNpcs);
            Item coin = new Item("Shiny coin", "A slightly tarnished but shiny gold coin.");
            clearing.Interactables.Add(coin);

            Location darkWoods = new Location("Dark Woods", "The trees are thick and ominous, The light barely pierces the canopy");
            clearing.AddExit("north", darkWoods);
            darkWoods.AddExit("south", clearing);

            return new Zone("Starting Zone", clearing);
        }
    }
}