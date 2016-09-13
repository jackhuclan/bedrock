using System;
using Bedrock.Views;

namespace Bedrock.Regions
{
    /// <summary>
    /// Argument class used by the <see cref="IRegionViewRegistry.ViewRegistered"/> event when a new content is registered.
    /// </summary>
    public class ViewRegisteredEventArgs : EventArgs
    {
        public ViewRegisteredEventArgs(IRegion region, IView view)
        {
            this.Region = region;
            this.RegisteredView = view;
        }

        /// <summary>
        /// Gets the region to which the content was registered.
        /// </summary>
        public IRegion Region { get; private set; }

        /// <summary>
        /// Gets the view which was registered.
        /// </summary>
        public IView RegisteredView { get; private set; }
    }
}
