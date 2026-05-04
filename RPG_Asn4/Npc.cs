namespace RPG_Asn4
{
    internal class Npc : Actor , IInteractable
    {
        public Npc(string name, int health) : base(name, health)
        {

        }

        public void OnInteract()
        {
            String[] NpcTownsfolkGreeting =  
            {
                "Lovely weather we're having, isn't it?",
                "Have you heard the latest gossip from the market?",
                "I remember when this town was just a small village.",
                "The harvest has been bountiful this year.",
                "Be careful out there, the woods can be dangerous.",
                "I heard there's a new shop opening up soon.",
                "The old mill is said to be haunted at night.",
                "Have you tried the baker's new pastry? It's delicious!",
                "The festival is coming up, it's always a good time.",
                "I wish I could travel, but I'm too old for that now."
            };
           
            int index = Random.Shared.Next(NpcTownsfolkGreeting.Length);
            Display.Igm($"\n{Name} says: '{NpcTownsfolkGreeting[index]}'");
        }

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

        private static readonly Func<Npc>[] _tier1Townsfolk =   //this is a recipe for creating NPCs that you can call on demand.
                                                                //It holds the instructions for how to make a standard tier 1 NPC,
                                                                //but doesnt actually make one until you call it.
        {
            () => new Npc(RandomNpcName(), Random.Shared.Next(10, 15)),  //creates a new NPC with a random name and health between 10 and 15
        };

        public static Npc GetStandardTier1()  //TODO: add the ability call any tier of NPC, Add a parameter for tier and have a switch
                                              //statement that calls the appropriate recipe based on the tier.
        {
            int index = Random.Shared.Next(_tier1Townsfolk.Length);
            return _tier1Townsfolk[index]();
        }
    }
}
