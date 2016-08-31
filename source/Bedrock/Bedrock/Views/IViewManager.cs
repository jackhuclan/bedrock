using Bedrock.Regions;

namespace Bedrock.Views
{
    public interface IViewManager
    {
        /// <summary>
        /// Gets a collection of <see cref="IView"/> that identify each region by name. You can use this collection to add or remove regions to the current region manager.
        /// </summary>
        IViewsCollection Views { get; }

        /// <summary>
        /// 
        /// </summary>
        IViewsCollection ActiveViews { get; }

        /// <summary>
        /// Creates a new view manager.
        /// </summary>
        /// <returns>A new view manager that can be used as a different scope from the current region manager.</returns>
        IViewManager CreateViewManager();
    }
}
