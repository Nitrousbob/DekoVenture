namespace DekoVenture
{
    public class MapDisplay
    {
        //for displaying the map to the console
        
        public static void Show(Map map)
        {
            Zone zone = map.CurrentZone;
            UI.Narrate($"\n---Area Map---");
            int width = zone.MapGrid.GetLength(0);
            int height = zone.MapGrid.GetLength(1);

            for (int y = 0; y < height; y++)
            {
                string row = "";
                for (int x = 0; x < width; x++)
                {
                    //Note in the future we check map.ExploredTiles[x,y] here for fog of ware
                    if (x == zone.PlayerX && y == zone.PlayerY && zone.CurrentInterior == null)
                    {
                        row += Clr.ColorString(255,0,0, "[X]"); //the playa
                    }
                    else if (zone.MapGrid[x,y].PointOfInterest != null)
                    {
                        row += Clr.ColorString(0,255,0, "[*]"); //point of interest, building, cave etc.
                    }
                    else
                    {
                        row += "[]"; //empty wilderness/tiles
                    }
                }
            UI.Narrate(row);
            }
            UI.Narrate("-----------------\n");            
        }
    }
}