using System;
using System.IO;

namespace GoldBox.Classes
{
    /// <summary>
    /// Summary description for File.
    /// </summary>
    public class OpenOrCreateFile : IDisposable
    {
        public string name;

        private FileStream stream;

        public void Assign(string fileString)
        {
            name = fileString;
            stream = File.Open(fileString, FileMode.OpenOrCreate);
        }

        public void Reset()
        {
            stream.Seek(0, SeekOrigin.Begin);
        }

        public void Dispose()
        {
            stream.Dispose();
        }

        public void Rewrite()
        {
            stream.SetLength(0);
        }

        public int BlockRead(int count, byte[] data)
        {
            return stream.Read(data, 0, count);
        }

        public void BlockWrite(int arg_4, byte[] arg_6)
        {
            stream.Write(arg_6, 0, arg_4);
        }

    }
}
