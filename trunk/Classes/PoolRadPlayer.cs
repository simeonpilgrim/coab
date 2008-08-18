using System;
using System.Collections.Generic;
using System.Text;

namespace Classes
{
    public class PoolRadPlayer
    {
        public string name; // 0x0	16
        public byte[] strength; // 0x10
        public byte strength_100; // 0x16
        public sbyte field_2D; // 0x2D
        public byte race; // 0x2e
        public byte _class; // 0x2F
        public short age; // 0x30
        public byte hp_max; // 0x33
        public byte sex; // 0x9e
        public byte[] field_C1; // 0xC1
        public byte field_111; // 0x111
        public byte field_112; // 0x112
        public byte field_113; // 0x113
        public byte field_114; // 0x114
        public byte field_115; // 0x115
        public byte field_116; // 0x116
        public byte field_117; // 0x117
        public byte field_118; // 0x118
        public byte field_119; // 0x119
        public byte field_11A; // 0x11a
        public byte field_11B; // 0x11b
        public sbyte field_11C; // 0x11c

        

        public PoolRadPlayer()
        {
            field_C1 = new byte[6];
        }

        public PoolRadPlayer(byte[] data)
        {
        }
    }
}
