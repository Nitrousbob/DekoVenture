namespace DekoVenture
{
    public class Map
    {
        //Holds the logic, layout, and tracking of explored areas.
        //keep data manipulation out of the UI layer.

        public Zone CurrentZone {get; private set;}
        //track the fog war: true if the player has seen this coordinate
        public bool[,] ExploredTiles {get; private set;}
                
        public Map(Zone zone)
        {
            CurrentZone = zone;
            int width = CurrentZone.MapGrid.GetLength(0);
            int height = CurrentZone.MapGrid.GetLength(1);
            ExploredTiles = new bool[width, height];
        }

        public void DiscoverTile(int x, int y)
        {
            if (x >= 0 && x < ExploredTiles.GetLength(0) && y >= 0 && y < ExploredTiles.GetLength(1))
            ExploredTiles[x,y] = true;
        }
    }
}