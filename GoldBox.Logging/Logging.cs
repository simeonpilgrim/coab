using System;
using System.IO;

namespace GoldBox.Logging
{
    public static class Logger
    {
        static string logPath;
        static DebugWriter debug;

        public static void Setup(string path)
        {
            logPath = path;

            debug = new DebugWriter(Path.Combine(logPath, "Debugging.txt"));
        }

        public static string GetPath() { return logPath; }

        public delegate void VoidDelegate();
        static VoidDelegate ExitFuncCallback;

        public static void SetExitFunc(VoidDelegate ExitFunc)
        {
            ExitFuncCallback = ExitFunc;
        }


        public static void Log(string fmt, params object[] args)
        {
            Console.WriteLine(fmt, args);
            debug.WriteLine(fmt, args);
        }

        public static void LogAndExit(string fmt, params object[] args)
        {
            Console.WriteLine(fmt, args);
            debug.WriteLine(fmt, args);
            ExitFuncCallback();
        }

        public static void Close()
        {
            debug.Close();
        }

        public static void Debug(string fmt, params object[] args)
        {
            Console.WriteLine(fmt, args);
            debug.WriteLine(fmt, args);
        }

        public static void DebugWrite(string fmt, params object[] args)
        {
            Console.Write(fmt, args);
            debug.Write(fmt, args);
        }
    }
}
