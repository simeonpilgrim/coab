using System;
using System.Collections.Generic;
using System.Text;

namespace Classes
{
    public class PoolRadPlayer
    {
        public string name; // 0x0	16
        public byte[] strength; // 0x10
        public byte strength_100; // 0x16
        public byte race; // 0x2e
        public byte sex; // 0x9e
        public byte[] field_C1; // 0xC1
        public sbyte field_11C; // 0x11c

        

        public PoolRadPlayer()
        {
            field_C1 = new byte[6];
        }

        public PoolRadPlayer(byte[] data)
        {
        }
    }
}
