using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Numerics;
using System.Runtime.Intrinsics;
using System.Text;
using System.Threading.Tasks;

namespace DungeonCrawler
{
    internal class EntityManager
    {
        // SPAWNING STUFF
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

        // AI STUFF
        // DECLARATIONS
        public List<Entity> entityList = new List<Entity>();
        Dictionary<StateTransition, ProcessState> transitions;
        const int PATHFINDING_FRAME_FREQUENCY = 3;
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
            public ProcessState CurrentState { get; private set; }
            public Command Command  { get; private set; }

            public StateTransition(ProcessState currentState, Command command)
            {
                CurrentState = currentState;
                Command = command;
            }
        }
        public ProcessState ChangeState(Command command)
        {
            StateTransition transition = new StateTransition(CurrentState, command);
            ProcessState nextState;
            transitions.TryGetValue(transition, out nextState);
            return nextState;
        }
        public ProcessState CurrentState { get; private set; }


        // iterate over entitylist, update state for each
        int frameCounter = PATHFINDING_FRAME_FREQUENCY;
        public void EnemyUpdate()
        {
            foreach (Enemy enemy in entityList)
            {
                switch (CurrentState)
                {
                    case ProcessState.Inactive:
                    if(Pathfinding.SightLineExists(new Vector2(enemy.x, enemy.y), new Vector2(Application.player.x, Application.player.y)))
                    {
                        ChangeState(Command.SeePlayer);
                        enemy.pathToPlayer = Pathfinding.GetPath(new Vector2(enemy.x, enemy.y), new Vector2(Application.player.x, Application.player.y));
                    }
                    break;

                    case ProcessState.Chase:
                        frameCounter++;
                        if(frameCounter == PATHFINDING_FRAME_FREQUENCY)
                        {
                            enemy.pathToPlayer = Pathfinding.GetPath(new Vector2(enemy.x, enemy.y), new Vector2(Application.player.x, Application.player.y));
                            frameCounter = 0;
                        }
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
