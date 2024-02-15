using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace DungeonCrawler
{
    internal class Entity
    {

        public char sprite = '@';
        protected ConsoleColor spriteColour = ConsoleColor.White;
        public Map.TileType entityType;
        public Vector2 currentTile;

        public ConsoleColor SpriteColour
        {
            get { return spriteColour; }
            private set { spriteColour = value; }
        }

        public Entity(Map.TileType newType)
        {
            entityType = newType;
        }


    }
}
