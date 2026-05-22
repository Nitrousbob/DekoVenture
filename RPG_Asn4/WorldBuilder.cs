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
   
            Location clearing = new Location("Small Clearing", "You stand in a quiet clearing surrounded by a dense forest");
            clearing.Interactables.AddRange(startNpcs);
            //Add a couple items to interact with to test
            clearing.Interactables.Add(ItemFactory.CreateGoldCoin(5));
            clearing.Interactables.Add(ItemFactory.CreateHealthDrink(2));
            

            Location darkWoods = new Location("Dark Woods", "The trees are thick and ominous, The light barely pierces the canopy");
            clearing.AddExit("north", darkWoods); //have to add the exit of the previous after the next room is built!
            darkWoods.Interactables.Add(ItemFactory.CreateLotion(1));
            darkWoods.AddExit("south", clearing);
            darkWoods.Interactables.Add(AnimalNpcFactory.GetStandardTier(1));



            return new Zone("Starting Zone", clearing);
        }
    }
}