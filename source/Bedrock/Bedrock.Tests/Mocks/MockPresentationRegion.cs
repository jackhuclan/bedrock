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
            return RegionManager;
        }

        public void RemoveAllViews()
        {
            MockViews = new MockViewsCollection();
            MockActiveViews = new MockViewsCollection();
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
            foreach (var view in Views)
            {
                Activate(view);
            }
        }
        
        public void Deactivate()
        {
            foreach (var view in MockActiveViews)
            {
                Deactivate(view);
            }
        }

        public IRegionManager Add(IView view, string viewName)
        {
            return this.Add(view, viewName, false);
        }

        public IRegionManager Add(IView view, string viewName, bool createRegionManagerScope)
        {
            return this.Add(view, viewName, false);
        }

        public void RegisterDefaultBehavior()
        {
            throw new NotImplementedException();
        }

        public IView GetView(string viewName)
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

        public void Deactivate(IView view)
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
    }
}