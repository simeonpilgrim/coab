using System;

namespace Classes
{
	/// <summary>
	/// Summary description for DaxBlock.
	/// </summary>
	public class DaxBlock
	{
        /// <summary>0x00</summary>
        public short height; // 0x0;
        /// <summary>0x02</summary>
        public short width; // 0x2;
        public short field_4; // 0x4;
        public short field_6; // 0x6;
        /// <summary>0x08</summary>
        public byte item_count; // 0x8;
        public byte[] field_9; // 0x9; byte[8]
        /// <summary>0x11 Bytes Per Picture</summary>
        public short bpp; // 0x11;
        /// <summary>0x13</summary>
        public byte[] data_ptr; // 0x13;
        /// <summary>0x17</summary>
        public byte[] data; // 0x17;

		public DaxBlock(int dataSize )
		{
            data = new byte[dataSize];
            field_9 = new byte[8];
            data_ptr = null;
		}
	}
}
