using System;
using System.Windows.Forms;
using UIComposition.Shell;

namespace UIComposition
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

            UiCompositionBootstrapper bootstrapper = new UiCompositionBootstrapper();
            bootstrapper.Run();
        }
    }
}
