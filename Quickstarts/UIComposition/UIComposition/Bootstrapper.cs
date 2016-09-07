// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System.Windows.Forms;
using Bedrock.Modularity;
using Bedrock.UnityExtensions;
using Bedrock.Views;
using Microsoft.Practices.Unity;

namespace UIComposition.Shell
{
    public class UiCompositionBootstrapper : UnityBootstrapper
    {
        protected override void ConfigureModuleCatalog()
        {
            base.ConfigureModuleCatalog();

            ModuleCatalog moduleCatalog = (ModuleCatalog)this.ModuleCatalog;
            moduleCatalog.AddModule(typeof(EmployeeModule.ModuleInit));
        }

        private StartupForm _startForm;
        protected override IView CreateStartup()
        {
            _startForm = this.Container.Resolve<StartupForm>();
            return _startForm;
        }

        protected override void ShowStartup()
        {
            Application.Run(_startForm);
        }
    }
}