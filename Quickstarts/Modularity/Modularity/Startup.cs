using System;
using Bedrock.Modularity;
using Bedrock.Winform;
using Modularity.ModuleTracking;

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
    }
}
