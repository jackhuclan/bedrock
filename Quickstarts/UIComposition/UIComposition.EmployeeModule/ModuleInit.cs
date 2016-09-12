using Bedrock.Modularity;
using Bedrock.Regions;
using Microsoft.Practices.Unity;
using UIComposition.EmployeeModule.Services;
using UIComposition.EmployeeModule.Views;

namespace UIComposition.EmployeeModule
{
    public class ModuleInit : IModule
    {
        public ModuleInit(IUnityContainer container, IRegionManager regionManager)
        {
            this._container = container;
            this._regionManager = regionManager;
        }

        public void Initialize()
        {
            // Register the EmployeeDataService concrete type with the _container.
            // Change this to swap in another data service implementation.
            this._container.RegisterType<IEmployeeDataService, EmployeeDataService>();

            // This is an example of View Discovery which associates the specified view type
            // with a region so that the view will be automatically added to the region when
            // the region is first displayed.

            // Show the Employee List view in the shell's left hand region.
            this._regionManager.RegisterViewWithRegion(RegionNames.LeftRegion, () => this._container.Resolve<EmployeeListView>());
            this._regionManager.Regions[RegionNames.LeftRegion].Activate();

            this._regionManager.RegisterViewWithRegion(RegionNames.TabRegion1, () => this._container.Resolve<EmployeeDetailsView>());
            this._regionManager.Regions[RegionNames.TabRegion1].Activate();
            this._regionManager.RegisterViewWithRegion(RegionNames.TabRegion2, () => this._container.Resolve<EmployeeProjectsView>());
            this._regionManager.Regions[RegionNames.TabRegion2].Activate();
        }

        private readonly IUnityContainer _container;
        private readonly IRegionManager _regionManager;
    }
}