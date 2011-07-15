using System;
using System.Collections.Generic;
using System.Text;

namespace Classes
{
    public class PoolRadPlayer
    {
        public const int StructSize = 0x011D;

        public string name; // 0x0	16
        public byte stat_str; // 0x10
        public byte stat_int; // 0x11
        public byte stat_wis; // 0x12
        public byte stat_dex; // 0x13
        public byte stat_con; // 0x14
        public byte stat_cha; // 0x15
        public byte stat_str00; // 0x16
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
        public byte[] field_77; // 0x77 Array 8, 0x77 - 0x7E
        public byte field_83; // 0x83
        public byte field_84; // 0x84
        public byte field_85; // 0x85
        public byte field_86; // 0x86
        public byte field_87; // 0x87

        public byte[] field_96; // 0x96 Array 8 0x96 - 0x9D
        public byte sex; // 0x9E
        public byte field_9F; // 0x9F
        public byte field_A0; // 0xA0

        public byte field_A1; // 0xA1
        public byte field_A2; // 0xA2
        public byte field_A3; // 0xA3
        public byte field_A4; // 0xA4
        public byte field_A5; // 0xA5
        public byte field_A6; // 0xA6
        public byte field_A7; // 0xA7
        public byte field_A8; // 0xA8

        public byte field_A9; // 0xA9
        public byte field_AA; // 0xAA
        public byte field_AB; // 0xAB

        public int field_AC; //0xAC
        public byte field_B0; // 0xB0
        public byte field_B1; // 0xB1
        public byte[] field_B2; // 0xB2 - 3
        public byte[] field_B5; // 0xB5 - 3

        public short field_B8; // 0xB8

        public byte field_BA; // 0xBA
        public byte field_BB; // 0xBB
        public byte field_BC; // 0xBC
        public byte field_BD; // 0xBD
        public byte field_BE; // 0xBE
        public byte field_C0; // 0xC0

        public byte[] field_C1; // 0xC1
        public byte field_C7;// 0xC7

        public byte field_100; // 0x100
        public byte field_101; // 0x101
        public short field_102; // 0x102

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


        public PoolRadPlayer()
        {
            field_33 = new byte[0x38];
            field_6D = new byte[5];
            field_77 = new byte[8];
            field_96 = new byte[8];
            field_B2 = new byte[3];
            field_B5 = new byte[3];
            field_C1 = new byte[6];
        }

        public PoolRadPlayer(byte[] data)
            : this()
        {
            name = Sys.ArrayToString(data, 0, 16);

            stat_str = data[0x10]; // 0x10
            stat_int = data[0x11]; // 0x11
            stat_wis = data[0x12]; // 0x12
            stat_dex = data[0x13]; // 0x13
            stat_con = data[0x14]; // 0x14
            stat_cha = data[0x15]; // 0x15
            stat_str00 = data[0x16]; // 0x16
            thac0 = (sbyte)data[0x2D];
            race = data[0x2e];
            _class = data[0x2F];
            age = Sys.ArrayToShort(data, 0x30);
            hp_max = data[0x32];

            System.Array.Copy(data, 0x33, field_33, 0, 0x38);

            field_6B = data[0x6B];
            field_6C = data[0x6C];
            System.Array.Copy(data, 0x6D, field_6D, 0, 5);
            field_72 = data[0x72];
            field_73 = data[0x73];
            field_74 = data[0x74];
            field_75 = data[0x75];
            field_76 = data[0x76];
            System.Array.Copy(data, 0x77, field_77, 0, 8);
            field_83 = data[0x83];
            field_84 = data[0x84];
            field_85 = data[0x85];
            field_86 = data[0x86];
            field_87 = data[0x87];
            System.Array.Copy(data, 0x96, field_96, 0, 8);

            sex = data[0x9E];
            field_9F = data[0x9F];
            field_A0 = data[0xA0];

            field_A1 = data[0xA1];
            field_A2 = data[0xA2];
            field_A3 = data[0xA3];
            field_A4 = data[0xA4];
            field_A5 = data[0xA5];
            field_A6 = data[0xA6];
            field_A7 = data[0xA7];
            field_A8 = data[0xA8];

            field_A9 = data[0xA9];
            field_AA = data[0xAA];
            field_AB = data[0xAB];

            field_AC = Sys.ArrayToInt(data, 0xAC);
            field_B0 = data[0xB0];
            field_B1 = data[0xB1];
            System.Array.Copy(data, 0xB2, field_B2, 0, 3);
            System.Array.Copy(data, 0xB5, field_B5, 0, 3);

            field_B8 = Sys.ArrayToShort(data, 0xb8);

            field_BA = data[0xBA];
            field_BB = data[0xBB];
            field_BC = data[0xBC];
            field_BD = data[0xBD];
            field_BE = data[0xBE];
            field_C0 = data[0xC0];
            System.Array.Copy(data, 0xC1, field_C1, 0, 6);

            field_C7 = data[0xC7];

            field_100 = data[0x100];
            field_101 = data[0x101];
            field_102 = Sys.ArrayToShort(data, 0x102);

            field_10C = data[0x10C];
            field_10D = data[0x10D];
            field_10E = data[0x10E];
            field_110 = (sbyte)data[0x110];

            field_111 = data[0x111];
            field_112 = data[0x112];
            field_113 = data[0x113];
            field_114 = data[0x114];
            field_115 = data[0x115];
            field_116 = data[0x116];
            field_117 = data[0x117];
            field_118 = data[0x118];
            field_119 = data[0x119];
            field_11A = data[0x11a];
            field_11B = data[0x11b];
            field_11C = (sbyte)data[0x11c];
        }
    }
}
