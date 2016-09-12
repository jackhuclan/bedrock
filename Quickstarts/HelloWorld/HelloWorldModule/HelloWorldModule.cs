using System.Linq;
using System.Windows.Forms;
using Bedrock.Modularity;
using Bedrock.Regions;
using Bedrock.Views;

namespace HelloWorldModule
{
    public class HelloWorldModule : IModule
    {
        private readonly IRegionViewRegistry _regionViewRegistry;
        private readonly IRegionManager _regionManager;

        public HelloWorldModule(IRegionViewRegistry registry, IRegionManager regionManager)
        {
            this._regionViewRegistry = registry;
            _regionManager = regionManager;
        }

        public void Initialize()
        {
            _regionViewRegistry.ViewRegistered += _regionViewRegistry_ViewRegistered;
            _regionViewRegistry.RegisterViewWithRegion("panel1", typeof(Views.HelloWorldView));

            ////you could show the subview by region's activate method;

            //            var leftRegion = this._regionManager.Regions["panel1"];
            //            var viewOfLeftRegion = leftRegion.Views.First() as IView;
            //            leftRegion.Activate(viewOfLeftRegion);
        }

        private void _regionViewRegistry_ViewRegistered(object sender, ViewRegisteredEventArgs e)
        {
            ////you could show the subview by register IRegionViewRegistry's ViewRegisteredEvent
            ////then add subview to container manually.

            var container = e.Region.Control as Control;
            var view = e.RegisteredView as Control;
            if (container != null && view != null)
            {
                container.Controls.Add(view);
            }
        }
    }
}
