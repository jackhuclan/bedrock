using System.Windows.Forms;
using Bedrock.Modularity;
using Bedrock.Regions;

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
            _regionViewRegistry.ContentRegistered += _regionViewRegistry_ContentRegistered;
            _regionViewRegistry.RegisterViewWithRegion("panel1", typeof(Views.HelloWorldView));
        }

        private void _regionViewRegistry_ContentRegistered(object sender, ViewRegisteredEventArgs e)
        {
            var view = e.GetView() as Control;
            var regionContainer = _regionManager.Regions[e.RegionName].Control as Control;
            if (regionContainer != null && view != null)
            {
                regionContainer.Controls.Add(view);
            }
        }
    }
}
