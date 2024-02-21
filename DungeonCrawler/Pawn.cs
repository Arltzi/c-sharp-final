using DungeonCrawler;
using DungeonCrawler.Attacks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonCrawler
{
    internal class Pawn : Entity
    {

        public int x, y;
        public Attack attack;

        protected int health;

        public int Health
        {
            get { return health; }
            private set { health = value; }
        }

        protected Direction direction;

        public Direction Dir { 
            get { return direction; }
            private set { direction = value; }
        }


        public Pawn()
        {
            health = 10;
            attack = new Slash(this);

        }

        public virtual void Die()
        {


        }

        virtual public void TakeDamage()
        {
            health--;
            if(health < 0)
            {
                Die();
            }
        }

        public void Attack()
        {
            attack.Action(Dir);
        }

        public bool Move(InputMap input)
        {
            int oldX = x;
            int oldY = y;

            direction = (Direction)input;

            switch (direction)
            {
                case Direction.UP:

                    sprite = '^';

                    if (CollisionCheck() == true)
                    {
                        y--;
                        Application.CurrentMap.Data[oldX, oldY].EmptyTile();
                        Application.CurrentMap.Data[x, y].SetOccupant(this);
                        return true;
                    }
                    break;

                case Direction.DOWN:

                    sprite = 'v';

                    if (CollisionCheck() == true)
                    {
                        y++;
                        Application.CurrentMap.Data[oldX, oldY].EmptyTile();
                        Application.CurrentMap.Data[x, y].SetOccupant(this);
                        return true;
                    }

                    break;

                case Direction.RIGHT:

                    sprite = '>';

                    if (CollisionCheck() == true)
                    {
                        x++;
                        Application.CurrentMap.Data[oldX, oldY].EmptyTile();
                        Application.CurrentMap.Data[x, y].SetOccupant(this);
                        return true;
                    }

                    break;

                case Direction.LEFT:

                    sprite = '<';

                    if (CollisionCheck() == true)
                    {
                        x--;
                        Application.CurrentMap.Data[oldX, oldY].EmptyTile();
                        Application.CurrentMap.Data[x, y].SetOccupant(this);
                        return true;
                    }
                    break;

            }

            return false;

        }

        public bool Move(Direction direction)
        {
            int oldX = x;
            int oldY = y;

            switch (direction)
            {
                case Direction.UP:

                    sprite = '^';

                    if (CollisionCheck() == true)
                    {
                        y--;
                        Application.CurrentMap.Data[oldX, oldY].EmptyTile();
                        Application.CurrentMap.Data[x, y].SetOccupant(this);
                        return true;
                    }
                    break;

                case Direction.DOWN:

                    sprite = 'v';

                    if (CollisionCheck() == true)
                    {
                        y++;
                        Application.CurrentMap.Data[oldX, oldY].EmptyTile();
                        Application.CurrentMap.Data[x, y].SetOccupant(this);
                        return true;
                    }

                    break;

                case Direction.RIGHT:

                    sprite = '>';

                    if (CollisionCheck() == true)
                    {
                        x++;
                        Application.CurrentMap.Data[oldX, oldY].EmptyTile();
                        Application.CurrentMap.Data[x, y].SetOccupant(this);
                        return true;
                    }

                    break;

                case Direction.LEFT:

                    sprite = '<';

                    if (CollisionCheck() == true)
                    {
                        x--;
                        Application.CurrentMap.Data[oldX, oldY].EmptyTile();
                        Application.CurrentMap.Data[x, y].SetOccupant(this);
                        return true;
                    }
                    break;

            }

            return false;

        }
        protected bool CollisionCheck()
        {

            switch (direction)
            {
                case Direction.UP:
                    if ((y - 1) == -1) // Top bounds check
                        return false;
                    else if (Application.CurrentMap.Data[x, y - 1].Occupant != null) // Wall tile collision check
                        return false;
                    break;

                case Direction.DOWN:
                    if ((y + 1) == Application.mapY) // Bottom bounds check (allowing player to go down in middle for doorway) (temp)

                        return false;
                    else if (Application.CurrentMap.Data[x, y + 1].Occupant != null) // Wall tile collision check
                        return false;
                    break;  
                case Direction.RIGHT:
                    if ((x + 1) == Application.mapX) // Right bounds check
                        return false;
                    else if (Application.CurrentMap.Data[x + 1, y].Occupant != null) // Wall tile collision check
                        return false;
                    break;
                case Direction.LEFT:
                    if ((x - 1) == -1) // Left bounds check
                        return false;
                    else if (Application.CurrentMap.Data[x - 1, y].Occupant != null) // Wall tile collision check
                        return false;
                    break;

            }


            return true;

        }


    }
}
