using System;

namespace Classes
{
	public class DaxBlock
	{
        public int height; // 0x0;
        public int width; // 0x2;
        public int x_pos; // 0x4;
        public int y_pos; // 0x6;
        public int item_count; // 0x8;
        public byte[] field_9; // 0x9; byte[8]
        
        /// <summary>0x11 Bytes Per Picture</summary>
        public int bpp; // 0x11;
        public byte[] data_ptr; // 0x13;
        public byte[] data; // 0x17;

		public DaxBlock(int dataSize )
		{
            data = new byte[dataSize];
            field_9 = new byte[8];
            data_ptr = null;
		}
	}
}
