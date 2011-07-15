using System;
using System.Collections.Generic;
using System.Text;

namespace Classes
{
    public class HillsFarPlayer
    {
        public const int StructSize = 0xBC;

        public string name; // 0x04 name char[?]
        public byte stat_str; // 0x14 str
        public byte stat_str00; // 0x15 str 100
        public byte stat_int; // 0x16 int
        public byte stat_wis; // 0x17 wis
        public byte stat_dex; // 0x18 dex
        public byte stat_con; // 0x19 con
        public byte stat_cha; // 0x1A cha
        public byte alignment; // 0x1C Alignment
        public byte field_1D;
        public short age; // 0x1E Age
        public byte field_20; // HP current
        public byte field_21; // HP Max
        public byte field_23;
        public byte field_26;
        public int field_28; // money
        public byte field_2C; // Sex
        public byte field_2D; // Race
        public int field_2E; // Exp

        public byte field_35;

        public byte field_86;
        public byte field_87;

        public byte field_B7; // cleric skill
        public byte field_B8; // magic-usr skill
        public byte field_B9; // fighter skill
        public byte field_BA; // thief skill

        public HillsFarPlayer()
        {
        }

        public HillsFarPlayer(byte[] data)
        {
        }
    }
}
