using System.Numerics;
using System.Reflection;


namespace DungeonCrawler
{
    internal class EntityManager
    {
        // DECLARATIONS
        public List<Entity> entityList = new List<Entity>();
        const int PATHFINDING_FRAME_FREQUENCY = 9;
        const int CHASE_FRAME_FREQUENCY = 3;
        const int HESITATE_FRAME_TIME = 10;

        public Enemy CreateEnemy()
        {
            Enemy e = new Enemy();
            entityList.Add(e);
            return e;
        }
        // AI STUFF
        // AI ENUMS
        internal enum ProcessState
        {
            Inactive,
            Hesitate,
            Chase
        }

        // iterate over entitylist, update state for each
        int chaseFrameCounter = CHASE_FRAME_FREQUENCY;
        int hesitationFrameCounter = 0;
        public void EnemyUpdate()
        {
            foreach (Enemy enemy in entityList)
            {
                switch (enemy.CurrentState)
                {
                    case ProcessState.Inactive:
                        if(Pathfinding.SightLineExists(new Vector2(enemy.x, enemy.y), new Vector2(Application.player.x, Application.player.y)))
                        {
                            enemy.CurrentState = ProcessState.Chase;
                            enemy.pathToPlayer = Pathfinding.GetPath(new Vector2(enemy.x, enemy.y), new Vector2(Application.player.x, Application.player.y));
                        }
                        break;

                    case ProcessState.Chase:
                        enemy.pathToPlayer = Pathfinding.GetPath(new Vector2(enemy.x, enemy.y), new Vector2(Application.player.x, Application.player.y));

                        if(Pathfinding.Distance(new Vector2(enemy.x, enemy.y), new Vector2(Application.player.x, Application.player.y)) == 1)
                        {
                            // Application.player.TakeDamage();
                            enemy.CurrentState = ProcessState.Hesitate;
                        }

                        if(chaseFrameCounter == CHASE_FRAME_FREQUENCY)
                        {
                            bool successMove = false;
                            switch(Vector2.Subtract(enemy.pathToPlayer.Last() , new Vector2(enemy.x, enemy.y)))
                            {
                                case Vector2 diff when diff.Equals(new Vector2(0, 1)): // enemy goes down
                                successMove = enemy.Move(Direction.DOWN);
                                break;
                                case Vector2 diff when diff.Equals(new Vector2(0, -1)): // enemy goes up
                                successMove = enemy.Move(Direction.UP);
                                break;
                                case Vector2 diff when diff.Equals(new Vector2(-1, 0)): // enemy goes left
                                successMove = enemy.Move(Direction.LEFT);
                                break;
                                case Vector2 diff when diff.Equals(new Vector2(1, 0)): // enemy goes right
                                successMove = enemy.Move(Direction.RIGHT);
                                break;
                            }

                            if(successMove)
                            {
                                enemy.pathToPlayer.RemoveAt(enemy.pathToPlayer.Count - 1);
                            }
                            chaseFrameCounter = 0;
                        }
                        chaseFrameCounter++;

                        break;

                    case ProcessState.Hesitate:
                        if(hesitationFrameCounter == HESITATE_FRAME_TIME)
                        {
                            enemy.CurrentState = ProcessState.Chase;
                            hesitationFrameCounter = 0;
                        }
                        hesitationFrameCounter++;
                        break;
                }
            }
        }
    }
}
