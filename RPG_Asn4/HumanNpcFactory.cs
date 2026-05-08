using System;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using System.Text;

namespace RPG_Asn4
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
            return _tier1Townsfolk[index]();
        }

        private static bool RollHasEyes(int chanceEyes)
        {
            return Random.Shared.Next(100) < chanceEyes;
        }
    }
}
