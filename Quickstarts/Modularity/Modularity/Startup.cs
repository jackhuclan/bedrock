using System;
using System.Drawing;
using System.Globalization;
using Bedrock.Logging;
using Bedrock.Modularity;
using Bedrock.Winform;

namespace Modularity
{
    public partial class Startup : FormView
    {
        private IModuleTracker moduleTracker;
        private IModuleManager moduleManager;
        private CallbackLogger logger;

        public Startup(IModuleManager moduleManager, IModuleTracker moduleTracker, CallbackLogger logger)
        {
            if (moduleManager == null)
            {
                throw new ArgumentNullException("moduleManager");
            }

            if (moduleTracker == null)
            {
                throw new ArgumentNullException("moduleTracker");
            }

            if (logger == null)
            {
                throw new ArgumentNullException("logger");
            }

            this.moduleManager = moduleManager;
            this.moduleTracker = moduleTracker;
            this.logger = logger;

            InitializeComponent();
        }

        public void Log(string message, Category category, Priority priority)
        {
            this.TraceTextBox.AppendText(string.Format(CultureInfo.CurrentUICulture, "[{0}][{1}] {2}\r\n", category, priority, message));
        }

        private void ModuleB_Load(object sender, EventArgs e)
        {
            this.moduleManager.LoadModule(WellKnownModuleNames.ModuleB);
            ModuleB.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128))))); 
        }

        private void ModuleC_Load(object sender, EventArgs e)
        {
            this.moduleManager.LoadModule(WellKnownModuleNames.ModuleC);
            ModuleC.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));

        }

        private void ModuleE_Load(object sender, EventArgs e)
        {
            this.moduleManager.LoadModule(WellKnownModuleNames.ModuleE);
            ModuleE.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
        }

        private void ModuleF_Load(object sender, EventArgs e)
        {
            this.moduleManager.LoadModule(WellKnownModuleNames.ModuleF);
            ModuleF.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));

        }

        private void Startup_Load(object sender, EventArgs e)
        {
            this.DataContext = this.moduleTracker;

            // I set this shell's Log method as the callback for receiving log messages.
            this.logger.Callback = this.Log;
            this.logger.ReplaySavedLogs();
            this.moduleManager.LoadModuleCompleted += this.ModuleManager_LoadModuleCompleted;
            this.moduleManager.ModuleDownloadProgressChanged += this.ModuleManager_ModuleDownloadProgressChanged;
        }

        /// <summary>
        /// Handles the LoadModuleProgressChanged event of the ModuleManager control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Microsoft.Practices.Composite.Modularity.LoadModuleProgressChangedEventArgs"/> instance containing the event data.</param>
        void ModuleManager_ModuleDownloadProgressChanged(object sender, ModuleDownloadProgressChangedEventArgs e)
        {
            this.moduleTracker.RecordModuleDownloading(e.ModuleInfo.ModuleName, e.BytesReceived, e.TotalBytesToReceive);
        }

        /// <summary>
        /// Handles the LoadModuleCompleted event of the ModuleManager control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Microsoft.Practices.Composite.Modularity.LoadModuleCompletedEventArgs"/> instance containing the event data.</param>
        void ModuleManager_LoadModuleCompleted(object sender, LoadModuleCompletedEventArgs e)
        {
            this.moduleTracker.RecordModuleLoaded(e.ModuleInfo.ModuleName);
        }
    }
}
