using System;

namespace Classes
{
	/// <summary>
	/// Summary description for Struct_1D183.
	/// </summary>
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
            field_6 = 0;
		}

        public Player target; // unk_1D17C field_0
        public Point map; // unk_1D180 field_4 was sbyte
        public byte field_6; // unk_1D182
	}
}
