using System;
using System.Collections.Generic;
using System.Text;

namespace Classes
{
    public class CombatPlayerIndex
    {
        public Player player;
        public int index;

        public CombatPlayerIndex(Player p, int i)
        {
            player = p;
            index = i;
        }
    }
}
