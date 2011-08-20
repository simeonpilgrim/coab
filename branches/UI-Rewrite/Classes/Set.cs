using System;

namespace Classes
{
    /// <summary>
    /// Summary description for Set.
    /// </summary>
    public class Set
    {
        byte[] bits;
        const uint arrayLen = 0x20;
        const int shift = 3;
        const int mask = 7;

        public Set()
        {
            bits = new byte[arrayLen];
        }


        public Set(ushort arg_0, byte[] arg_2)
        {
            bits = new byte[arrayLen];

            int indA = arg_0 >> 8;
            int indB = arg_0 & 0x00ff;

            for (int i = 0; i < indA; i++)
            {
                bits[i] = 0;
            }

            for (int i = 0; i < indB; i++)
            {
                bits[i + indA] = arg_2[i];
            }

            for (int i = indA + indB; i < 20; i++)
            {
                bits[i] = 0;
            }
        }

        public void Clear()
        {
            for (int i = 0; i < arrayLen; i++)
            {
                bits[i] = 0;
            }
        }

        public static Set operator +(Set lhs, byte rhs)
        {
            lhs.bits[rhs >> shift] |= (byte)(1 << (rhs & mask));

            return lhs;
        }

        public void SetRange(byte arg_0, byte arg_2)
        {
            //public static void Set__operator+=( byte arg_0, byte arg_2, int arg_4 )
            //Set::operator+=(Byte,Byte)
            //original left the Set object on the stack.

            for (byte i = arg_2; i <= arg_0; i++)
            {
                bits[i >> shift] |= (byte)(1 << (i & mask));
            }
        }

        public bool MemberOf(int bit)
        {
            int lhs = bits[bit >> shift];
            int rhs = 1 << (bit & mask);

            return ((lhs & rhs) != 0);
        }
    }
}
