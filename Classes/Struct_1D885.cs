using System;

namespace Classes
{
    /// <summary>
    /// Summary description for Struct_1D885.
    /// </summary>
    public class Struct_1D885
    {
        public Struct_1D885()
        {
            player = null;
            next = null;
            // zero the rest.

            field_7 = new byte[10];
            field_10 = new byte[10];
        }

        /// <summary>0x00</summary>
        public Player player; // 0x00;
        /// <summary>0x04</summary>
        public Struct_1D885 next; // 0x04;
        /// <summary>
        /// 0x07 base-1 array
        /// </summary>
        public byte[] field_7; //0x07 base-1 array
        // 0x08
        // 0x09
        // 0x0A
        // 0x0B
        // 0x0C
        // 0x0D
        // 0x0E
        // 0x0F
        /// <summary>
        /// 0x10 base-1 array
        /// </summary>
        public byte[] field_10; // 0x10 base-1 array
        // 0x11
        // 0x12
        // 0x13
        // 0x14
        // 0x15
        // 0x16
        // 0x17
        // 0x18
        // 0x19
        public int target_x;// 0x1A - field_1A
        public int target_y;// 0x1B - field_1B
        public int field_1C;// 0x1C - field_1C
        public byte field_1D;// 0x1D
    }
}
