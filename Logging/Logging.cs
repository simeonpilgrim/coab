using System;
using System.IO;
using System.Text;


namespace Logging
{
    public class Logger
    {
		static string logPath;
		static DebugWriter debug;

		static public void Setup(string path)
		{
			logPath = path;

			debug = new DebugWriter(Path.Combine(logPath, "Debugging.txt"));
		}

		public static string GetPath() { return logPath; }

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

    public class DebugWriter
    {
        string filename;
        System.IO.TextWriter writer;
        object iolock = new object();

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
                    writer = new System.IO.StreamWriter(filename,true);
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
                    writer = new System.IO.StreamWriter(filename, true);
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
