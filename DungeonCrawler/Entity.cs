using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonCrawler
{

    internal class Entity
    {

        public char sprite = '@';
        protected ConsoleColor spriteColour = ConsoleColor.White;

        public ConsoleColor SpriteColour
        {
            get { return spriteColour; }
            private set { spriteColour = value; }
        }

         
        public Entity()
        {


        }


    }
}
