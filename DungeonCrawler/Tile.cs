using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonCrawler
{
    internal struct Tile
    {

        public ConsoleColor effectColour;
        private Entity? occupant = null;

        public Entity? Occupant
        {
            get { return occupant; }
            private set { occupant = value; }
        }

        public Tile()
        {
            effectColour = ConsoleColor.Black;
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
