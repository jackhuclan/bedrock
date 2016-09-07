using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Bedrock;
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
