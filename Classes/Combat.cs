using System;
using System.Collections.Generic;
using System.Text;

namespace Classes
{


    public class SortedCombatant : IComparable<SortedCombatant>   /*Struct_1D1C1*/
    {
        public SortedCombatant(Player Player, Point Pos, int Steps, int Direction)
        {
            player = Player;
            pos = Pos;
            steps = Steps;
            direction = Direction;
        }

        public Player player;
        public Point pos;

        /// <summary>
        /// steps to counted in 2's, so that diagional steps can be 3 (thus 1.5)
        /// </summary>
        public int steps;
        int direction;

        public override string ToString()
        {
            return "index: " + player + " pos: " + pos + " dir: " + direction + " steps: " + steps;
        }

        int IComparable<SortedCombatant>.CompareTo(SortedCombatant other)
        {
            if (steps == other.steps)
            {
                if (direction == other.direction)
                {
                    return (direction % 2) - (other.direction % 2);
                }
                else
                {
                    return direction - other.direction;
                }
            }
            else
            {
                return steps - other.steps;
            }
        }
    }

}
