using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DaxDump
{
    class Sys
    {
        private Sys()
        {
        }

        public static int ArrayToInt(byte[] data, int offset)
        {
            int i = data[offset + 0] + (data[offset + 1] << 8) + (data[offset + 2] << 16) + (data[offset + 3] << 24);
            return i;
        }

        public static uint ArrayToUint(byte[] data, int offset)
        {
            uint i = (uint)(data[offset + 0] + (data[offset + 1] << 8) + (data[offset + 2] << 16) + (data[offset + 3] << 24));
            return i;
        }

        public static short ArrayToShort(byte[] data, int offset)
        {
            short i = (short)(data[offset + 0] + (data[offset + 1] << 8));
            return i;
        }

        public static ushort ArrayToUshort(byte[] data, int offset)
        {
            ushort i = (ushort)(data[offset + 0] + (data[offset + 1] << 8));
            return i;
        }
    }
}
