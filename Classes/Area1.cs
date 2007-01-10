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

        const int Area1Size = 0x3fe;

        public Area1(byte[] data, int offset)
        {
            DataIO.ReadObject(this, data, offset);

            origData = new byte[Area1Size];
            System.Array.Copy(data, offset, origData, 0, Area1Size);
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

        protected byte[] origData;

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

        [DataOffset(0x226, DataType.Word)]
        public ushort field_226;

        [DataOffset(0x342, DataType.Byte)]
        public byte field_342;
        [DataOffset(0x3FA, DataType.Byte)]
        public byte field_3FA;
        [DataOffset(0x3FE, DataType.SWord)]
        public short field_3FE;

        public void field_6A00_Set(int index, ushort value)
        {
            int loc = index & 0xFFFF;
            System.Console.WriteLine("     field_6A00_Set loc: {0,4:X} value: {1,4:X}", loc, value );

            /* ovr021:0482 */
            switch (loc)
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
                    //throw new NotImplementedException();
                    System.Console.WriteLine("       default access");
                    Sys.ShortToArray((short)value, origData, loc);
                    break;
            }
        }

        public ushort field_6A00_Get(int index)
        {
            int loc = index & 0xFFFF;
            System.Console.WriteLine("     field_6A00_Get loc: {0,4:X}", loc);
            
            /* ovr021:0482 */
            switch (loc)
            {
                case 0x192:
                    return field_192;

                case 0x1E4:
                    return field_1E4;

                case 0x200:
                case 0x202:
                case 0x204:
                case 0x206:
                case 0x208:
                case 0x20a:
                case 0x20c:
                case 0x20e:
                case 0x210:
                case 0x212:
                case 0x214:
                case 0x216:
                case 0x218:
                case 0x21a:
                case 0x21c:
                case 0x21e:
                    return (ushort)field_200[(loc - 0x200) / 2];


                case 0x226:
                    return field_226;

                default:
                    System.Console.WriteLine("       default access");
                    return Sys.ArrayToUshort(origData, loc);
                    //throw new NotImplementedException();
            }
        }

        public byte[] ToByteArray()
        {
            throw new Exception("The method or operation is not implemented.");
        }
    }
}
