using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using Bedrock.Properties;
using Bedrock.Regions.Behaviors;
using Bedrock.Views;
using Microsoft.Practices.ServiceLocation;

namespace Bedrock.Regions
{
    /// <summary>
    /// Defines a registry for the content of the regions used on View Discovery composition.
    /// </summary>
    public class RegionViewRegistry : IRegionViewRegistry
    {
        private readonly IServiceLocator _locator;
        private readonly ListDictionary<string, Func<object>> _registeredView = new ListDictionary<string, Func<object>>();
        private readonly WeakDelegatesManager _viewRegisteredListeners = new WeakDelegatesManager();

        /// <summary>
        /// Creates a new instance of the <see cref="RegionViewRegistry"/> class.
        /// </summary>
        /// <param name="locator"><see cref="IServiceLocator"/> used to create the instance of the views from its <see cref="Type"/>.</param>
        public RegionViewRegistry(IServiceLocator locator)
        {
            this._locator = locator;
        }
        
        /// <summary>
        /// Occurs whenever a new view is registered.
        /// </summary>
        public event EventHandler<ViewRegisteredEventArgs> ViewRegistered
        {
            add { this._viewRegisteredListeners.AddListener(value); }
            remove { this._viewRegisteredListeners.RemoveListener(value); }
        }

        /// <summary>
        /// Returns the contents registered for a region.
        /// </summary>
        /// <param name="regionName">Name of the region which content is being requested.</param>
        /// <returns>Collection of contents registered for the region.</returns>
        public IEnumerable<IView> GetContents(string regionName)
        {
            List<IView> items = new List<IView>();
            foreach (Func<IView> getContentDelegate in this._registeredView[regionName])
            {
                items.Add(getContentDelegate());
            }

            return items;
        }

        /// <summary>
        /// Registers a content type with a region name.
        /// </summary>
        /// <param name="regionName">Region name to which the <paramref name="viewType"/> will be registered.</param>
        /// <param name="viewType">Content type to be registered for the <paramref name="regionName"/>.</param>
        public void RegisterViewWithRegion(string regionName, Type viewType)
        {
            this.RegisterViewWithRegion(regionName, () => this.CreateInstance(viewType));
        }

        /// <summary>
        /// Registers a delegate that can be used to retrieve the content associated with a region name. 
        /// </summary>
        /// <param name="regionName">Region name to which the <paramref name="getContentDelegate"/> will be registered.</param>
        /// <param name="getContentDelegate">Delegate used to retrieve the content associated with the <paramref name="regionName"/>.</param>
        public void RegisterViewWithRegion(string regionName, Func<IView> getContentDelegate)
        {
            RegionShouldAlreadyExist(regionName);
            this._registeredView.Add(regionName, getContentDelegate);
            InvokeBindingBehaviors(regionName);
        }

        /// <summary>
        /// Creates an instance of a registered view <see cref="Type"/>. 
        /// </summary>
        /// <param name="type">Type of the registered view.</param>
        /// <returns>Instance of the registered view.</returns>
        protected virtual IView CreateInstance(Type type)
        {
            return this._locator.GetInstance(type) as IView;
        }

        public void OnViewRegistered(ViewRegisteredEventArgs e)
        {
            try
            {
                this._viewRegisteredListeners.Raise(this, e);
            }
            catch (TargetInvocationException ex)
            {
                Exception rootException;
                if (ex.InnerException != null)
                {
                    rootException = ex.InnerException.GetRootException();
                }
                else
                {
                    rootException = ex.GetRootException();
                }

                throw new ViewRegistrationException(
                    string.Format(
                        CultureInfo.CurrentCulture,
                        Resources.OnViewRegisteredException,
                        e.Region.Name,
                        rootException),
                    ex.InnerException);
            }
        }

        private void RegionShouldAlreadyExist(string regionName)
        {
            var regionManager = (IRegionManager)_locator.GetInstance(typeof(IRegionManager));
            if (regionManager != null && !regionManager.Regions.ContainsRegionWithName(regionName))
            {
                throw new ArgumentNullException(
                    string.Format("The '{0}' should already be added via IView.InitializeRegions!", regionName));
            }
        }

        private void InvokeBindingBehaviors(string regionName)
        {
            var regionManager = (IRegionManager)_locator.GetInstance(typeof(IRegionManager));
            var regionBehaviorFactory = _locator.GetInstance<IRegionBehaviorFactory>();
            if (regionBehaviorFactory == null)
            {
                throw new ArgumentNullException(Resources.IRegionBehaviorFactoryInstanceNotExist);
            }

            if (regionManager != null)
            {
                var region = regionManager.Regions.GetRegionByName(regionName);
                if (region == null)
                {
                    throw new ArgumentNullException(Resources.RegionNotFound);
                }

                region.RegisterDefaultBehavior();
                var behavior = region.Behaviors[AutoPopulateRegionBehavior.BehaviorKey];
                behavior.Attach();
            }
        }
    }
}