using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace DungeonCrawler
{
    internal static class EntityManager
    {
        internal static Dictionary<Entity, Vector2> entityList = new Dictionary<Entity, Vector2>();

        internal static void CreateEntity(Map.TileType entityType, Vector2 position)
        {
            switch(entityType)
            {
                // create entity if applicable, add to entityList, tile setoccupant
                case Map.TileType.ENEMY:
                    Enemy e = new Enemy();
                    entityList.Add(e, position);
                    Application.CurrentMap.data[(int)position.X, (int)position.Y].SetOccupant(e);
                    break;
                case Map.TileType.PLAYER:
                    Application.player.x = (int)position.X;
                    Application.player.y = (int)position.Y;
                    entityList.Add(Application.player, new Vector2(Application.player.x, Application.player.y));
                    Application.CurrentMap.data[(int)position.X, (int)position.Y].SetOccupant(Application.player);
                    break;
                // add case for wall, empty

            }
        }
    }
}
