using System;

namespace Classes
{
	public class Struct_1D183
	{
		public Struct_1D183()
        {
            Clear();
        }

        public void Clear()
		{
            target = null;
            map.x = 0;
            map.y = 0;
            originalBackgroundTile = 0;
		}

        public Player target; // unk_1D17C field_0
        public Point map; // unk_1D180 field_4 was sbyte
        public int originalBackgroundTile; // unk_1D182 field_6 was byte
	}
}
