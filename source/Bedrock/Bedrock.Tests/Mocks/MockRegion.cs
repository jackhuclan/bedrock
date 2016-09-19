using System;
using System.ComponentModel;
using Bedrock.Regions;
using Bedrock.Regions.Behaviors;
using Bedrock.Views;

namespace Bedrock.Tests.Mocks
{
    internal class MockRegion : IRegion
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public Func<string, IView> GetViewStringDelegate { get; set; }

        private MockViewsCollection views = new MockViewsCollection();

        public MockRegion()
        {
            this.Behaviors = new RegionBehaviorCollection(this);
        }

        public IViewsCollection Views
        {
            get { return views; }
        }

        public IViewsCollection ActiveViews
        {
            get { throw new System.NotImplementedException(); }
        }

        public object Context { get; set; }

        public string Name { get; set; }

        public IRegionManager Add(IView view)
        {
            this.views.Add(view);
            return null;
        }

        public IRegionManager Add(IView view, string viewName)
        {
            return Add(view);
        }

        public IRegionManager Add(object view, string viewName, bool createRegionManagerScope)
        {
            throw new System.NotImplementedException();
        }

        public void Remove(IView view)
        {
            throw new System.NotImplementedException();
        }

        public void Activate(IView view)
        {
            throw new System.NotImplementedException();
        }

        public void Deactivate(IView view)
        {
            throw new System.NotImplementedException();
        }

        public IView GetView(string viewName)
        {
            return GetViewStringDelegate(viewName);
        }

        public IRegionManager RegionManager { get; set; }

        public IRegionBehaviorCollection Behaviors { get; set; }
        public bool Navigate(System.Uri source)
        {
            throw new System.NotImplementedException();
        }


        public IRegionManager Add(IView view, string viewName, bool createRegionManagerScope)
        {
            throw new NotImplementedException();
        }
        
        public void RemoveAllViews()
        {
            throw new NotImplementedException();
        }


        public void Activate()
        {
            throw new NotImplementedException();
        }

        public void Deactivate()
        {
            throw new NotImplementedException();
        }

        public System.Comparison<object> SortComparison
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
                throw new System.NotImplementedException();
            }
        }

        public object Control
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }
    }
}
