using System.Data;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;

namespace DungeonCrawler
{
    static class Pathfinding
    {
        static Vector2[] deltas =
        {
            // up right down left
            new Vector2(0, -1),
            new Vector2(1, 0),
            new Vector2(0, 1),
            new Vector2(-1, 0)
        };

        public static float FloatLerp(float a, float b, float t)
        {
            return a + (b - a) * t;
        }

        public static Vector2 TileRound(Vector2 fracCoords)
        {
            // Application.CurrentMap.Data[(int)Math.Round(fracCoords.X), (int)Math.Round(fracCoords.Y)].effectColour = ConsoleColor.Magenta;
            return new Vector2((int)Math.Round(fracCoords.X), (int)Math.Round(fracCoords.Y));
        }

        public static int Distance(Vector2 origin, Vector2 target)
        {
            return Math.Max((int)Math.Abs(origin.X - target.X), (int)Math.Abs(origin.Y - target.Y));
        }

        public static bool SightLineExists(Vector2 origin, Vector2 target)
        {
            int distance = Distance(origin, target);
            for (int i = 0; i <= distance; i++)
            {
                Vector2 tileCoord = TileRound(new Vector2(
                    FloatLerp(origin.X, target.X, (float)1.0/distance * i),
                    FloatLerp(origin.Y, target.Y, (float)1.0/distance * i)));
                // what a fucking line
                // Application.CurrentMap.Data[(int)tileCoord.X, (int)tileCoord.Y].effectColour = ConsoleColor.Magenta;
                if(Application.CurrentMap.Data[(int)tileCoord.X, (int)tileCoord.Y].Occupant != null)
                {
                    if(Application.CurrentMap.Data[(int)tileCoord.X, (int)tileCoord.Y].Occupant.GetType() == typeof(Wall))
                    {
                        return false;
                    };
                }
            }
            return true;
        }

        static bool IsValidTile(Vector2 tile)
        {
            if(
                0 <= tile.X && tile.X <= Application.CurrentMap.Data.GetLength(0) - 1
                && 0 <= tile.Y && tile.Y <= Application.CurrentMap.Data.GetLength(1) - 1
                && (Application.CurrentMap.Data[(int)tile.X, (int)tile.Y].Occupant == null
                || Application.CurrentMap.Data[(int)tile.X, (int)tile.Y].Occupant.GetType() != typeof(Wall))
            )
            {
                return true;
            }
            return false;
        }

        public static Vector2?[] GetNeighbours(Vector2 tile)
        {
            Vector2?[] neighbours = new Vector2?[4];
            for (int i = 0; i < neighbours.Length; i++)
            {
                if(IsValidTile(tile + deltas[i]))
                {
                    neighbours[i] = tile + deltas[i];
                }
                else
                {
                    neighbours[i] = null;
                }
            }
            return neighbours;
        }

        public static List<Vector2> GetPath(Vector2 origin, Vector2 target)
        {
            bool done = false;

            Dictionary<Vector2, Vector2?> checkedTiles = new Dictionary<Vector2, Vector2?>{ { origin, null } };
            List<Vector2> path = new List<Vector2>();

            while(!done)
            {
                Dictionary<Vector2, Vector2?> newTiles = new Dictionary<Vector2, Vector2?>();

                foreach (KeyValuePair<Vector2, Vector2?> checkTile in checkedTiles)
                {
                    foreach (Vector2? neighbour in GetNeighbours(checkTile.Key))
                    {
                        if(neighbour != null)
                        {
                            if(!checkedTiles.ContainsKey((Vector2)neighbour) && !newTiles.ContainsKey((Vector2)neighbour))
                            {
                                newTiles.Add((Vector2)neighbour, checkTile.Key);
                            }
                        }
                    }
                }

                foreach (KeyValuePair<Vector2, Vector2?> newTile in newTiles)
                {
                    checkedTiles.Add(newTile.Key, newTile.Value);
                    if(newTile.Key == target)
                    {
                        done = true;
                        break;
                    }
                }

                if(!done && newTiles.Count == 0)
                {
                    return new List<Vector2>(0);
                }
            }

            Vector2 tile = checkedTiles.Last().Key;
            while(checkedTiles[tile] != null){
                path.Add(tile);
                // Application.CurrentMap.Data[(int)tile.X, (int)tile.Y].effectColour = ConsoleColor.Magenta;
                tile = (Vector2)checkedTiles[tile];
            }
            return path;
        }
    }
}