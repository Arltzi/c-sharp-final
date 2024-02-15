using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonCrawler
{
    internal class EntityManager
    {
        public List<Entity> entityList = new List<Entity>();
        

        public EntityManager()
        {
            entityList.Add(Application.player);
        }

        public Enemy CreateEnemy()
        {
            Enemy e = new Enemy();
            entityList.Add(e);
            return e;
        }

    }
}
