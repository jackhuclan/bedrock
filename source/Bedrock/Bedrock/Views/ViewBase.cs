using Bedrock.Regions;

namespace Bedrock.Views
{
    public abstract class ViewBase : IView
    {
        public string Name { get; set; }
        public object DataContext { get; set; }
        public abstract void InitializeRegions();
        public IRegionManager RegionManager { get; set; }

        public void AddRegion(string regionName, object control)
        {
            if (RegionManager != null)
            {
                RegionManager.Regions.Add(new Region(regionName, control));
            }
        }

        public IRegion GetRegionByName(string regionName)
        {
            if (RegionManager != null)
            {
                RegionManager.Regions.GetRegionByName(regionName);
            }

            return null;
        }

        public bool ContainsRegionWithName(string regionName)
        {
            return RegionManager != null && RegionManager.Regions.ContainsRegionWithName(regionName);
        }

        public void RemoveRegion(string regionName)
        {
            if (RegionManager != null)
            {
                RegionManager.Regions.Remove(regionName);
            }
        }
    }
}
