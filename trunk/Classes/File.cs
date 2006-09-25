using System;

namespace Classes
{
	/// <summary>
	/// Summary description for File.
	/// </summary>
	public class File
	{
		public File()
		{
			//
			// TODO: Add constructor logic here
			//
			field_4 = new byte[22];
		}

		public short field_0;
		public ushort status;
		public byte id;
		public byte[] field_4;
		public string name;

        public System.IO.FileStream stream;

        public void Assign(string fileString)
        {
            name = fileString;
            stream = System.IO.File.Open(fileString, System.IO.FileMode.OpenOrCreate);
        }  
	}
}
