using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonCrawler
{
    internal struct Tile
    {

        public ConsoleColor effectColour = ConsoleColor.Black;
        public byte newEffectColor = 0;
        private Entity? occupant = null;

        public Entity? Occupant
        {
            get { return occupant; }
            private set { occupant = value; }
        }

        public Tile()
        {
            effectColour = 0;
        }

        public void SetOccupant(Entity e)
        {
            occupant = e;
        }

        public void EmptyTile()
        {
            occupant = null;
        }


    }
}
