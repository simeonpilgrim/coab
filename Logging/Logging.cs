using System;
using System.Text;


namespace Logging
{
    public class Logger
    {
        static DebugWriter debug = new DebugWriter("debug.txt");
        
        public delegate void VoidDelegate();
        static VoidDelegate ExitFuncCallback;

        static public void SetExitFunc(VoidDelegate ExitFunc)
        {
            ExitFuncCallback = ExitFunc;
        }
       

        static public void Log(string fmt, params object[] args)
        {
            System.Console.WriteLine(fmt, args);
            debug.WriteLine(fmt, args);
        }

        static public void LogAndExit(string fmt, params object[] args)
        {
            System.Console.WriteLine(fmt, args);
            debug.WriteLine(fmt, args);
            ExitFuncCallback();
        }

        static public void Close()
        {
            debug.Close();
        }

        static public void Debug(string fmt, params object[] args)
        {
            System.Console.WriteLine(fmt, args);
            debug.WriteLine(fmt, args);
        }

        static public void DebugWrite(string fmt, params object[] args)
        {
            System.Console.Write(fmt, args);
            debug.Write(fmt, args);
        }
    }

    class DebugWriter
    {
        string filename;
        System.IO.TextWriter writer;
        object iolock = new object();

        internal DebugWriter(string _filename)
        {
            filename = _filename;
        }

        internal void WriteLine(string fmt, params object[] args)
        {
            lock (iolock)
            {
                if (writer == null)
                {
                    writer = new System.IO.StreamWriter(filename,true);
                }

                if (writer != null)
                {
                    writer.WriteLine(fmt, args);
                }
            }
        }

        internal void Write(string fmt, params object[] args)
        {
            lock (iolock)
            {
                if (writer == null)
                {
                    writer = new System.IO.StreamWriter(filename, true);
                }

                if (writer != null)
                {
                    writer.Write(fmt, args);
                }
            }
        }

        internal void Close()
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
