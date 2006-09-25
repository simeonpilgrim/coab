using System;

namespace Classes
{
    /// <summary>
    /// Summary description for Area2.
    /// </summary>
    public class Area2
    {
        public Area2()
        {
            //
            // TODO: Add constructor logic here
            //
            field_6F2 = new ushort[10];
        }

        public Area2(byte[] data, int offset)
        {
            throw new System.NotImplementedException();
        }

        public void Clear()
        {
            field_550 = 0; // 0x550
            field_580 = 0; // 0x580
            field_582 = 0; // 0x582
            field_58C = 0; // 0x58c
            field_58E = 0; // 0x58e
            field_590 = 0; // 0x590
            field_592 = 0; // 0x592
            field_594 = 0; // 0x594
            field_596 = 0; // 0x596
            field_5A4 = 0; // 0x5a4
            field_5A6 = 0; // 0x5a6
            field_5AA = 0; // 0x5aa
            field_5C2 = 0; // 0x5c2
            field_5C4 = 0; // 0x5c4
            field_5C6 = 0; // 0x5c6
            field_5CC = 0; // 0x5cc
            game_area = 0; // 0x624
            field_666 = 0; // 0x666
            field_67C = 0; // 0x67c
            field_67E = 0; // 0x67e
            field_6D8 = 0; // 0x6d8
            field_6DA = 0; // 0x6da
            field_6E0 = 0; // 0x6e0
            field_6E2 = 0; // 0x6e2
            field_6E4 = 0; // 0x6e4
            Array.Clear(field_6F2, 0, 10);

            field_799 = 0; // 0x799
            field_79A = 0; // 0x79a
            field_79B = 0; // 0x79b
            field_79C = 0; // 0x79c
            field_79D = 0; // 0x79d
            field_79E = 0; // 0x79e
            field_79F = 0; // 0x79f
            field_7A0 = 0; // 0x7a0
            field_7A1 = 0; // 0x7a1
            field_7A2 = 0; // 0x7a2
            field_7A3 = 0; // 0x7a3
            field_7A4 = 0; // 0x7a4
            field_7A5 = 0; // 0x7a5
            field_7A6 = 0; // 0x7a6
            field_7A7 = 0; // 0x7a7
            field_7A8 = 0; // 0x7a8
            field_7A9 = 0; // 0x7a9
            field_7AA = 0; // 0x7aa
            field_7AB = 0; // 0x7ab
        }

        public byte field_550; // 0x550
        public ushort field_580; // 0x580
        public ushort field_582; // 0x582
        public ushort field_58C; // 0x58c
        public short field_58E; // 0x58e
        public short field_590; // 0x590
        public short field_592; // 0x592
        public ushort field_594; // 0x594
        public short field_596; // 0x596
        public short field_5A4; // 0x5a4
        public short field_5A6; // 0x5a6
        public short field_5AA; // 0x5aa
        public short field_5C2; // 0x5c2
        public short field_5C4; // 0x5c4
        public short field_5C6; // 0x5c6
        public short field_5CC; // 0x5cc
        public byte game_area; // 0x624
        public short field_666; // 0x666
        public byte field_67C; // 0x67c
        public short field_67E; // 0x67e
        public short field_6D8; // 0x6d8
        public short field_6DA; // 0x6da
        public short field_6E0; // 0x6e0
        public short field_6E2; // 0x6e2
        public short field_6E4; // 0x6e4
        public ushort[] field_6F2; // 0x6f2 Word[9]

        public byte field_799; // 0x799
        public byte field_79A; // 0x79a
        public byte field_79B; // 0x79b
        public byte field_79C; // 0x79c
        public byte field_79D; // 0x79d
        public byte field_79E; // 0x79e
        public byte field_79F; // 0x79f
        public byte field_7A0; // 0x7a0
        public byte field_7A1; // 0x7a1
        public byte field_7A2; // 0x7a2
        public byte field_7A3; // 0x7a3
        public byte field_7A4; // 0x7a4
        public byte field_7A5; // 0x7a5
        public byte field_7A6; // 0x7a6
        public byte field_7A7; // 0x7a7
        public byte field_7A8; // 0x7a8
        public byte field_7A9; // 0x7a9
        public byte field_7AA; // 0x7aa
        public byte field_7AB; // 0x7ab

        public byte[] ToByteArray()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public ushort field_800_Get(int index)
        {
            int i = ((index & 0xFFFF) - 0x6F2) >> 1;

            return field_6F2[i];
        }

        public void field_800_Set(int index, ushort value)
        {
            switch( index & 0xFFFF )
            {
                case 0x58C:
                    field_58C = value;
                    break;

                default:
            int i = ((index & 0xFFFF) - 0x6F2) >> 1;

            field_6F2[i] = value;
                    break;
            }
        }
    }
}
