using Bedrock.Regions;

namespace Bedrock.Views
{
    public interface IView
    {
        string Name { get; set; }
        object DataContext { get; set; }
        void RegisterRegion(string regionName, object control);
    }
}
