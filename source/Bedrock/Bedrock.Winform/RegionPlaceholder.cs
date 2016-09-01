using System.ComponentModel;
using System.Windows.Forms;
using Bedrock.Regions;
using Bedrock.Views;
using Microsoft.Practices.ServiceLocation;

namespace Bedrock.Winform
{
    public class RegionPlaceholder : UserControl, IRegion
    {
        private IContainer components;
        private readonly Region _region = new Region();

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

        #region Component Designer generated code

        public RegionPlaceholder()
        {
            InitializeComponent();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new Container();
            AutoScaleMode = AutoScaleMode.Font;
            RegionManager = ServiceLocator.Current.GetInstance<IRegionManager>();
            RegionManager.Regions.Add(this);
        }

        #endregion
    }
}
