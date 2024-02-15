using System.Numerics;

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
            return Application.CurrentMap.data[(int)Math.Round(fracCoords.X), (int)Math.Round(fracCoords.Y)];
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
                if(tile.Occupant.entityType == Map.TileType.WALL)
                {
                    return false;
                }
            }
            return true;
        }

        static bool IsValidTile(Vector2 tile)
        {
            if(0 <= tile.X && tile.X <= Application.CurrentMap.data.GetLength(0)
            && 0 <= tile.Y && tile.Y <= Application.CurrentMap.data.GetLength(1)
            && Application.CurrentMap.data[(int)tile.X, (int)tile.Y].Occupant.entityType != Map.TileType.WALL){
                return true;
            }
            return false;
        }
        public static Vector2[] BFS(Vector2 origin)
        {
            List<Vector2> visited = new List<Vector2>();
            Queue<Vector2> q = new Queue<Vector2>();

            q.Enqueue(origin);
            visited.Add(origin);

            while(q.Count != 0)
            {
                Vector2 curTile = q.Peek();
                q.Dequeue();

                for (int i = 0; i < 4; i++)
                {
                    if(IsValidTile(curTile + deltas[i]) && !visited.Contains(curTile + deltas[i]))
                    {
                        q.Enqueue(curTile + deltas[i]);
                    }
                }
            }
            return q.ToArray();;
        }
    }
}