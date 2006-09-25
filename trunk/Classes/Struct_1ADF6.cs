using System;
using System.Collections.Generic;
using System.Text;

namespace Classes
{
    public class Struct_1ADF6
    {
        // 0x17 or 23 long
        public ushort field_00;
        public ushort field_02;
        public short field_04; 
        public short field_06;
        public short field_08;
        public short field_0A;
        public short field_0C;
        public short field_0E;
        public byte field_10;
        public byte field_11;
        public byte field_12;
        public byte field_13;
        public byte field_14;
        public byte field_15;
        public byte field_16;

        public byte byteArray_11(int index)
        {
            switch (index)
            {
                case 0:
                    return field_11;
                case 1:
                    return field_12;
                case 2:
                    return field_13;
                case 3:
                    return field_14;
                case 4:
                    return field_15;
                case 5:
                    return field_16;
                default:
                    throw new ApplicationException();
            }
        }

    }
}
