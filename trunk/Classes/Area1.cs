using System;

namespace Classes
{
    /// <summary>
    /// Summary description for Area1.
    /// </summary>
    public class Area1
    {
        const int Area1Size = 0x800;
 
        public Area1()
        {
            constructorInit();
        }

        public Area1(byte[] data, int offset)
        {
            constructorInit();

            DataIO.ReadObject(this, data, offset);

            System.Array.Copy(data, offset, origData, 0, Area1Size);
        }

        private void constructorInit()
        {
            field_200 = new short[33];
            origData = new byte[Area1Size];
        }

        public void Clear()
        {
            System.Array.Clear(origData, 0, Area1Size);

            DataIO.ReadObject(this, origData, 0);
        }

        protected byte[] origData;

        [DataOffset(0xAE, DataType.Word)]
        public ushort field_AE;
        [DataOffset(0x10E, DataType.Word)]
        public ushort field_10E;
        [DataOffset(0x136, DataType.Word)]
        public ushort field_136;
        [DataOffset(0x16C, DataType.Word)]
        public ushort field_16C;

        [DataOffset(0x186, DataType.Byte)]
        public byte field_186;
        [DataOffset(0x188, DataType.Byte)]
        public byte field_188;
        [DataOffset(0x18A, DataType.Byte)]
        public byte field_18A;
        [DataOffset(0x18E, DataType.SWord)]
        public short time_minutes_ones; // field_18E
        [DataOffset(0x190, DataType.SWord)]
        public short time_minutes_tens; // field_190
        [DataOffset(0x192, DataType.Word)]
        public ushort time_hour; // field_192

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
        public short block_area_view; // field_1F6
        [DataOffset(0x1F8, DataType.Byte)]
        public byte game_speed;
        [DataOffset(0x1FA, DataType.Word)]
        public ushort outdoor_sky_colour;
        [DataOffset(0x1FC, DataType.Word)]
        public ushort indoor_sky_colour;
        [DataOffset(0x1FE, DataType.Byte)]
        public byte pics_on; //field_1FE;

        [DataOffset(0x1FF, DataType.Bool)]
        public bool can_cast_spells;

        [DataOffset(0x200, DataType.ShortArray,33)]
        public short[] field_200; // 1-32

        [DataOffset(0x342, DataType.Byte)]
        public byte field_342;
        [DataOffset(0x3E2, DataType.Word)]
        public ushort field_3E2;
        [DataOffset(0x3FA, DataType.Byte)]
        public byte field_3FA;
        [DataOffset(0x3FE, DataType.SWord)]
        public short field_3FE;

        public void field_6A00_Set(int index, ushort value)
        {
            int loc = index & 0xFFFF;
            //System.Console.WriteLine("     field_6A00_Set loc: {0,4:X} value: {1,4:X}", loc, value );

            /* ovr021:0482 */
            switch (loc)
            {
                case 0xAE:
                    field_AE = value; 
                    break;

                case 0x10E:
                    field_10E = value; 
                    break;

                case 0x136:
                    field_136 = value;
                    break;

                case 0x16C:
                    field_16C = value;
                    break;

                case 0x192:
                    time_hour = value;
                    break;

                case 0x1CC:
                    field_1CC = (short)value;
                    break;

                case 0x1CE:
                    field_1CE = (short)value;
                    break;

                case 0x1D0:
                    field_1D0 = (short)value;
                    break;

                case 0x1E4:
                    field_1E4 = value;
                    break;

                case 0x1F6:
                    block_area_view = (short)value;
                    break;

                case 0x1F8:
                    game_speed = (byte)value;
                    break;

                case 0x1FA:
                    outdoor_sky_colour = value;
                    break;

                case 0x1FC:
                    indoor_sky_colour = value;
                    break;

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
                case 0x220:
                case 0x222:
                case 0x224:
                case 0x226:
                case 0x228:
                case 0x22a:
                case 0x22c:
                case 0x22e:
                case 0x230:
                case 0x232:
                case 0x234:
                case 0x236:
                case 0x238:
                case 0x23a:
                case 0x23c:
                case 0x23e:
                case 0x240:
                    field_200[(loc - 0x200) / 2] = (short)value;
                    break;

                case 0x342:
                    field_342 = (byte)value;
                    break;

                case 0x3E2:
                    field_3E2 = value;
                    break;

                case 0x3fe:
                    field_3FE = (short)value;
                    break;

                default:
                    DataIO.SetObjectUShort(this, origData, loc, value);
                    break;
            }
        }

        public ushort field_6A00_Get(int index)
        {
            int loc = index & 0xFFFF;
            //System.Console.WriteLine("     field_6A00_Get loc: {0,4:X}", loc);
            
            /* ovr021:0482 */
            switch (loc)
            {
                case 0xAE:
                    return field_AE;
                case 0x10E:
                    return field_10E;
                case 0x136:
                    return field_136;
                case 0x16C:
                    return field_16C;

                case 0x192:
                    return time_hour;

                case 0x1E0:
                    return (ushort)field_1E0;

                case 0x1E2:
                    return (ushort)field_1E2;
                
                case 0x1CC:
                    return (ushort)field_1CC;

                case 0x1E4:
                    return field_1E4;

                case 0x1F8:
                    return (ushort)game_speed;

                case 0x1FA:
                    return outdoor_sky_colour;

                case 0x1FC:
                    return indoor_sky_colour;

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
                case 0x220:
                case 0x222:
                case 0x224:
                case 0x226:
                case 0x228:
                case 0x22a:
                case 0x22c:
                case 0x22e:
                case 0x230:
                case 0x232:
                case 0x234:
                case 0x236:
                case 0x238:
                case 0x23a:
                case 0x23c:
                case 0x23e:
                case 0x240:
                    return (ushort)field_200[(loc - 0x200) / 2];

                case 0x3E2:
                    return field_3E2;

                default:
                    return DataIO.GetObjectUShort(this, origData, loc);
            }
        }

        public byte[] ToByteArray()
        {
            DataIO.WriteObject(this, origData);

            return (byte[])origData.Clone();
        }
    }
}
