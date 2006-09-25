using System;
using System.Diagnostics;

namespace Classes
{
	/// <summary>
	/// Summary description for StringList.
	/// </summary>
	public class StringList : IListBase // size = 0x2E
	{
		public string s;        // 0x00;
		public byte field_29;   // 0x29;
		public StringList next; // 0x2a; 

		public StringList()
		{
			next = null;
			field_29 = 0;
			s = string.Empty;
		}

        [DebuggerStepThroughAttribute]
        public string String()
        {
            return s;
        }

        public IListBase Next()
        {
            return next;
        }

        public byte Field29()
        {
            return field_29;
        }
	}
}
