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

        public DaxBlock(int masked, int _item_count, int _width, int _height)
        {
            height = _height;
            width = _width;
            bpp = height * width * 8;
            item_count = _item_count;
            int ram_size = item_count * bpp;

            data = new byte[ram_size];
            field_9 = new byte[8];
            data_ptr = null;

            //seg051.FillChar(0, ram_size, data); // maybe not needed?

            if ((masked & 1) != 0)
            {
                data_ptr = new byte[ram_size];
                //seg051.FillChar(0, ram_size, data_ptr); // maybe not needed?
            }
        }
    }
}
