using System;

namespace Classes
{
	/// <summary>
	/// Summary description for Struct_1C020.
	/// </summary>
	public class Struct_1C020
	{
		public byte field_0; //seg600:5D10 unk_1C020
		public byte field_1; //seg600:5D11 unk_1C021
        /// <summary>
        /// field_2
        /// </summary>
        public byte diceCount; //seg600:5D12 unk_1C022
        /// <summary>
        /// field_3
        /// </summary>
        public byte diceSize; //seg600:5D13 unk_1C023
		public sbyte field_4; //seg600:5D14
		public byte field_5; //seg600:5D15
		public byte field_6; //seg600:5D16 unk_1C026
		public byte field_7; //seg600:5D17 unk_1C027
		public byte field_8; //seg600:5D18
		public byte field_9; //seg600:5D19
		public byte field_A; //seg600:5D1A
		public byte field_B; //seg600:5D1B
		public byte field_C; //seg600:5D1C unk_1C02C
		public byte field_D; //seg600:5D1D
		public byte field_E; //seg600:5D1E unk_1C02E
		public byte field_F; //seg600:5D1F 

		public Struct_1C020()
		{
		}

        public Struct_1C020(byte[] data, int offset)
        {
            field_0 = data[offset + 0];
            field_1 = data[offset + 1];
            diceCount = data[offset + 2];
            diceSize = data[offset + 3];
            field_4 = (sbyte)data[offset + 4];
            field_5 = data[offset + 5];
            field_6 = data[offset + 6];
            field_7 = data[offset + 7];
            field_8 = data[offset + 8];
            field_9 = data[offset + 9];
            field_A = data[offset + 0xa];
            field_B = data[offset + 0xb];
            field_C = data[offset + 0xc];
            field_D = data[offset + 0xd];
            field_E = data[offset + 0xe];
            field_F = data[offset + 0xf];
        }

        public Struct_1C020 ShallowClone()
        {
            Struct_1C020 i = (Struct_1C020)this.MemberwiseClone();
            return i;
        }
	}
}
