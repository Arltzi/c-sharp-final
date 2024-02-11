using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonCrawler
{
    internal class Levels
    {
        static char[] level1 = { '#', '#', '#', '#', '#', '#', '#', '#', '#', '#',
                                        '#', ' ',' ',' ',' ',' ',' ',' ',' ','#',
                                         '#','#','#','#','#','#','#','#','#','#',};
        public static Entity[] level;
        public static void BuildLevels () 
        {
            level = new Entity[level1.Length];

            for (int x = 0; x < level.Length; x++) 
            {
                level[x].sprite = level1[x];
            }
        }
    }
}
