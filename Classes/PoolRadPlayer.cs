using System;
using System.Collections.Generic;
using System.Text;

namespace Classes
{
    public class PoolRadPlayer
    {
        public const int StructSize = 0x011D;

        public string name; // 0x0	16
        public byte[] strength; // 0x10
        public byte strength_100; // 0x16
        public sbyte thac0; // 0x2D
        public byte race; // 0x2e
        public byte _class; // 0x2F
        public short age; // 0x30
        public byte hp_max; // 0x32
        public byte[] field_33; // 0x33 Array 0x38, 0x33 - 0x6A
        public byte field_6B; // 0x6B
        public byte field_6C; // 0x6C
        public byte[] field_6D; // 0x6D Array 5, 0x6D - 0x71
        public byte field_72; // 0x72
        public byte field_73; // 0x73
        public byte field_74; // 0x74
        public byte field_75; // 0x75
        public byte field_76; // 0x76

        public byte sex; // 0x9e
        public byte[] field_C1; // 0xC1
        public byte field_C7;// 0xC7

        public byte field_100; // 0x100
        public byte field_101; // 0x101

        public byte field_10C; // 0x10C
        public byte field_10D; // 0x10D
        public byte field_10E; // 0x10E
        public sbyte field_110; // 0x110

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

        public short field_1C0; // 0x1C0

        public PoolRadPlayer()
        {
            field_33 = new byte[0x38];
            field_C1 = new byte[6];

        }

        public PoolRadPlayer(byte[] data)
        {
        }
    }
}
