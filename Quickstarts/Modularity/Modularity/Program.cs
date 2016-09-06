using System;
using System.Windows.Forms;

namespace Modularity
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            QuickStartBootstrapper bootstrapper = new QuickStartBootstrapper();
            bootstrapper.Run();
        }
    }
}
