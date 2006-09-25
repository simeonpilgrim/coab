using System;
using System.Collections.Generic;
using System.Text;

namespace Classes
{
    public class Sys
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

        public static void ShortToArray(short value, byte[] data, int offset)
        {
            data[offset + 0] = (byte)(value & 0x00ff);
            data[offset + 1] = (byte)((value >> 8) & 0x00ff);
        }

        /// <summary>
        /// Converts a Pascal string array to a C# string.
        /// </summary>
        public static string ArrayToString(byte[] data, int offset, int length)
        {
            length = Math.Min(data[offset], length);

            StringBuilder sb = new StringBuilder(length);
            for (int i = 1; i <= length; i++)
            {
                sb.Append((char)data[i + offset]);
            }

            return sb.ToString();
        }

        public static string ArrayToString(char[] data, int offset, int length)
        {
            length = Math.Min(data[offset], length);

            StringBuilder sb = new StringBuilder(length);
            for (int i = 1; i < length; i++)
            {
                sb.Append(data[i + offset]);
            }

            return sb.ToString();
        }

        /// <summary>
        /// returns array of strings based on an array of Pascal strings.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="offset"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string[] ArrayToStrings(byte[] data, int offset, int length)
        {
            throw new Exception("The method or operation is not implemented.");
        }
    }
}
