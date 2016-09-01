// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System;
using System.Globalization;
using System.Threading;
using Bedrock.Properties;
using Bedrock.Views;
using Microsoft.Practices.ServiceLocation;

namespace Bedrock.Regions
{
    /// <summary>
    /// Class that creates a fluent interface for the <see cref="IRegionManager"/> class, with respect to 
    /// adding views to regions (View Injection pattern), registering view types to regions (View Discovery pattern)
    /// </summary>
    public static class RegionManagerExtensions
    {
        /// <summary>
        ///     Add a view to the Views collection of a Region. Note that the region must already exist in this regionmanager. 
        /// </summary>
        /// <param name="regionManager">The regionmanager that this extension method effects.</param>
        /// <param name="regionName">The name of the region to add a view to</param>
        /// <param name="view">The view to add to the views collection</param>
        /// <returns>The RegionManager, to easily add several views. </returns>
        public static IRegionManager AddToRegion(this IRegionManager regionManager, string regionName, IView view)
        {
            if (regionManager == null) throw new ArgumentNullException("regionManager");

            if (!regionManager.Regions.ContainsRegionWithName(regionName))
            {
                throw new ArgumentException(
                    string.Format(Thread.CurrentThread.CurrentCulture, Resources.RegionNotFound, regionName), "regionName");
            }

            IRegion region = regionManager.Regions[regionName];

            return region.Add(view);
        }

        /// <summary>
        /// Associate a view with a region, by registering a type. When the region get's displayed
        /// this type will be resolved using the ServiceLocator into a concrete instance. The instance
        /// will be added to the Views collection of the region
        /// </summary>
        /// <param name="regionManager">The regionmanager that this extension method effects.</param>
        /// <param name="regionName">The name of the region to associate the view with.</param>
        /// <param name="viewType">The type of the view to register with the </param>
        /// <returns>The regionmanager, for adding several views easily</returns>
        public static IRegionManager RegisterViewWithRegion(this IRegionManager regionManager, string regionName, Type viewType)
        {
            var regionViewRegistry = ServiceLocator.Current.GetInstance<IRegionViewRegistry>();

            regionViewRegistry.RegisterViewWithRegion(regionName, viewType);

            return regionManager;
        }

        /// <summary>
        /// Associate a view with a region, using a delegate to resolve a concreate instance of the view. 
        /// When the region get's displayed, this delelgate will be called and the result will be added to the
        /// views collection of the region. 
        /// </summary>
        /// <param name="regionManager">The regionmanager that this extension method effects.</param>
        /// <param name="regionName">The name of the region to associate the view with.</param>
        /// <param name="getContentDelegate">The delegate used to resolve a concreate instance of the view.</param>
        /// <returns>The regionmanager, for adding several views easily</returns>
        public static IRegionManager RegisterViewWithRegion(this IRegionManager regionManager, string regionName, Func<IView> getContentDelegate)
        {
            var regionViewRegistry = ServiceLocator.Current.GetInstance<IRegionViewRegistry>();
            regionViewRegistry.RegisterViewWithRegion(regionName, getContentDelegate);

            return regionManager;
        }

        /// <summary>
        /// Adds a region to the regionmanager with the name received as argument.
        /// </summary>
        /// <param name="regionCollection">The regionmanager's collection of regions.</param>
        /// <param name="regionName">The name to be given to the region.</param>
        /// <param name="region">The region to be added to the regionmanager.</param>        
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="region"/> or <paramref name="regionCollection"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">Thrown if <paramref name="regionName"/> and <paramref name="region"/>'s name do not match and the <paramref name="region"/> <see cref="IRegion.Name"/> is not <see langword="null"/>.</exception>
        public static void Add(this IRegionCollection regionCollection, string regionName, IRegion region)
        {
            if (region == null) throw new ArgumentNullException("region");
            if (regionCollection == null) throw new ArgumentNullException("regionCollection");

            if (region.Name != null && region.Name != regionName)
            {
                throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Resources.RegionManagerWithDifferentNameException, region.Name, regionName), "regionName");
            }

            if (region.Name == null)
            {
                region.Name = regionName;
            }

            regionCollection.Add(region);
        }
        
    }
}
