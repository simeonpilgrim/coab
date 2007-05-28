using System;

namespace Classes
{
	/// <summary>
	/// Summary description for Text.
	/// </summary>
	public class Text
	{

        public ushort field_2;


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

            field_2 = 0xD7B0;
        }
	}
}
