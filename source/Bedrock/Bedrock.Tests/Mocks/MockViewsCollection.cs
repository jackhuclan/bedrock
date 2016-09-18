using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using Bedrock.Regions;
using Bedrock.Views;

namespace Bedrock.Tests.Mocks
{
    class MockViewsCollection : IViewsCollection
    {
        public ObservableCollection<IView> Items = new ObservableCollection<IView>();

        public void Add(IView view)
        {
            this.Items.Add(view);
        }

        public void Remove(IView view)
        {
            throw new System.NotImplementedException();
        }

        public bool Contains(IView value)
        {
            return Items.Contains(value);
        }

        public IEnumerator<IView> GetEnumerator()
        {
            return Items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public event NotifyCollectionChangedEventHandler CollectionChanged
        {
            add { Items.CollectionChanged += value; }
            remove { Items.CollectionChanged -= value; }
        }
    }
}
