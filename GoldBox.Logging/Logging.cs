using System;
using System.IO;

namespace GoldBox.Logging
{
    public static class Logger
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
            Console.WriteLine(fmt, args);
            debug.WriteLine(fmt, args);
        }

        static public void LogAndExit(string fmt, params object[] args)
        {
            Console.WriteLine(fmt, args);
            debug.WriteLine(fmt, args);
            ExitFuncCallback();
        }

        static public void Close()
        {
            debug.Close();
        }

        static public void Debug(string fmt, params object[] args)
        {
            Console.WriteLine(fmt, args);
            debug.WriteLine(fmt, args);
        }

        static public void DebugWrite(string fmt, params object[] args)
        {
            Console.Write(fmt, args);
            debug.Write(fmt, args);
        }
    }
}
