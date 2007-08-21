using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Threading;

namespace Main
{
    static class Program
    {
        static MainForm main;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            main = new MainForm();

            ThreadStart threadDelegate = new ThreadStart(EngineThread);
            Thread engineThread = new Thread(threadDelegate);
            engineThread.Name = "Engine";
            engineThread.Start();
            

            Application.Run(main);
        }

        public delegate void VoidDelegate();

        static void EngineThread()
        {
            try
            {
                engine.seg001.__SystemInit();
                engine.seg001.PROGRAM();
            }
            catch (Exception e)
            {
                Console.Write(e.ToString());
            }
            finally
            {
                VoidDelegate d = delegate() { main.Close(); };
                main.Invoke(d);
            }
        }
    }
}