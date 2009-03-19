using System;
using System.Collections.Generic;
using System.Text;

namespace Classes
{
    public class SubStruct_1A35E
    {
        public SubStruct_1A35E(short f0, byte f2, byte f3) 
        {
            base_age = f0; 
            dice_count = f2;
            dice_size = f3;
        }

        public short base_age;
        public byte dice_count;
        public byte dice_size;
    }

    public class Struct_1A35E
    {
        // 0x1C long.
        SubStruct_1A35E[] field_00;


        public Struct_1A35E()
        {
            field_00 = new SubStruct_1A35E[7];
        }

        public Struct_1A35E(SubStruct_1A35E[] values)
        {
            field_00 = values;
        }

        public SubStruct_1A35E this[int i]
        {
            get
            {
                return field_00[i];
            }
        }

        public SubStruct_1A35E this[ClassId i]
        {
            get
            {
                return field_00[(int)i];
            }
        }

        /*
        private short field_0;   // seg600:404E unk_1A35E
        private byte field_2;    // seg600:4050 unk_1A360
        private byte field_3;    // seg600:4051 unk_1A361
        private int field_4;     // not used.
        private short field_8;   // seg600:4056 unk_1A366
        private byte field_A;    // seg600:4058 unk_1A368
        private byte field_B;    // seg600:4059 unk_1A369
        private short field_14;  // seg600:4062 unk_1A372
        private byte field_16;   // seg600:4064 unk_1A374
        private byte field_17;   // seg600:4065 unk_1A375
        */
    }
}
