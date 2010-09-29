using System;

namespace Classes
{
    /// <summary>
    /// Summary description for Area2.
    /// </summary>
    public class Area2
    {
        const int Area2Size = 0x800;

        public Area2()
        {
        }

        public Area2(byte[] data, int offset)
        {
            DataIO.ReadObject(this, data, offset);

            System.Array.Copy(data, offset, origData, 0, Area2Size);
        }

        public void Clear()
        {
            System.Array.Clear(origData, 0, Area2Size);

            DataIO.ReadObject(this, origData, 0);
        }

        protected byte[] origData = new byte[Area2Size];

        [DataOffset(0x218, DataType.Word)]
        public ushort field_218; // 0x218

        [DataOffset(0x550, DataType.Byte)]
        public byte training_class_mask; // 0x550
        [DataOffset(0x580, DataType.Word)]
        public ushort max_encounter_distance; // 0x580
        [DataOffset(0x582, DataType.Word)]
        public ushort encounter_distance; // 0x582
        [DataOffset(0x58C, DataType.Word)]
        public ushort field_58C; // 0x58c
        [DataOffset(0x58E, DataType.SWord)]
        public short field_58E; // 0x58e
        [DataOffset(0x590, DataType.SWord)]
        public short field_590; // 0x590
        [DataOffset(0x592, DataType.SWord)]
        public short field_592; // 0x592
        [DataOffset(0x594, DataType.Word)]
        public ushort search_flags; // 0x594 - field_594 bit flags 1 - searching, 2 - looking
        [DataOffset(0x596, DataType.SWord)]
        public short field_596; // 0x596
        [DataOffset(0x5A4, DataType.SWord)]
        public short rest_incounter_period; // 0x5a4
        [DataOffset(0x5A6, DataType.SWord)]
        public short rest_incounter_percentage; // 0x5a6
        [DataOffset(0x5AA, DataType.Bool)]
        public bool tried_to_exit_map; // 0x5aa
        [DataOffset(0x5C2, DataType.Byte)]
        public byte HeadBlockId; // 0x5c2
        [DataOffset(0x5C4, DataType.Word)]
        public ushort EnterTemple; // 0x5c4
        [DataOffset(0x5C6, DataType.SWord)]
        public short field_5C6; // 0x5c6
        //[DataOffset(0x5CC, DataType.SWord)]
        public bool isDuel; // 0x5cc field_5CC
        [DataOffset(0x624, DataType.Byte)]
        public byte game_area; // 0x624
        [DataOffset(0x666, DataType.SWord)]
        public short field_666; // 0x666
        [DataOffset(0x67C, DataType.Byte)]
        public byte party_size; // 0x67c field_67C
        [DataOffset(0x67E, DataType.SWord)]
        public short field_67E; // 0x67e
        [DataOffset(0x6D8, DataType.Word)]
        public ushort EnterShop; // 0x6d8
        [DataOffset(0x6DA, DataType.SWord)]
        public short field_6DA; // 0x6da
        [DataOffset(0x6E0, DataType.SWord)]
        public short field_6E0; // 0x6e0
        [DataOffset(0x6E2, DataType.SWord)]
        public short field_6E2; // 0x6e2
        [DataOffset(0x6E4, DataType.SWord)]
        public short field_6E4; // 0x6e4

        [DataOffset(0x6F2, DataType.Word)]
        public ushort field_6F2;  // 0x6F2
        [DataOffset(0x6F4, DataType.Word)]
        public ushort field_6F4;  // 0x6F4
        [DataOffset(0x6F6, DataType.Word)]
        public ushort field_6F6;  // 0x6F6
        [DataOffset(0x6F8, DataType.Word)]
        public ushort field_6F8;  // 0x6F8
        [DataOffset(0x6FA, DataType.Word)]
        public ushort field_6FA;  // 0x6FA
        [DataOffset(0x6FC, DataType.Word)]
        public ushort field_6FC;  // 0x6FC
        [DataOffset(0x6FE, DataType.Word)]
        public ushort field_6FE;  // 0x6FE
        [DataOffset(0x700, DataType.Word)]
        public ushort field_700;  // 0x700
        [DataOffset(0x702, DataType.Word)]
        public ushort field_702;  // 0x702
        [DataOffset(0x704, DataType.Word)]
        public ushort field_704;  // 0x704

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
        [DataOffset(0x7EC, DataType.Word)]
        public ushort field_7EC;

        public byte[] ToByteArray()
        {
            byte[] data = new byte[0x800];
            DataIO.WriteObject(this, data);
            return data;
        }

        public ushort field_800_Get(int index)
        {
            int loc = index & 0xFFFF;
            //System.Console.WriteLine("     field_800_Get loc: {0,4:X}", loc);

            switch (loc)
            {
                case 0x550: return training_class_mask;
                case 0x58e: return (ushort)field_58E;
                case 0x592: return (ushort)field_592;
                case 0x594: return search_flags;
                case 0x5aa: return tried_to_exit_map ? (ushort)1 : (ushort)0;
                case 0x67e: return (ushort)field_67E;

                case 0x6F2: return field_6F2;
                case 0x6F4: return field_6F4;
                case 0x6F6: return field_6F6;
                case 0x6F8: return field_6F8;
                case 0x6FA: return field_6FA;
                case 0x6FC: return field_6FC;
                case 0x6FE: return field_6FE;
                case 0x700: return field_700;
                case 0x702: return field_702;
                case 0x704: return field_704;

                default:
                    return DataIO.GetObjectUShort(this, origData, loc);
            }
        }

        public void field_800_Set(int index, ushort value)
        {
            int loc = index & 0xFFFF;
            //System.Console.WriteLine("     field_800_Set loc: {0,4:X} value: {1,4:X}", loc, value);

            switch (loc)
            {
                case 0x550:
                    training_class_mask = (byte)value;
                    break;

                case 0x580:
                    max_encounter_distance = value;
                    break;

                case 0x58C:
                    field_58C = value;
                    break;

                case 0x592:
                    field_592 = (short)value;
                    break;

                case 0x596:
                    field_596 = (short)value;
                    break;

                case 0x5a4:
                    rest_incounter_period = (short)value;
                    break;

                case 0x5a6:
                    rest_incounter_percentage = (short)value;
                    break;

                case 0x5c2:
                    HeadBlockId = (byte)value;
                    break;
                case 0x5c4:
                    EnterTemple = value;
                    break;
                case 0x5c6:
                    field_5C6 = (short)value;
                    break;

                case 0x5cc:
                    isDuel = value != 0;
                    break;

                case 0x624:
                    game_area = (byte)value;
                    break;

                case 0x6d8:
                    EnterShop = value;
                    break;

                case 0x6da:
                    field_6DA = (short)value;
                    break;

                case 0x6e0:
                    field_6E0 = (short)value;
                    break;

                case 0x6e2:
                    field_6E2 = (short)value;
                    break;

                case 0x6e4:
                    field_6E4 = (short)value;
                    break;

                case 0x6F2:
                    field_6F2 = value;
                    break;

                case 0x6F4:
                    field_6F4 = value;
                    break;

                case 0x6F6:
                    field_6F6 = value;
                    break;

                case 0x6F8:
                    field_6F8 = value;
                    break;

                case 0x6FA:
                    field_6FA = value;
                    break;

                case 0x6FC:
                    field_6FC = value;
                    break;

                case 0x6FE:
                    field_6FE = value;
                    break;

                case 0x700:
                    field_700 = value;
                    break;

                case 0x702:
                    field_702 = value;
                    break;

                case 0x704:
                    field_704 = value;
                    break;

                case 0x7ec:
                    field_7EC = value;
                    break;


                default:
                    DataIO.SetObjectUShort(this, origData, loc, value);
                    break;
            }
        }

        public void RestField6F2Values()
        {
            field_6F2 = 0;
            field_6F4 = 0;
            field_6F6 = 0;
            field_6F8 = 0;
            field_6FA = 0;
            field_6FC = 0;
            field_6FE = 0;
            field_700 = 0;
            field_702 = 0;
            field_704 = 0;
        }
    }
}
