using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Threading;
using Classes;
using Logging;

namespace Main
{
    static class Program
    {
        static MainForm main;
        static Thread engineThread;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Logger.SetExitFunc(engine.seg043.print_and_exit);

            main = new MainForm();

            ThreadStart threadDelegate = new ThreadStart(EngineThread);
            engineThread = new Thread(threadDelegate);
            engineThread.Name = "Engine";
            engineThread.Start();
            

            Application.Run(main);
        }

        public delegate void VoidDelegate();

        static void EngineStopped()
        {
            VoidDelegate d = delegate()
            {
                Application.Exit();
            };
            main.Invoke(d);
        }

        static void EngineThread()
        {
            //try
            //{
            engine.seg001.__SystemInit(EngineStopped);
            engine.seg001.PROGRAM();
            //}
            //catch (Exception e)
            //{
            //    Console.Write(e.ToString());
            //}
            //finally
            //{
            EngineStopped();
            //}
        }
    }
}