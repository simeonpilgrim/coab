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

        public Point mapScreenTopLeft;
        public bool drawTargetCursor; // field_4, was byte
        public int size; // field_5
        public bool field_6; // field_6, was byte
        int[] field_7;

        public int this[Point pos]
        {
            get
            {
                int index = (pos.y * 50) + pos.x;
                return field_7[index];
            }
            set
            {
                int index = (pos.y * 50) + pos.x;
                field_7[index] = value;
            }
        }

        public int this[int x, int y]
        {
            get
            {
                int index = x + (y * 50);
                return field_7[index];
            }
            set
            {
                int index = x + (y * 0x32);
                field_7[index] = value;
            }
        }
    }
}
