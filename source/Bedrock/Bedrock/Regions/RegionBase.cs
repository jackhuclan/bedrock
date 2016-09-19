using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using Bedrock.Properties;
using Bedrock.Regions.Behaviors;
using Bedrock.Views;
using Microsoft.Practices.ServiceLocation;

namespace Bedrock.Regions
{
    /// <summary>
    /// Implementation of <see cref="IRegion"/> that allows multiple active views.
    /// </summary>
    public class RegionBase : IRegion
    {
        private ObservableCollection<IView> itemCollection;
        private ObservableCollection<IView> activatedItemCollection;
        private string name;
        private ViewsCollection views;
        private ViewsCollection activeViews;
        private object context;
        private IRegionManager regionManager;

        /// <summary>
        /// Initializes a new instance of <see cref="Region"/>.
        /// </summary>
        public RegionBase(object control)
        {
            this.Behaviors = new RegionBehaviorCollection(this);
            this.name = GetNamePropertyVal(control);
            this.Control = control;
            this.activatedItemCollection = new ObservableCollection<IView>();
        }

        public RegionBase(string name, object control)
        {
            this.Behaviors = new RegionBehaviorCollection(this);
            this.name = name;
            this.Control = control;
            this.activatedItemCollection = new ObservableCollection<IView>();
        }

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Gets the collection of <see cref="IRegionBehavior"/>s that can extend the behavior of regions. 
        /// </summary>
        public IRegionBehaviorCollection Behaviors { get; private set; }

        /// <summary>
        /// Gets or sets a context for the region. This value can be used by the user to share context with the views.
        /// </summary>
        /// <value>The context value to be shared.</value>
        public object Context
        {
            get
            {
                return this.context;
            }

            set
            {
                if (this.context != value)
                {
                    this.context = value;
                    this.OnPropertyChanged("Context");
                }
            }
        }

        /// <summary>
        /// Gets the name of the region that uniequely identifies the region within a <see cref="IRegionManager"/>.
        /// </summary>
        /// <value>The name of the region.</value>
        public string Name
        {
            get
            {
                return this.name;
            }

            set
            {
                if (this.name != null && this.name != value)
                {
                    throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, Resources.CannotChangeRegionNameException, this.name));
                }

                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException(Resources.RegionNameCannotBeEmptyException);
                }

                this.name = value;
                this.OnPropertyChanged("Name");
            }
        }

        public object Control { get; set; }

        /// <summary>
        /// Gets a readonly view of the collection of views in the region.
        /// </summary>
        /// <value>An <see cref="IViewsCollection"/> of all the added views.</value>
        public virtual IViewsCollection Views
        {
            get
            {
                if (this.views == null)
                {
                    this.views = new ViewsCollection(ItemMetadataCollection);
                }

                return this.views;
            }
        }

        /// <summary>
        /// Gets a readonly view of the collection of all the active views in the region.
        /// </summary>
        /// <value>An <see cref="IViewsCollection"/> of all the active views.</value>
        public virtual IViewsCollection ActiveViews
        {
            get
            {
                if (this.activeViews == null)
                {
                    this.activeViews = new ViewsCollection(activatedItemCollection);
                }

                return this.activeViews;
            }
        }


        /// <summary>
        /// Gets or sets the <see cref="IRegionManager"/> that will be passed to the views when adding them to the region, unless the view is added by specifying createRegionManagerScope as <see langword="true" />.
        /// </summary>
        /// <value>The <see cref="IRegionManager"/> where this <see cref="IRegion"/> is registered.</value>
        /// <remarks>This is usually used by implementations of <see cref="IRegionManager"/> and should not be
        /// used by the developer explicitely.</remarks>
        public IRegionManager RegionManager
        {
            get
            {
                return this.regionManager;
            }

            set
            {
                if (this.regionManager != value)
                {
                    this.regionManager = value;
                    this.OnPropertyChanged("RegionManager");
                }
            }
        }


        /// <summary>
        /// Gets the collection with all the views along with their metadata.
        /// </summary>
        /// <value>An <see cref="ObservableCollection{T}"/> of <see cref="ItemMetadata"/> with all the added views.</value>
        protected virtual ObservableCollection<IView> ItemMetadataCollection
        {
            get
            {
                if (this.itemCollection == null)
                {
                    this.itemCollection = new ObservableCollection<IView>();
                }

                return this.itemCollection;
            }
        }

        /// <overloads>Adds a new view to the region.</overloads>
        /// <summary>
        /// Adds a new view to the region.
        /// </summary>
        /// <param name="view">The view to add.</param>
        /// <returns>The <see cref="IRegionManager"/> that is set on the view if it is a <see cref="DependencyObject"/>. It will be the current region manager when using this overload.</returns>
        public IRegionManager Add(IView view)
        {
            return this.Add(view, view.Name, false);
        }

        /// <summary>
        /// Adds a new view to the region.
        /// </summary>
        /// <param name="view">The view to add.</param>
        /// <param name="viewName">The name of the view. This can be used to retrieve it later by calling <see cref="IRegion.GetView"/>.</param>
        /// <returns>The <see cref="IRegionManager"/> that is set on the view if it is a <see cref="DependencyObject"/>. It will be the current region manager when using this overload.</returns>
        public IRegionManager Add(IView view, string viewName)
        {
            if (string.IsNullOrEmpty(viewName))
            {
                throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Resources.StringCannotBeNullOrEmpty, "viewName"));
            }

            return this.Add(view, viewName, false);
        }

        /// <summary>
        /// Adds a new view to the region.
        /// </summary>
        /// <param name="view">The view to add.</param>
        /// <param name="viewName">The name of the view. This can be used to retrieve it later by calling <see cref="IRegion.GetView"/>.</param>
        /// <param name="createRegionManagerScope">When <see langword="true"/>, the added view will receive a new instance of <see cref="IRegionManager"/>, otherwise it will use the current region manager for this region.</param>
        /// <returns>The <see cref="IRegionManager"/> that is set on the view if it is a <see cref="DependencyObject"/>.</returns>
        public virtual IRegionManager Add(IView view, string viewName, bool createRegionManagerScope)
        {
            if (GetView(viewName) != null)
            {
                throw new InvalidOperationException(Resources.ViewAlreadyExistsInRegion);
            }

            view.Name = viewName;
            IRegionManager manager = createRegionManagerScope ? this.RegionManager.CreateRegionManager() : this.RegionManager;
            this.ItemMetadataCollection.Add(view);
            return manager;
        }

        public void RemoveAllViews()
        {
            foreach (IView view in Views)
            {
                Deactivate(view);
                ActiveViews.Remove(view);
                Views.Remove(view);
            }
        }

        /// <summary>
        /// Removes the specified view from the region.
        /// </summary>
        /// <param name="view">The view to remove.</param>
        public virtual void Remove(IView view)
        {
            this.ItemMetadataCollection.Remove(view);
            ActiveViews.Remove(view);
            Views.Remove(view);
        }

        /// <summary>
        /// Marks the specified view as active. 
        /// </summary>
        /// <param name="view">The view to activate.</param>
        public virtual void Activate(IView view)
        {
            if (view == null)
            {
                throw new ArgumentNullException(Resources.ViewShouldNotBeNull);
            }

            if (GetView(view.Name) == null)
            {
                throw new ArgumentException(Resources.ViewNotInRegionException);
            }

            ActiveViews.Add(view);
        }

        public void Activate()
        {
            foreach (IView view in Views)
            {
                Activate(view);
            }
        }

        /// <summary>
        /// Marks the specified view as inactive. 
        /// </summary>
        /// <param name="view">The view to deactivate.</param>
        public virtual void Deactivate(IView view)
        {
            if (view == null)
            {
                throw new ArgumentNullException(Resources.ViewShouldNotBeNull);
            }

            if (GetView(view.Name) == null)
            {
                throw new ArgumentException(Resources.ViewNotInRegionException);
            }

            ActiveViews.Remove(view);
        }

        public void Deactivate()
        {
            foreach (IView view in Views)
            {
                Deactivate(view);
            }
        }

        /// <summary>
        /// Returns the view instance that was added to the region using a specific name.
        /// </summary>
        /// <param name="viewName">The name used when adding the view to the region.</param>
        /// <returns>Returns the named view or <see langword="null"/> if the view with <paramref name="viewName"/> does not exist in the current region.</returns>
        public virtual IView GetView(string viewName)
        {
            if (string.IsNullOrEmpty(viewName))
            {
                throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Resources.StringCannotBeNullOrEmpty, "viewName"));
            }

            return ItemMetadataCollection.FirstOrDefault(x => x.Name == viewName);
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler eventHandler = this.PropertyChanged;
            if (eventHandler != null)
            {
                eventHandler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private string GetNamePropertyVal(object control)
        {
            var nameProperty = control.GetType().GetProperty("Name");
            if (nameProperty == null)
            {
                throw new ArgumentNullException(Resources.CannotFindNameProperty);
            }

            var val = nameProperty.GetValue(control);
            if (val == null)
            {
                throw new ArgumentNullException(Resources.NameValueOfControlIsNull);
            }

            return val.ToString();
        }

    }
}