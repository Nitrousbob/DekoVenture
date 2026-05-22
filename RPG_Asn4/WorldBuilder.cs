using System.Collections.Generic;

namespace RPG_Asn4
{
    public static class WorldBuilder
    {
        public static Zone CreateStartingZone()
        {
            var startNpcs = new List<IInteractable>();

            Location clearing = new Location("Small Clearing", "You stand in a quiet clearing surrounded by a dense forest");
            clearing.Interactables.AddRange(HumanNpcFactory.CreateTownsfolk(2));
            //add a secret npc with all the goodies authored
            var miser = new SecretMiser("William Buffalo", 15, "There is a hidden trapdoor beneath the roots of the oldest tree in the Dark Woods.", "Lotion")
            {
                IdleAction = "rubs his dry, cracked hands together",
                BusyStart = "pulls out an empty lotion bottle.",
                BusyAction = "tries to squeeze a final drop of lotion from the bottle.",
                BusyEnd = "sighs and puts the empty bottle away.",
                BusyRefusal = "is too busy lamenting his dry skin to talk."
            };
            clearing.Interactables.Add(miser);
            //Add a couple items to interact with to test
            clearing.Interactables.Add(ItemFactory.CreateGoldCoin(5));
            clearing.Interactables.Add(ItemFactory.CreateHealthDrink(2));
            

            Location darkWoods = new Location("Dark Woods", "The trees are thick and ominous, The light barely pierces the canopy");
            clearing.AddExit("north", darkWoods); //have to add the exit of the previous after the next room is built!
            darkWoods.Interactables.Add(ItemFactory.CreateLotion(1));
            var snitch = new SecretNpc("Da Snitch", 5, "That guy over there did it.")
            {
                IdleAction = "looks around nervously",
                BusyStart = "pulls his collar up.",
                BusyAction = "darts his eyes back and forth into the shadows.",
                BusyEnd = "relaxes slightly and lowers his collar.",
                BusyRefusal = "is too paranoid to speak to you right now."
            };
            darkWoods.Interactables.Add(snitch);
            darkWoods.AddExit("south", clearing);
            darkWoods.Interactables.Add(AnimalNpcFactory.GetStandardTier(1));
            darkWoods.Interactables.Add(new SecretAnimal("Precious", 10, "Dog", true, ItemFactory.CreateGoldCoin(10)));
            return new Zone("Starting Zone", clearing);
        }
    }
}