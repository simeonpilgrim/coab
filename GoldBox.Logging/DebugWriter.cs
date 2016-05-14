using System.IO;

namespace GoldBox.Logging
{
    public class DebugWriter
    {
        string filename;
        TextWriter writer;
        readonly object iolock = new object();

        public DebugWriter(string _filename)
        {
            filename = _filename;
        }

        public void WriteLine(string fmt, params object[] args)
        {
            lock (iolock)
            {
                if (writer == null)
                {
                    writer = new StreamWriter(filename, true);
                }

                if (writer != null)
                {
                    writer.WriteLine(fmt, args);
                }
            }
        }

        public void Write(string fmt, params object[] args)
        {
            lock (iolock)
            {
                if (writer == null)
                {
                    writer = new StreamWriter(filename, true);
                }

                if (writer != null)
                {
                    writer.Write(fmt, args);
                }
            }
        }

        public void Close()
        {
            lock (iolock)
            {
                if (writer != null)
                {
                    writer.Close();
                }
            }
        }

    }

}