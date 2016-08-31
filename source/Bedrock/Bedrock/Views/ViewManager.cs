using System;
using Bedrock.Regions;

namespace Bedrock.Views
{
    public class ViewManager : IViewManager
    {
        private static readonly WeakDelegatesManager updatingRegionsListeners = new WeakDelegatesManager();

        public IViewsCollection Views { get; }
        public IViewsCollection ActiveViews { get; }

        /// <summary>
        /// Notification used by attached behaviors to update the region managers appropriatelly if needed to.
        /// </summary>
        /// <remarks>This event uses weak references to the event handler to prevent this static event of keeping the
        /// target element longer than expected.</remarks>
        public static event EventHandler UpdatingRegions
        {
            add { updatingRegionsListeners.AddListener(value); }
            remove { updatingRegionsListeners.RemoveListener(value); }
        }

        public IViewManager CreateViewManager()
        {
            throw new NotImplementedException();
        }
    }
}
