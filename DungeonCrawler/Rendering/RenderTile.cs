using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonCrawler.Rendering
{
    internal class RenderTile
    {
        public char sprite;
        
        // TODO add color


        public RenderTile() 
        {
        
        }

        public RenderTile(char newSprite) 
        {
            sprite = newSprite;
        }
    }
}
