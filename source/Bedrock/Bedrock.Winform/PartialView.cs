using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Bedrock.Regions;
using Bedrock.Views;
using Microsoft.Practices.ServiceLocation;
using Region = Bedrock.Regions.Region;
// ReSharper disable VirtualMemberCallInContructor

namespace Bedrock.Winform
{
    public partial class PartialView : UserControl, IView
    {
        private IRegionManager _regionManager;
        public PartialView()
        {
            InitializeComponent();
        }

        public object DataContext { get; set; }

        #region region operations

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            InitializeRegions();
        }

        public virtual void InitializeRegions()
        {
        }

        public IRegionManager RegionManager
        {
            get
            {
                if (_regionManager == null)
                {
                    _regionManager = ServiceLocator.Current.GetInstance<IRegionManager>();
                }

                return _regionManager;
            }
            set { _regionManager = value; }
        }

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
        #endregion
    }
}
