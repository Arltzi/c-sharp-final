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

        public static Tile TileRound(Vector2 fracCoords)
        {
            return Application.CurrentMap.Data[(int)Math.Round(fracCoords.X), (int)Math.Round(fracCoords.Y)];
        }

        public static float Distance(Vector2 origin, Vector2 target)
        {
            return (float)Math.Sqrt((target.X - origin.X)*(target.X - origin.X) + (target.Y - origin.Y)*(target.Y - origin.Y));
        }

        public static Tile[] DrawLine(Vector2 origin, Vector2 target)
        {
            float distance = Distance(origin, target);
            Tile[] results = new Tile[(int)distance + 1];
            for (int i = 0; i <= distance; i++)
            {
                // what a fucking line
                Tile lerpCoords = TileRound(new Vector2(FloatLerp(origin.X, target.X, (float)1.0/distance * i), FloatLerp(origin.Y, target.Y, (float)1.0/distance * i)));
                results.Append<Tile>(lerpCoords);
            }
            return results;
        }

        public static bool SightLineExists(Vector2 origin, Vector2 target)
        {
            Tile[] tiles = DrawLine(origin, target);
            foreach (Tile tile in tiles)
            {
                if(tile.Occupant.GetType() == typeof(Wall))
                {
                    return false;
                }
            }
            return true;
        }

        static bool IsValidTile(Vector2 tile)
        {
            if(
                0 <= tile.X && tile.X <= Application.CurrentMap.Data.GetLength(0)
                && 0 <= tile.Y && tile.Y <= Application.CurrentMap.Data.GetLength(1)
                && Application.CurrentMap.Data[(int)tile.X, (int)tile.Y].Occupant.GetType() != typeof(Wall)
                && Application.CurrentMap.Data[(int)tile.X, (int)tile.Y].Occupant.GetType() != typeof(Enemy)
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

        public static Vector2[]? GetPath(Vector2 origin, Vector2 target)
        {
            bool done = false;

            Dictionary<Vector2, Vector2?> checkedTiles = new Dictionary<Vector2, Vector2?>{ { origin, null } };
            List<Vector2> path = new List<Vector2>();

            while(!done)
            {
                Dictionary<Vector2, Vector2?> newTiles = new Dictionary<Vector2, Vector2?>();

                foreach (KeyValuePair<Vector2, Vector2?> checkTile in checkedTiles)
                {
                    foreach (Vector2 neighbour in GetNeighbours(checkTile.Key))
                    {
                        if(neighbour != null)
                        {
                            if(!checkedTiles.ContainsKey(neighbour) && !newTiles.ContainsKey(neighbour))
                            {
                                newTiles.Add(neighbour, checkTile.Key);
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
                        return null;
                    }

                    Vector2 tile = target;
                    while(checkedTiles[tile] != null){
                        path.Add(tile);
                        tile = (Vector2)checkedTiles[tile];
                        break;
                    }
                }
            }
        return path.ToArray();
        }
    }
}