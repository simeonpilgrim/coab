using System;

namespace Classes
{
    /// <summary>
    /// Summary description for Struct_1A484.
    /// </summary>
    public class ClassStatsMin
    {
        public ClassStatsMin(byte v0, byte v1, byte v2, byte v3, byte v4, byte cha)
        {
            str_min = v0;
            int_min = v1;
            wis_min = v2;
            dex_min = v3;
            con_min = v4;
            cha_min = cha;
        }

        public byte str_min; // seg600:4174 unk_1A484
        public byte int_min; // seg600:4175 unk_1A485
        public byte wis_min; // seg600:4176 unk_1A486
        public byte dex_min; // seg600:4177 unk_1A487
        public byte con_min; // seg600:4178 unk_1A488
        public byte cha_min; // field_5 seg600:4179 unk_1A489

        public byte this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0:
                        return str_min;

                    case 1:
                        return int_min;

                    case 2:
                        return wis_min;

                    case 3:
                        return dex_min;

                    case 4:
                        return con_min;

                    case 5:
                        return cha_min;

                    default:
                        throw new System.ArgumentOutOfRangeException("index");
                }
            }
        }
    }
}
