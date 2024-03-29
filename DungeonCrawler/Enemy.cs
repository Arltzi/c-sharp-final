﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;

namespace DungeonCrawler
{
    internal class Enemy : Pawn
    {
        public List<Vector2> pathToPlayer;
        public EntityManager.ProcessState CurrentState;
        public Enemy()
        {
            CurrentState = EntityManager.ProcessState.Inactive;
            sprite = '^';
            spriteColour = ConsoleColor.Red;
            color = 0x0040;
            health = 5;
        }

        public override void Die()
        {
            Application.CurrentMap.Data[x, y].EmptyTile();
            Application.entityManager.entityList.Remove(this);
        }

    }
}
