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
            DataIO.ReadObject(this, data, offset);
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

        [DataOffset(0x550, DataType.Byte)]
        public byte field_550; // 0x550
        [DataOffset(0x580, DataType.Word)]
        public ushort field_580; // 0x580
        [DataOffset(0x582, DataType.Word)]
        public ushort field_582; // 0x582
        [DataOffset(0x58C, DataType.Word)]
        public ushort field_58C; // 0x58c
        [DataOffset(0x58E, DataType.SWord)]
        public short field_58E; // 0x58e
        [DataOffset(0x590, DataType.SWord)]
        public short field_590; // 0x590
        [DataOffset(0x592, DataType.SWord)]
        public short field_592; // 0x592
        [DataOffset(0x594, DataType.Word)]
        public ushort field_594; // 0x594
        [DataOffset(0x596, DataType.SWord)]
        public short field_596; // 0x596
        [DataOffset(0x5A4, DataType.SWord)]
        public short field_5A4; // 0x5a4
        [DataOffset(0x5A6, DataType.SWord)]
        public short field_5A6; // 0x5a6
        [DataOffset(0x5AA, DataType.SWord)]
        public short field_5AA; // 0x5aa
        [DataOffset(0x5C2, DataType.SWord)]
        public short field_5C2; // 0x5c2
        [DataOffset(0x5C4, DataType.SWord)]
        public short field_5C4; // 0x5c4
        [DataOffset(0x5C6, DataType.SWord)]
        public short field_5C6; // 0x5c6
        [DataOffset(0x5CC, DataType.SWord)]
        public short field_5CC; // 0x5cc
        [DataOffset(0x624, DataType.Byte)]
        public byte game_area; // 0x624
        [DataOffset(0x666, DataType.SWord)]
        public short field_666; // 0x666
        [DataOffset(0x67C, DataType.Byte)]
        public byte field_67C; // 0x67c
        [DataOffset(0x67E, DataType.SWord)]
        public short field_67E; // 0x67e
        [DataOffset(0x6D8, DataType.SWord)]
        public short field_6D8; // 0x6d8
        [DataOffset(0x6DA, DataType.SWord)]
        public short field_6DA; // 0x6da
        [DataOffset(0x6E0, DataType.SWord)]
        public short field_6E0; // 0x6e0
        [DataOffset(0x6E2, DataType.SWord)]
        public short field_6E2; // 0x6e2
        [DataOffset(0x6E4, DataType.SWord)]
        public short field_6E4; // 0x6e4

        [DataOffset(0x6F2, DataType.WordArray, 10)]
        public ushort[] field_6F2; // 0x6f2 Word[9]

        [DataOffset(0x799, DataType.Byte)]
        public byte field_799; // 0x799
        [DataOffset(0x79A, DataType.Byte)]
        public byte field_79A; // 0x79a
        [DataOffset(0x79B, DataType.Byte)]
        public byte field_79B; // 0x79b
        [DataOffset(0x79C, DataType.Byte)]
        public byte field_79C; // 0x79c
        [DataOffset(0x79D, DataType.Byte)]
        public byte field_79D; // 0x79d
        [DataOffset(0x79E, DataType.Byte)]
        public byte field_79E; // 0x79e
        [DataOffset(0x79F, DataType.Byte)]
        public byte field_79F; // 0x79f
        [DataOffset(0x7A0, DataType.Byte)]
        public byte field_7A0; // 0x7a0
        [DataOffset(0x7A1, DataType.Byte)]
        public byte field_7A1; // 0x7a1
        [DataOffset(0x7A2, DataType.Byte)]
        public byte field_7A2; // 0x7a2
        [DataOffset(0x7A3, DataType.Byte)]
        public byte field_7A3; // 0x7a3
        [DataOffset(0x7A4, DataType.Byte)]
        public byte field_7A4; // 0x7a4
        [DataOffset(0x7A5, DataType.Byte)]
        public byte field_7A5; // 0x7a5
        [DataOffset(0x7A6, DataType.Byte)]
        public byte field_7A6; // 0x7a6
        [DataOffset(0x7A7, DataType.Byte)]
        public byte field_7A7; // 0x7a7
        [DataOffset(0x7A8, DataType.Byte)]
        public byte field_7A8; // 0x7a8
        [DataOffset(0x7A9, DataType.Byte)]
        public byte field_7A9; // 0x7a9
        [DataOffset(0x7AA, DataType.Byte)]
        public byte field_7AA; // 0x7aa
        [DataOffset(0x7AB, DataType.Byte)]
        public byte field_7AB; // 0x7ab

        public byte[] ToByteArray()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public ushort field_800_Get(int index)
        {
            int loc = index & 0xFFFF;
            System.Console.WriteLine("     field_800_Get loc: {0,4:X}", loc);

            int i = (loc - 0x6F2) >> 1;

            if (loc == 0x67e) return (ushort)field_67E;

            return field_6F2[i];
        }

        public void field_800_Set(int index, ushort value)
        {
            int loc = index & 0xFFFF;
            System.Console.WriteLine("     field_800_Set loc: {0,4:X} value: {1,4:X}", loc, value);

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
