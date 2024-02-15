using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace DungeonCrawler
{
    public enum ProcessState
    {
        Inactive,
        Hesitate,
        Attack,
        Chase
    }

    public enum Command
    {
        SeePlayer,
        NearPlayerUnderwhelmed,
        NearPlayerOverwhelmed
    }

    internal class Enemy : Entity
    {
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
        Dictionary<StateTransition, ProcessState> transitions;
        public ProcessState CurrentState { get; private set; }


        public Enemy() : base(Map.TileType.ENEMY)
        {
            // Create states and current state
            CurrentState = ProcessState.Inactive;
            transitions = new Dictionary<StateTransition, ProcessState>
            {
                { new StateTransition(ProcessState.Inactive, Command.SeePlayer), ProcessState.Chase},
                { new StateTransition(ProcessState.Chase, Command.NearPlayerUnderwhelmed), ProcessState.Attack },
                { new StateTransition(ProcessState.Chase, Command.NearPlayerOverwhelmed), ProcessState.Hesitate },
                { new StateTransition(ProcessState.Hesitate, Command.NearPlayerUnderwhelmed), ProcessState.Attack },
            };

            sprite = '^';
            spriteColour = ConsoleColor.Red;
        }

        public void LoopAI()
        {
            switch (CurrentState)
            {
                case ProcessState.Inactive:
                if(Pathfinding.SightLineExists(currentTile, Application.player.currentTile))
                {

                }

                break;
            }

        }

    }
}
