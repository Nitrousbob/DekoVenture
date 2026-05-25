namespace DekoVenture
{
    public static class HumanNpcFactory
    {
        private static readonly Random _rng = new Random();

        public static string RandomNpcName()
        {
            //in luei of authored bosses for now NPC names generated with ChatGpt.
            String[] NpcTownsfolkName =
            {
                "Old Man Bramble","Granny Mosswick","Farmer Thistletoe","Millie Fernwhistle","Barnaby Rootwell","Elara Greenbloom","Mayor Oakenford",
                "Jasper Mudboots","Tilda Briarpatch","Finnick Leafturner","Dockmaster Gulliver","Pearl Tidewell","Old Salt Marrow","Captain Netterby",
                "Mira Shellsong","Barnacle Ben","Coralyn Drift","Fisher Tomlin","Selkie Wavecrest","Jonah Kelpwick","Amara Dunesong","Tarek Sandstep",
                "Old Mother Claypot","Merchant Vashir","Nima Sunveil","Farid Dustcloak","Zara Cactusflower","Jebidiah Drywell","Salma Emberglass",
                "Omar Shadehand","Greta Snowmend","Harold Frostbeard","Inga Hearthwarm","Borin Icepatch","Elsa Woolwick","Old Nan Wintertoe","Torren Snowshoe",
                "Mira Coldbrook","Yana Frostbell","Silas Chimneykin","Gearson Cogwright","Mira Sparkhand","Old Wrench Wilby","Professor Brasswick",
                "Tilly Steamwhistle","Barnabus Bolt","Clara Copperpot","Edwin Geargrind","Nora Lampwick","Finch Tinkerbell"
            };

            int index = Random.Shared.Next(NpcTownsfolkName.Length);
            return NpcTownsfolkName[index];
        }

        //recipe list for Npc's
        private static readonly Func<Npc>[] _tier1Townsfolk =
        {
            () => new Npc(RandomNpcName(), Random.Shared.Next(10, 15), RollHasEyes(90)),
        };

        public static Npc GetStandardTier(int tier = 1)
        {
            int index = Random.Shared.Next(_tier1Townsfolk.Length);
            Npc npc = _tier1Townsfolk[index]();
            AssignRandomBehaviors(npc);
            return npc;
        }

        private static void AssignRandomBehaviors(Npc npc)
        {
            int choice = Random.Shared.Next(4);
            switch (choice)
            {
                case 0:
                    npc.IdleAction = "shifts their weight idly.";
                    npc.BusyStart = "pulls out a yo-yo.";
                    npc.BusyAction = "does the 'Walk the Dog' trick with their yo-yo.";
                    npc.BusyEnd = "puts the yo-yo away.";
                    npc.BusyRefusal = "is too focused on their yo-yo to talk right now.";
                    npc.AttackReaction = "Keep your mitts off the merchandise, space cowboy!";
                    break;
                case 1:
                    npc.IdleAction = "stares up at the sky.";
                    npc.BusyStart = "pulls out a small, worn book.";
                    npc.BusyAction = "flips a page in their book, reading intently.";
                    npc.BusyEnd = "snaps their book shut and pockets it.";
                    npc.BusyRefusal = "is too engrossed in their book to talk right now.";
                    npc.AttackReaction = "";
                    break;
                case 2:
                    npc.IdleAction = "whistles a quiet tune.";
                    npc.BusyStart = "pulls a gold coin from their pocket.";
                    npc.BusyAction = "rolls the coin gracefully across their knuckles.";
                    npc.BusyEnd = "snatches the coin from the air and puts it away.";
                    npc.BusyRefusal = "is too busy practicing a coin trick to talk right now.";
                    npc.AttackReaction = "Do I look like a malfunctioning vending machine to you?";
                    break;
                case 3:
                    npc.IdleAction = "kicks a pebble on the ground.";
                    npc.BusyStart = "takes out a piece of wood and a small knife.";
                    npc.BusyAction = "whittles away at the piece of wood.";
                    npc.BusyEnd = "blows the sawdust off their carving and puts it away.";
                    npc.BusyRefusal = "is too focused on whittling to talk right now.";
                    npc.AttackReaction = "Time to recalibrate your face.";
                    break;
            }
        }

        //a helper method to make groups of townsfolks, to sprinkle some unauthored folks around
        public static List<IInteractable> CreateTownsfolk(int count)
        {
            //create the return type, the List
            var group = new List<IInteractable>();

            //make count many
            for (int i = 0; i < count; i++)
            {
                group.Add(GetStandardTier(1));
            }
            return group;
        }

        private static bool RollHasEyes(int chanceEyes)
        {
            return Random.Shared.Next(100) < chanceEyes;
        }
    }
}
