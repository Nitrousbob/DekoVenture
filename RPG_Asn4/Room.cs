namespace RPG_Asn4
{
    public class Room
    {
        public string Description { get; set; }
        List<Npc> Npcs { get; set; }
        //List<Item> Items { get; set; }
        Room? North { get; set; }
        Room? South { get; set; }
        Room? East { get; set; }
        Room? West { get; set; }
    

    public Room (string description)
    {
        Description = description;
        Npcs = new List<Npc>();
        //Items = new List<Item>();
    }

//  <!-- treasure
//  traps
//  npcs - enemies, other folks List<Npc>
//  items - interactable windows, plants desks portals
//  string description
//  exits
//  Player? player (nullable) -->
    }
}