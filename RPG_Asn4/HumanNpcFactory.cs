using System;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using System.Text;

namespace RPG_Asn4
{
    public static class HumanNpcFactory
    {
        private static readonly Random _rng = new Random();

        //this is just a reference to an old project where I used to author Npc's,
        // private static readonly Func<Npc>[] _tier1Normal =
        // {
        //     //this holds recipes for enemies but doesnt cook them yet.
        //     () => new Npc("Thistlefolk Ambusher", 10),
        //     () => new Npc("Moss Crawler",5),
        //     () => new Npc("Ancient Skeleton",12),
        //     () => new Npc("Giant Rat", 14),
        //     () => new Npc("Bladed Guard",10)

        // };

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
            () => new Npc(RandomNpcName(), Random.Shared.Next(10, 15)),  //creates a new NPC with a random name and health between 10 and 15
        };

        public static Npc GetStandardTier(int tier = 1)  
        {
            int index = Random.Shared.Next(_tier1Townsfolk.Length);
            return _tier1Townsfolk[index]();
        }
    }
}
