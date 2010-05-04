using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Threading;
using Classes;
using Logging;
using System.IO;

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
			Config.Setup();

            //Environment.OSVersion.Platform
			AppDomain.CurrentDomain.UnhandledException
				+= delegate(object sender, UnhandledExceptionEventArgs args)
				{
					var exception = (Exception)args.ExceptionObject;

					string logFile = Path.Combine(Logger.GetPath(), "Crash Log.txt");

					using(TextWriter tw = new StreamWriter(logFile, true))
					{
						tw.WriteLine("");
						tw.WriteLine("{0} {1}", Application.ProductName, Application.ProductVersion);
						tw.WriteLine("{0}", DateTime.Now);
						tw.WriteLine("Unhandled exception: " + exception);
					}

					MessageBox.Show(string.Format("Unexpected Error, please send '{0}' to simeon.pilgrim@gmail.com", logFile), "Unexpected Error");
					Environment.Exit(1);
				};


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