using System;
using System.Collections.Generic;
using System.Text;

namespace Classes.Combat
{
    public class CombatPlayerIndex
    {
        public Player player; // { get; protected set; }
        public Point pos;//{ get; protected set; }

        public CombatPlayerIndex(Player Player, Point Pos)
        {
            player = Player;
            pos = Pos;
        }

        public override string ToString()
        {
            return string.Format("{0} pos: {1}", player, pos);
        }
    }
}
