using System;

namespace Classes
{
    /// <summary>
    /// Summary description for Struct_1D1BC.
    /// </summary>
    public class Struct_1D1BC
    {
        const int dataSize = 1250; // 0x4E2 
        public Struct_1D1BC()
        {
            field_7 = new int[dataSize];
        }

        public void SetField_7(int value)
        {
            for (int i = 0; i < dataSize; i++)
            {
                field_7[i] = value;
            }
        }

        //public byte field_0;
        //public byte field_1;
        public int mapScreenLeftX; // field_2, was sbyte
        public int mapScreenTopY; // field_3, was sbyte
        public bool field_4; // field_4, was byte
        public byte size; // field_5
        public byte field_6;
        public int[] field_7;

        public int this[int indexA, int indexB]
        {
            get
            {
                int index = indexA + (indexB * 0x32);
                if (index < 0 || index > dataSize)
                {
                    index = 0;
                }

                return field_7[index];
            }
            set
            {
                int index = indexA + (indexB * 0x32);
                field_7[index] = value;
            }
        }
    }
}
