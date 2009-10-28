using System;
using System.Collections.Generic;
using System.Text;

namespace Classes.Combat
{
    public class CombatantMap // Struct_1C9CD
    {
        public Point pos;
        public int size;

        public Point screenPos;

        public override string ToString()
        {
            return string.Format("size: {0} pos: {1},{2}", size, pos.x, pos.y);
        }
    }

}
