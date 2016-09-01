using System;
using System.ComponentModel;
using System.Windows.Forms;
using Bedrock.Regions;
using Bedrock.Views;

namespace Bedrock.Winform
{
    namespace Bedrock.Winform
    {
        public class RegionPlaceholder : UserControl, IRegion
        {            
            private IContainer components = null;
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

            /// <summary> 
            /// Required method for Designer support - do not modify 
            /// the contents of this method with the code editor.
            /// </summary>
            private void InitializeComponent()
            {
                components = new System.ComponentModel.Container();
                this.AutoScaleMode = AutoScaleMode.Font;
            }

            public IRegionManager Add(IView view)
            {
                throw new NotImplementedException();
            }

            public IRegionManager Add(IView view, string viewName)
            {
                throw new NotImplementedException();
            }

            public IRegionManager Add(IView view, string viewName, bool createRegionManagerScope)
            {
                throw new NotImplementedException();
            }

            public void Remove(IView view)
            {
                throw new NotImplementedException();
            }

            public void Activate(IView view)
            {
                throw new NotImplementedException();
            }

            public void Deactivate(IView view)
            {
                throw new NotImplementedException();
            }

            public object GetView(string viewName)
            {
                throw new NotImplementedException();
            }

            #endregion
        }
    }

}
