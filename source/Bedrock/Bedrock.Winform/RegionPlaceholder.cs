using System;
using System.ComponentModel;
using System.Windows.Forms;
using Bedrock.Regions;
using Bedrock.Views;
using Microsoft.Practices.ServiceLocation;

namespace Bedrock.Winform
{
    public partial class RegionPlaceholder : UserControl, IRegion
    {
        private readonly Region _region = new Region();
        public RegionPlaceholder()
        {
            InitializeComponent();
            if (!DesignMode)
            {
                try
                {
                    //todo: why this line will throw exception when this control be dragged into a form.
                    RegionManager = ServiceLocator.Current.GetInstance<IRegionManager>();
                    RegionManager.Regions.Add(this);
                }
                catch (Exception)
                {
                    // ignored
                }
            }
        }

        #region implements IRegion
        public event PropertyChangedEventHandler PropertyChanged;

        public IViewsCollection Views
        {
            get { return _region.Views; }
        }

        public IViewsCollection ActiveViews
        {
            get { return _region.ActiveViews; }
        }

        public object Context
        {
            get { return _region.Context; }
            set { _region.Context = value; }
        }

        public IRegionManager RegionManager
        {
            get { return _region.RegionManager; }
            set { _region.RegionManager = value; }
        }

        public IRegionManager Add(IView view)
        {
            return _region.Add(view);
        }

        public IRegionManager Add(IView view, string viewName)
        {
            return _region.Add(view, viewName);
        }

        public IRegionManager Add(IView view, string viewName, bool createRegionManagerScope)
        {
            return _region.Add(view, viewName, createRegionManagerScope);
        }

        public void Remove(IView view)
        {
            _region.Remove(view);
        }

        public void Activate(IView view)
        {
            _region.Activate(view);
        }

        public void Deactivate(IView view)
        {
            _region.Deactivate(view);
        }

        public object GetView(string viewName)
        {
            return _region.GetView(viewName);
        }

        public IRegionBehaviorCollection Behaviors { get { return _region.Behaviors; } }

        #endregion
    }
}
