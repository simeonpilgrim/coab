using System;

namespace Classes
{
	/// <summary>
	/// Summary description for Struct_1D1BC.
	/// </summary>
	public class Struct_1D1BC
	{
		public Struct_1D1BC()
		{
			field_7 = new byte[0x4E2];
		}

        //public byte field_0;
        //public byte field_1;
        public int mapScreenLeftX; // field_2, was sbyte
        public int mapScreenTopY; // field_3, was sbyte
        public byte field_4;
        public byte field_5;
        public byte field_6;
		public byte[] field_7;

		public byte this [int indexA, int indexB ]
		{
			get 
			{
				return field_7[ indexA + ( indexB * 0x32 ) ];
			}
			set 
			{	
				field_7[ indexA + ( indexB * 0x32 ) ] = value;
			}
		}
    }
}
