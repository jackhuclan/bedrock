using Bedrock.Regions;

namespace Bedrock.Views
{
    public interface IView
    {
        string Name { get; set; }
        object DataContext { get; set; }

        #region region operations

        void InitializeRegions();
        IRegionManager RegionManager { get; set; }
        void AddRegion(string regionName, object control);
        void RemoveRegion(string regionName);
        IRegion GetRegionByName(string regionName);
        bool ContainsRegionWithName(string regionName);

        #endregion
    }
}
