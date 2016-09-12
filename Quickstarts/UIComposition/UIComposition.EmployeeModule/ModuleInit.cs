using System.Linq;
using Bedrock.Modularity;
using Bedrock.Regions;
using Bedrock.Views;
using Microsoft.Practices.Unity;
using UIComposition.EmployeeModule.Controllers;
using UIComposition.EmployeeModule.Services;
using UIComposition.EmployeeModule.Views;

namespace UIComposition.EmployeeModule
{
    public class ModuleInit : IModule
    {
        private readonly IUnityContainer container;
        private readonly IRegionManager regionManager;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        private MainRegionController _mainRegionController;

        public ModuleInit(IUnityContainer container, IRegionManager regionManager)
        {
            this.container = container;
            this.regionManager = regionManager;
        }

        public void Initialize()
        {
            // Register the EmployeeDataService concrete type with the container.
            // Change this to swap in another data service implementation.
            this.container.RegisterType<IEmployeeDataService, EmployeeDataService>();

            // This is an example of View Discovery which associates the specified view type
            // with a region so that the view will be automatically added to the region when
            // the region is first displayed.

            // Show the Employee List view in the shell's left hand region.
            this.regionManager.RegisterViewWithRegion(RegionNames.LeftRegion, () => this.container.Resolve<EmployeeListView>());
            this.regionManager.Regions[RegionNames.LeftRegion].Activate();

            // Create the main region controller.
            // This is used to programmatically coordinate the view
            // in the main region of the shell.
            this._mainRegionController = this.container.Resolve<MainRegionController>();

            this.regionManager.RegisterViewWithRegion(RegionNames.TabRegion1, () => this.container.Resolve<EmployeeDetailsView>());
            this.regionManager.Regions[RegionNames.TabRegion1].Activate();
            this.regionManager.RegisterViewWithRegion(RegionNames.TabRegion2, () => this.container.Resolve<EmployeeProjectsView>());
            this.regionManager.Regions[RegionNames.TabRegion2].Activate();
        }
    }
}