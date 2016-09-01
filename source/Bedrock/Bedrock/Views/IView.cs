using Bedrock.Regions;

namespace Bedrock.Views
{
    public interface IView
    {
        string Name { get; set; }
        object DataContext { get; set; }
        IRegionManager RegionManager { get; set; }
    }
}
