using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 Alex Gulewich 
 Feb, 12, 2024
 Attack
 Creates a line of attack jutting straight out from the player
 */


namespace DungeonCrawler.Attacks
{
    internal class Stab : Attack
    {
        // shorthand constructor for enemies
        public Stab(Pawn parent) : base(parent)
        {
            // assigns in accordance to tile usage
            mAffectedTiles = new int[9,9];
        }



        public Stab(Pawn parent, ConsoleColor color) : base(parent, color)
        {
            // assigns in accordance to tile usage
            mAffectedTiles = new int[9,9];
        }



        public override void Action(Direction dir)
        {
            // Sets vars for later cleaning
            timer = 0;
            affectedtilesNeedClearing = true;

            // Adds the attack to a list for later cleaning
            AttackAutoClear.staticRef.attack = this;

            // Gets the direction to attack
            switch (dir)
            {
                case (Direction.UP):
                    DrawAttack(parent.x, parent.y, -1, 0);
                    break;

                case (Direction.DOWN):
                    DrawAttack(parent.x, parent.y, 1, 0);
                    break;

                case (Direction.LEFT):
                    DrawAttack(parent.x, parent.y, 0, -1);
                    break;

                case (Direction.RIGHT):
                    DrawAttack(parent.x, parent.y, 0, 1);
                    break;
            }

            // TODO implement color in attack
        }

        void DrawAttack(int x, int y, int verticleDir, int horizontalDir)
        {
            // declatations
            int xIter = 0;
            int yIter = 0;

            // draws attack
            for (int iter = 0; iter < 3; iter++)
            {
                // ensures we do not go out of bounds of map array
                if (Application.CurrentMap.Data.GetLength(0) - 1 < x + xIter || Application.CurrentMap.Data.GetLength(1) - 1 < y + yIter || x + xIter < 0 || y + yIter < 0)
                {
                    break;
                }

                // Assigns values to store
                mAffectedTiles[iter,0] = x + xIter;
                mAffectedTiles[iter, 1] = y + yIter;
                Application.CurrentMap.Data[x + xIter, y + yIter].effectColour = ConsoleColor.White;

                // iterates across the proper acess in proper direction
                xIter += horizontalDir;
                yIter += verticleDir;
            }

            // Prepare ntuff that needs clearing to be cleared
            affectedtilesNeedClearing = true;

            CheckForDamage();
        }
    }
}