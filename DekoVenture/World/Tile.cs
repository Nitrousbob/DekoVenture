namespace DekoVenture
{
    public class Tile
    {
        public int X {get; set;}
        public int Y {get; set;}
        public string TerrainType{get;set;}

        //if there is a cave,cabin,or shop, then attach the point of interest
        //entering the location goes into the point of interest

        public Location? PointOfInterest {get;set;}

        //the default physical space of the tile if the player is just wandering
        //this ensures dropped items stay on this specific tile
        public Location EmptyGround {get; private set;}

        public Tile(int x, int y, string terrainType)
        {
            X = x;
            Y = y;
            TerrainType = terrainType;
            EmptyGround = new Location(TerrainType, $"You are wandering the {TerrainType.ToLower()}.");
        }
    }
}