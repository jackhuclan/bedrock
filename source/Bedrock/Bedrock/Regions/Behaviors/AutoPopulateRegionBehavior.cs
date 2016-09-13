using System.Collections.Generic;
using Bedrock.Views;

namespace Bedrock.Regions.Behaviors
{
    /// <summary>
    /// Populates the target region with the views registered to it in the <see cref="IRegionViewRegistry"/>.
    /// </summary>
    public class AutoPopulateRegionBehavior : RegionBehavior
    {
        /// <summary>
        /// The key of this behavior.
        /// </summary>
        public const string BehaviorKey = "AutoPopulate";

        private readonly IRegionViewRegistry regionViewRegistry;

        /// <summary>
        /// Creates a new instance of the AutoPopulateRegionBehavior 
        /// associated with the <see cref="IRegionViewRegistry"/> received.
        /// </summary>
        /// <param name="regionViewRegistry"><see cref="IRegionViewRegistry"/> that the behavior will monitor for views to populate the region.</param>
        public AutoPopulateRegionBehavior(IRegionViewRegistry regionViewRegistry)
        {
            this.regionViewRegistry = regionViewRegistry;
        }

        /// <summary>
        /// Attaches the AutoPopulateRegionBehavior to the Region.
        /// </summary>
        protected override void OnAttach()
        {
            if (string.IsNullOrEmpty(this.Region.Name))
            {
                this.Region.PropertyChanged += this.Region_PropertyChanged;
            }
            else
            {
                this.StartPopulatingContent();
            }
        }

        private void StartPopulatingContent()
        {
            foreach (IView view in this.CreateViewsToAutoPopulate())
            {
                AddViewIntoRegion(view);
                regionViewRegistry.OnViewRegistered(new ViewRegisteredEventArgs(Region, view));
            }
        }

        /// <summary>
        /// Returns a collection of views that will be added to the
        /// View collection. 
        /// </summary>
        /// <returns></returns>
        protected virtual IEnumerable<IView> CreateViewsToAutoPopulate()
        {
            return this.regionViewRegistry.GetContents(this.Region.Name);
        }

        /// <summary>
        /// Adds a view into the views collection of this region. 
        /// </summary>
        /// <param name="viewToAdd"></param>
        protected virtual void AddViewIntoRegion(IView viewToAdd)
        {
            this.Region.Add(viewToAdd);
        }

        private void Region_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Name" && !string.IsNullOrEmpty(this.Region.Name))
            {
                this.Region.PropertyChanged -= this.Region_PropertyChanged;
                this.StartPopulatingContent();
            }
        }
    }
}