using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Numerics;
using System.Runtime.Intrinsics;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace DungeonCrawler
{
    internal class EntityManager
    {
        // DECLARATIONS
        public List<Entity> entityList = new List<Entity>();
        Dictionary<int, ProcessState> transitions;
        const int PATHFINDING_FRAME_FREQUENCY = 3;

        public EntityManager()
        {
            transitions = new Dictionary<int, ProcessState>
            {
                {0, ProcessState.Chase },
                {1, ProcessState.Attack },
                {2, ProcessState.Hesitate },
                {3, ProcessState.Attack }
            };
        }

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
            Attack,
            Chase
        }

        internal enum Command
        {
            SeePlayer,
            NearPlayerUnderwhelmed,
            NearPlayerOverwhelmed
        }

        class StateTransition
        {
            public int id;
            public ProcessState CurrentState { get; private set; }
            public Command Command  { get; private set; }

            public override int GetHashCode()
            {
                return id;
            }

            public StateTransition(int newId)
            {
                id = newId;
                switch(id)
                {
                    case 0:
                    CurrentState = ProcessState.Inactive;
                    Command = Command.SeePlayer;
                    break;
                    case 1:
                    CurrentState = ProcessState.Chase;
                    Command = Command.NearPlayerOverwhelmed;
                    break;
                    case 2:
                    CurrentState = ProcessState.Chase;
                    Command = Command.NearPlayerUnderwhelmed;
                    break;
                    case 3:
                    CurrentState = ProcessState.Hesitate;
                    Command = Command.NearPlayerOverwhelmed;
                    break;
                }
            }
        }
        public ProcessState ChangeState(int stateID)
        {
            ProcessState nextState;
            nextState = transitions[stateID];
            return nextState;
        }
        public ProcessState CurrentState { get; set; }

        // iterate over entitylist, update state for each
        int frameCounter = PATHFINDING_FRAME_FREQUENCY;
        public void EnemyUpdate()
        {
            foreach (Enemy enemy in entityList)
            {
                switch (enemy.CurrentState)
                {
                    case ProcessState.Inactive:
                    if(Pathfinding.SightLineExists(new Vector2(enemy.x, enemy.y), new Vector2(Application.player.x, Application.player.y)))
                    {
                        enemy.CurrentState = ChangeState(0);
                        enemy.pathToPlayer = Pathfinding.GetPath(new Vector2(enemy.x, enemy.y), new Vector2(Application.player.x, Application.player.y));
                    }
                    break;

                    case ProcessState.Chase:
                        // frameCounter++;
                        // if(frameCounter == PATHFINDING_FRAME_FREQUENCY)
                        // {
                        //     frameCounter = 0;
                        // }
                        enemy.pathToPlayer = Pathfinding.GetPath(new Vector2(enemy.x, enemy.y), new Vector2(Application.player.x, Application.player.y));
                        switch(Vector2.Subtract(enemy.pathToPlayer.Last() , new Vector2(enemy.x, enemy.y)))
                        {
                            case Vector2 diff when diff.Equals(new Vector2(0, -1)): // enemy goes down
                            enemy.Move(Direction.DOWN);
                            break;
                            case Vector2 diff when diff.Equals(new Vector2(0, 1)): // enemy goes up
                            enemy.Move(Direction.UP);
                            break;
                            case Vector2 diff when diff.Equals(new Vector2(-1, 0)): // enemy goes left
                            enemy.Move(Direction.LEFT);
                            break;
                            case Vector2 diff when diff.Equals(new Vector2(1, 0)): // enemy goes right
                            enemy.Move(Direction.RIGHT);
                            break;
                        }
                    break;

                    case ProcessState.Attack:

                    break;
                }
            }

        }
    }
}
