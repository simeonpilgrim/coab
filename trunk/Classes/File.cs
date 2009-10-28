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
            // TODO tidy-up this pascal based concept.
		}

		public string name;

        public System.IO.FileStream stream;

        public void Assign(string fileString)
        {
            name = fileString;
            stream = System.IO.File.Open(fileString, System.IO.FileMode.OpenOrCreate);
        }  
	}
}
