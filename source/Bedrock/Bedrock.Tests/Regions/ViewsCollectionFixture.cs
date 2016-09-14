using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using Bedrock.Regions;
using Bedrock.Tests.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bedrock.Views;

namespace Bedrock.Tests.Regions
{
    [TestClass]
    public class ViewsCollectionFixture
    {
        [TestMethod]
        public void CanWrapCollectionCollection()
        {
            var originalCollection = new ObservableCollection<IView>();
            IViewsCollection viewsCollection = new ViewsCollection(originalCollection);

            Assert.AreEqual(0, viewsCollection.Count());

            var item = new MockView();
            originalCollection.Add(item);
            Assert.AreEqual(1, viewsCollection.Count());
            Assert.AreSame(item, viewsCollection.First());
        }

        [TestMethod]
        public void CanFilterCollection()
        {
            var originalCollection = new ObservableCollection<IView>();
            IViewsCollection viewsCollection = new ViewsCollection(originalCollection);

            originalCollection.Add(new MockView());

            Assert.AreEqual(0, viewsCollection.Count());

            var item = new MockView();
            originalCollection.Add(item);
            Assert.AreEqual(1, viewsCollection.Count());

            Assert.AreSame(item, viewsCollection.First());
        }

        [TestMethod]
        public void RaisesCollectionChangedWhenFilteredCollectionChanges()
        {
            var originalCollection = new ObservableCollection<IView>();
            IViewsCollection viewsCollection = new ViewsCollection(originalCollection);
            bool collectionChanged = false;
            viewsCollection.CollectionChanged += (s, e) => collectionChanged = true;

            originalCollection.Add(new MockView());

            Assert.IsTrue(collectionChanged);
        }

        [TestMethod]
        public void RaisesCollectionChangedWithAddAndRemoveWhenFilteredCollectionChanges()
        {
            var originalCollection = new ObservableCollection<IView>();
            IViewsCollection viewsCollection = new ViewsCollection(originalCollection);
            bool addedToCollection = false;
            bool removedFromCollection = false;
            viewsCollection.CollectionChanged += (s, e) =>
                                                     {
                                                         if (e.Action == NotifyCollectionChangedAction.Add)
                                                         {
                                                             addedToCollection = true;
                                                         }
                                                         else if (e.Action == NotifyCollectionChangedAction.Remove)
                                                         {
                                                             removedFromCollection = true;
                                                         }
                                                     };
            var filteredInObject = new MockView();

            originalCollection.Add(filteredInObject);

            Assert.IsTrue(addedToCollection);
            Assert.IsFalse(removedFromCollection);

            originalCollection.Remove(filteredInObject);

            Assert.IsTrue(removedFromCollection);
        }

        [TestMethod]
        public void DoesNotRaiseCollectionChangedWhenAddingOrRemovingFilteredOutObject()
        {
            var originalCollection = new ObservableCollection<IView>();
            IViewsCollection viewsCollection = new ViewsCollection(originalCollection);
            bool collectionChanged = false;
            viewsCollection.CollectionChanged += (s, e) => collectionChanged = true;
            var filteredOutObject = new MockView();

            originalCollection.Add(filteredOutObject);
            originalCollection.Remove(filteredOutObject);

            Assert.IsFalse(collectionChanged);
        }
    }


}