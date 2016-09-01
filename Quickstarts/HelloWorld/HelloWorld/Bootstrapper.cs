using System.Windows.Forms;
using Bedrock.Modularity;
using Bedrock.UnityExtensions;
using Bedrock.Views;
using Microsoft.Practices.Unity;

namespace HelloWorld
{
    class Bootstrapper : UnityBootstrapper
    {
        private Form1 _startForm;
        protected override IStartupView CreateStartup()
        {
            _startForm = this.Container.Resolve<Form1>();
            return _startForm;
        }

        protected override void ShowStartup()
        {
            Application.Run(_startForm);
        }     

        protected override void ConfigureModuleCatalog()
        {
            base.ConfigureModuleCatalog();

            ModuleCatalog moduleCatalog = (ModuleCatalog)this.ModuleCatalog;
            moduleCatalog.AddModule(typeof(HelloWorldModule.HelloWorldModule));
        }
    }
}
