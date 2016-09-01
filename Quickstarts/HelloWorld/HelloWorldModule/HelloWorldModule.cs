using Bedrock.Modularity;
using Bedrock.Regions;

namespace HelloWorldModule
{
    public class HelloWorldModule : IModule
    {
        private readonly IRegionViewRegistry _regionViewRegistry;

        public HelloWorldModule(IRegionViewRegistry registry)
        {
            this._regionViewRegistry = registry;   
        }

        public void Initialize()
        {
            _regionViewRegistry.RegisterViewWithRegion("MainRegion", typeof(Views.HelloWorldView));
        }
    }
}
