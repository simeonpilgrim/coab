using System;

namespace Classes
{
	/// <summary>
	/// Summary description for Struct_1A484.
	/// </summary>
	public class Struct_1A484
	{
        public Struct_1A484(byte v0, byte v1, byte v2, byte v3, byte v4, byte v5)
        {
            field_0 = v0;
            field_1 = v1;
            field_2 = v2;
            field_3 = v3;
            field_4 = v4;
            field_5 = v5;
        }

		public byte field_0; // seg600:4174 unk_1A484
		public byte field_1; // seg600:4175 unk_1A485
		public byte field_2; // seg600:4176 unk_1A486
		public byte field_3; // seg600:4177 unk_1A487
		public byte field_4; // seg600:4178 unk_1A488
		public byte field_5; // seg600:4179 unk_1A489

        public byte this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0:
                        return field_0;

                    case 1:
                        return field_1;

                    case 2:
                        return field_2;

                    case 3:
                        return field_3;

                    case 4:
                        return field_4;

                    case 5:
                        return field_5;

                    default:
                        throw new System.ArgumentOutOfRangeException("index");
                }
            }
        }

		public Struct_1A484()
		{
		}
	}
}
