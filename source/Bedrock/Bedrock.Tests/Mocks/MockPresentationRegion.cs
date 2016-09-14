using System;
using System.ComponentModel;
using Bedrock.Regions;
using Bedrock.Views;

namespace Bedrock.Tests.Mocks
{
    class MockPresentationRegion : IRegion
    {
        public MockViewsCollection MockViews = new MockViewsCollection();
        public MockViewsCollection MockActiveViews = new MockViewsCollection();

        public MockPresentationRegion()
        {
            Behaviors = new MockRegionBehaviorCollection();
        }

        public object Control { get; set; }

        public IRegionManager Add(IView view)
        {
            MockViews.Items.Add(view);

            return null;
        }

        public void RemoveAllViews()
        {
            throw new NotImplementedException();
        }

        public void Remove(IView view)
        {
            MockViews.Items.Remove(view);
            MockActiveViews.Items.Remove(view);
        }

        public void Activate(IView view)
        {
            MockActiveViews.Items.Add(view);
        }

        public void Activate()
        {
            throw new NotImplementedException();
        }

        public void Deactivate(IView view)
        {
            throw new NotImplementedException();
        }

        public void Deactivate()
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

        public void RegisterDefaultBehavior()
        {
            throw new NotImplementedException();
        }

        public object GetView(string viewName)
        {
            throw new NotImplementedException();
        }

        public IRegionManager RegionManager { get; set; }

        public IRegionBehaviorCollection Behaviors { get; set; }

        public IViewsCollection Views
        {
            get { return MockViews; }
        }

        public IViewsCollection ActiveViews
        {
            get { return MockActiveViews; }
        }

        public void Deactivate(object view)
        {
            MockActiveViews.Items.Remove(view);
        }

        private object context;
        public object Context
        {
            get { return context; }
            set
            {
                context = value;
                OnPropertyChange("Context");
            }
        }

        private string name;
        public string Name
        {
            get { return this.name; }
            set
            {
                this.name = value;
                this.OnPropertyChange("Name");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChange(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public bool Navigate(Uri source)
        {
            throw new NotImplementedException();
        }

        public Comparison<object> SortComparison
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