using System;
using System.Collections.Generic;
using System.Text;

namespace Classes
{
    /// <summary>
    /// 0xBC in size. Import type 02
    /// </summary>
    public class HillsFarPlayer
    {
        public string field_4; // name char[?]
        public byte field_14; // str
        public byte field_15; // str 100
        public byte field_16; // int
        public byte field_17; // wis
        public byte field_18; // dex
        public byte field_19; // con
        public byte field_1A; // cha
        public byte field_1C; // Alignment
        public byte field_1D;
        public short field_1E; // Age
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
