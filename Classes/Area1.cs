using System;

namespace Classes
{
    /// <summary>
    /// Summary description for Area1.
    /// </summary>
    public class Area1
    {
        public Area1()
        {
            field_200 = new short[33];
        }

        public Area1(byte[] data, int offset)
        {
            DataIO.ReadObject(this, data, offset);
        }

        public void Clear(byte value)
        {
            field_186  = value;
            field_188  = value;
            field_18A  = value;
            field_18E  = value;
            field_190  = value;
            field_192  = value;

            field_1CA  = value;
            field_1CC  = value;
            field_1CE  = value;
            field_1D0  = value;

            field_1E0  = value;
            field_1E2  = value;
            field_1E4  = value;
            field_1F6  = value;
            game_speed  = value;
            field_1FA  = value;
            field_1FC  = value;
            pics_on  = value; //field_1FE;

            can_cast_spells = false;

            Array.Clear(field_200, 1, 32);

            field_342  = value;
            field_3FA  = value;
            field_3FE  = value;

        }

        [DataOffset(0x186, DataType.Byte)]
        public byte field_186;
        [DataOffset(0x188, DataType.Byte)]
        public byte field_188;
        [DataOffset(0x18A, DataType.Byte)]
        public byte field_18A;
        [DataOffset(0x18E, DataType.SWord)]
        public short field_18E;
        [DataOffset(0x190, DataType.SWord)]
        public short field_190;
        [DataOffset(0x192, DataType.Word)]
        public ushort field_192;

        [DataOffset(0x1CA, DataType.SWord)]
        public short field_1CA;
        [DataOffset(0x1CC, DataType.SWord)]
        public short field_1CC;
        [DataOffset(0x1CE, DataType.SWord)]
        public short field_1CE;
        [DataOffset(0x1D0, DataType.SWord)]
        public short field_1D0;

        [DataOffset(0x1E0, DataType.SWord)]
        public short field_1E0;
        [DataOffset(0x1E2, DataType.SWord)]
        public short field_1E2;
        [DataOffset(0x1E4, DataType.Word)]
        public ushort field_1E4;
        [DataOffset(0x1F6, DataType.SWord)]
        public short field_1F6;
        [DataOffset(0x1F8, DataType.Byte)]
        public byte game_speed;
        [DataOffset(0x1FA, DataType.Word)]
        public ushort field_1FA;
        [DataOffset(0x1FC, DataType.Word)]
        public ushort field_1FC;
        [DataOffset(0x1FE, DataType.Byte)]
        public byte pics_on; //field_1FE;

        [DataOffset(0x1FF, DataType.Bool)]
        public bool can_cast_spells;

        [DataOffset(0x200, DataType.ShortArray,33)]
        public short[] field_200; // 1-32

        [DataOffset(0x342, DataType.Byte)]
        public byte field_342;
        [DataOffset(0x3FA, DataType.Byte)]
        public byte field_3FA;
        [DataOffset(0x3FE, DataType.SWord)]
        public short field_3FE;

        public void field_6A00_Set(int index, ushort value)
        {
            /* ovr021:0482 */
            switch (index & 0xFFFF)
            {
                case 0x1E4:
                    field_1E4 = value;
                    break;

                case 0x1FA:
                    field_1FA = value;
                    break;

                case 0x1FC:
                    field_1FC = value;
                    break;

                default:
                    throw new NotImplementedException();
                    break;
            }
        }

        public ushort field_6A00_Get(int index)
        {
            /* ovr021:0482 */
            switch (index & 0xFFFF)
            {
                case 0x192:
                    return field_192;

                case 0x1E4:
                    return field_1E4;

                case 0x206:
                    return (ushort)field_200[6];
                default:
                    throw new NotImplementedException();
            }
        }

        public byte[] ToByteArray()
        {
            throw new Exception("The method or operation is not implemented.");
        }
    }
}