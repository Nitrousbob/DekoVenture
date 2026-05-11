namespace RPG_Asn4
{
    internal class AnimalNpcFactory
    {
        public static string RandomAnimalName()
        {
            //in luei of authored bosses for now NPC names generated with ChatGpt.
            String[] AnimalNames =
            {
                "Mossback","Bramblehorn","Fernfoot","Old Snout","Thistlepaw","Willowtail","Acornwhisker",
                "Grumblehoof","Leafnose","Cloverclaw","Mudstripe","Pinefang","Elderbranch","Twiglet",
                "Barkhide","Mushroomback","Nettlewing","Dewdrop","Ivycoil","Rootsniffer",

                "Barnacleback","Kelpfin","Coralwhisker","Old Shellsnapper","Tidepool","Pearlscale","Driftwood",
                "Brinefang","Foamfoot","Sandyclaw","Wavecrest","Moonfin","Saltbeard","Bluegill",
                "Stormshell","Reefnose","Bubbleback","Gulliver","Mistfin","Deepwhisker",

                "Dustpaw","Sandstripe","Cactusback","Emberfang","Dunewing","Sunscale","Pebblehoof",
                "Old Drysnout","Ashclaw","Miragewing","Clayback","Oasisfoot","Brassfang",
                "Thornhide","Sunstreak","Brittlebone","Dusty Whiskers","Saffronback","Glassscale",

                "Frostpaw","Snowhoof","Icetooth","Old Woolback","Glacierhorn","Winterwhisker","Hailclaw",
                "Snowbell","Boreal","Shiverfin","Icepatch","Frostnose","Blizzardwing","Coldfoot",
                "Bluefur","Crystalshell","Sleetstripe","Polar Snout","Frozenpaw","Aurora",

                "Copperclaw","Steamwhistle","Gearback","Brasswing","Old Rivetnose","Cogtail","Tinpaw",
                "Boltshell","Sprocket Beak","Ironwhisker","Rustfang","Lampwick","Pistonhoof","Clockwork",
                "Nickelback","Wrenchjaw","Copperpot","Sparkpaw","Geargrind","Bellowsnort"
            };

            int index = Random.Shared.Next(AnimalNames.Length);
            return AnimalNames[index];
        }
        public static string RandomAnimalSpecies()
        {
            String[] AnimalSpecies =
            {
                "Badger","Stag","Hare","Boar","Fox","Deer","Squirrel",
                "Raccoon","Otter","Wolf","Elk","Mouse","Bear","Toad",
                "Crow","Finch","Snake","Mole","Hedgehog","Rabbit",

                "Turtle","Seal","Crab","Fish","Gull","Eel","Penguin",
                "Hermit Crab","Dolphin","Ray","Walrus","Minnow","Lobster",
                "Seahorse","Frog","Catfish","Pelican","Clam","Octopus","Shrimp",

                "Jackal","Lizard","Tortoise","Coyote","Hawk","Viper","Goat",
                "Scorpion","Beetle","Crane","Hyena","Iguana","Falcon",
                "Vulture","Camel","Gecko","Armadillo","Roadrunner","Locust","Mongoose",

                "Lynx","Caribou","Ram","Owl","Polar Bear","Seal","Marmot",
                "Moose","Raven","Penguin","Mink","Ferret","Elk","Snow Fox",
                "Mountain Goat","Yak","Weasel","Husky","Ptarmigan","Ermine",

                "Rat","Ferret","Beetle","Sparrow","Mouse","Terrier","Turtle",
                "Cat","Raccoon","Moth","Mule","Cricket","Badger","Weasel",
                "Crow","Hound","Goat","Pig","Pigeon","Hedgehog"
            };

            int index = Random.Shared.Next(AnimalSpecies.Length);
            return AnimalSpecies[index];
        }

        //recipe list for AnimalNpc's
        private static readonly Func<Animal>[] _tier1Animal =
        {
            () => new Animal(RandomAnimalName(), Random.Shared.Next(5, 7), RandomAnimalSpecies(), true)
        };

        public static Animal GetStandardTier(int tier = 1)
        {
            int index = Random.Shared.Next(_tier1Animal.Length);
            return _tier1Animal[index]();
        }

        //all animals have eyes, may implement a chance for blind animals in the future, but for now they all have eyes so this method will always return true.
        private static bool RollHasEyes(int chanceEyes)
        {
            return Random.Shared.Next(100) < chanceEyes;
        }

    }
}

