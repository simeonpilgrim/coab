using System;

namespace Classes
{
    public delegate int TextDelegate(Text arg_0);
	/// <summary>
	/// Summary description for Text.
	/// </summary>
	public class Text
	{
        public ushort field_0;

        public ushort field_2;
        public ushort field_4;
        public ushort field_6;
        public ushort field_8;

        public object field_C;

        public TextDelegate field_14;
        public TextDelegate field_18;
        public TextDelegate field_1C;

        public byte field_30 = 0;

        public object field_80;

        public System.IO.TextReader reader;
        public System.IO.TextWriter writer;

		public Text()
		{
            reader = System.Console.In;
            writer = System.Console.Out;
		}

        public enum AssignType
        {
            Read,
            Write
        }

        AssignType _type;

        public void Assign(string s, AssignType type)
        {
            _type = type;

            if (s != string.Empty)
            {
                if (type == AssignType.Read)
                {
                    reader = new System.IO.StreamReader(s);
                }
                else
                {
                    writer = new System.IO.StreamWriter(s);
                }
            }

            field_0 = 0;
            field_2 = 0xD7B0;
            field_4 = 0x0080;
        }
	}
}
