// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System;
using System.Collections.Specialized;
using System.Linq;
using Bedrock.Regions;
using Bedrock.Tests.Mocks;
using Bedrock.Views;
using Microsoft.Practices.ServiceLocation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Bedrock.Tests.Regions
{
    [TestClass]
    public class RegionFixture
    {

        [TestMethod]
        public void CanAddContentToRegion()
        {
            var control = new object();
            IRegion region = new RegionBase(control);

            region.Add(new MockView());

            Assert.AreEqual(1, region.Views.Cast<object>().Count());
        }


        [TestMethod]
        public void CanRemoveContentFromRegion()
        {
            var control = new object();
            IRegion region = new RegionBase(control);
            IView view = new MockView();

            region.Add(view);
            region.Remove(view);

            Assert.AreEqual(0, region.Views.Cast<object>().Count());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void RemoveInexistentViewThrows()
        {
            var control = new object();
            IRegion region = new RegionBase(control);
            IView view = new MockView();

            region.Remove(view);

            Assert.AreEqual(0, region.Views.Cast<object>().Count());
        }

        [TestMethod]
        public void RegionExposesCollectionOfContainedViews()
        {
            var control = new object();
            IRegion region = new RegionBase(control);
            IView view = new MockView();

            region.Add(view);

            var views = region.Views;

            Assert.IsNotNull(views);
            Assert.AreEqual(1, views.Cast<object>().Count());
            Assert.AreSame(view, views.Cast<object>().ElementAt(0));
        }

        [TestMethod]
        public void CanAddAndRetrieveNamedViewInstance()
        {
            var control = new object();
            IRegion region = new RegionBase(control);
            IView myView = new MockView();
            region.Add(myView, "MyView");
            object returnedView = region.GetView("MyView");

            Assert.IsNotNull(returnedView);
            Assert.AreSame(returnedView, myView);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void AddingDuplicateNamedViewThrows()
        {
            var control = new object();
            IRegion region = new RegionBase(control);
            IView myView = new MockView();

            region.Add(myView, "MyView");
            region.Add(myView, "MyView");
        }

        [TestMethod]
        public void AddNamedViewIsAlsoListedInViewsCollection()
        {
            var control = new object();
            IRegion region = new RegionBase(control);
            IView myView = new MockView();

            region.Add(myView, "MyView");

            Assert.AreEqual(1, region.Views.Cast<object>().Count());
            Assert.AreSame(myView, region.Views.Cast<object>().ElementAt(0));
        }

        [TestMethod]
        public void GetViewReturnsNullWhenViewDoesNotExistInRegion()
        {
            var control = new object();
            IRegion region = new RegionBase(control);
            IView myView = new MockView();

            Assert.IsNull(region.GetView("InexistentView"));
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetViewWithNullOrEmptyStringThrows()
        {
            var control = new object();
            IRegion region = new RegionBase(control);
            IView myView = new MockView();

            region.GetView(string.Empty);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AddNamedViewWithNullOrEmptyStringNameThrows()
        {
            var control = new object();
            IRegion region = new RegionBase(control);
            IView myView = new MockView();

            region.Add(myView, string.Empty);
        }

        [TestMethod]
        public void GetViewReturnsNullAfterRemovingViewFromRegion()
        {
            var control = new object();
            IRegion region = new RegionBase(control);
            IView myView = new MockView();

            region.Add(myView, "MyView");
            region.Remove(myView);

            Assert.IsNull(region.GetView("MyView"));
        }


        [TestMethod]
        public void CreatingNewScopesAsksTheRegionManagerForNewInstance()
        {
            var regionManager = new MockRegionManager();
            var control = new object();
            IRegion region = new RegionBase(control);
            region.RegionManager = regionManager;
            var myView = new MockView();

            region.Add(myView, "MyView", true);

            Assert.IsTrue(regionManager.CreateRegionManagerCalled);
        }

        [TestMethod]
        public void AddViewReturnsExistingRegionManager()
        {
            var regionManager = new MockRegionManager();
            var control = new object();
            IRegion region = new RegionBase(control);
            region.RegionManager = regionManager;
            var myView = new MockView();

            var returnedRegionManager = region.Add(myView, "MyView", false);

            Assert.AreSame(regionManager, returnedRegionManager);
        }

        [TestMethod]
        public void AddViewReturnsNewRegionManager()
        {
            var regionManager = new MockRegionManager();
            var control = new object();
            IRegion region = new RegionBase(control);
            region.RegionManager = regionManager;
            var myView = new MockView();

            var returnedRegionManager = region.Add(myView, "MyView", true);

            Assert.AreNotSame(regionManager, returnedRegionManager);
        }

        [TestMethod]
        public void AddingNonDependencyObjectToRegionDoesNotThrow()
        {
            var regionManager = new MockRegionManager();
            var control = new object();
            IRegion region = new RegionBase(control);
            region.RegionManager = regionManager;
            var myView = new MockView();

            region.Add(myView);

            Assert.AreEqual(1, region.Views.Cast<object>().Count());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ActivateNonAddedViewThrows()
        {
            var regionManager = new MockRegionManager();
            var control = new object();
            IRegion region = new RegionBase(control);
            region.RegionManager = regionManager;
            IView nonAddedView = new MockView();

            region.Activate(nonAddedView);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void DeactivateNonAddedViewThrows()
        {
            var regionManager = new MockRegionManager();
            var control = new object();
            IRegion region = new RegionBase(control);
            region.RegionManager = regionManager;
            IView nonAddedView = new MockView();

            region.Deactivate(nonAddedView);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ActivateNullViewThrows()
        {
            var regionManager = new MockRegionManager();
            var control = new object();
            IRegion region = new RegionBase(control);
            region.RegionManager = regionManager;
            IView nonAddedView = new MockView();

            region.Activate(null);
        }

        [TestMethod]
        public void AddViewRaisesCollectionViewEvent()
        {
            bool viewAddedCalled = false;

            var regionManager = new MockRegionManager();
            var control = new object();
            IRegion region = new RegionBase(control);
            region.RegionManager = regionManager;
            IView myView = new MockView();

            region.Views.CollectionChanged += (sender, e) =>
                                                  {
                                                      if (e.Action == NotifyCollectionChangedAction.Add)
                                                          viewAddedCalled = true;
                                                  };

            object model = new object();
            Assert.IsFalse(viewAddedCalled);
            region.Add(myView);

            Assert.IsTrue(viewAddedCalled);
        }

        [TestMethod]
        public void ViewAddedEventPassesTheViewAddedInTheEventArgs()
        {
            object viewAdded = null;

            var regionManager = new MockRegionManager();
            var control = new object();
            IRegion region = new RegionBase(control);
            region.RegionManager = regionManager;
            IView myView = new MockView();

            region.Views.CollectionChanged += (sender, e) =>
                                                  {
                                                      if (e.Action == NotifyCollectionChangedAction.Add)
                                                      {
                                                          viewAdded = e.NewItems[0];
                                                      }
                                                  };
            object model = new object();
            Assert.IsNull(viewAdded);
            region.Add(myView);

            Assert.IsNotNull(viewAdded);
            Assert.AreSame(model, viewAdded);
        }

        [TestMethod]
        public void RemoveViewFiresViewRemovedEvent()
        {
            bool viewRemoved = false;

            var regionManager = new MockRegionManager();
            var control = new object();
            IRegion region = new RegionBase(control);
            region.RegionManager = regionManager;
            IView myView = new MockView();

            region.Views.CollectionChanged += (sender, e) =>
                                                  {
                                                      if (e.Action == NotifyCollectionChangedAction.Remove)
                                                          viewRemoved = true;
                                                  };

            region.Add(myView);
            Assert.IsFalse(viewRemoved);

            region.Remove(myView);

            Assert.IsTrue(viewRemoved);
        }

        [TestMethod]
        public void ViewRemovedEventPassesTheViewRemovedInTheEventArgs()
        {
            object removedView = null;

            var regionManager = new MockRegionManager();
            var control = new object();
            IRegion region = new RegionBase(control);
            region.RegionManager = regionManager;
            IView myView = new MockView();

            region.Views.CollectionChanged += (sender, e) =>
                                                  {
                                                      if (e.Action == NotifyCollectionChangedAction.Remove)
                                                          removedView = e.OldItems[0];
                                                  };
            object model = new object();
            region.Add(myView);
            Assert.IsNull(removedView);

            region.Remove(myView);

            Assert.AreSame(model, removedView);
        }

        [TestMethod]
        public void ShowViewFiresViewShowedEvent()
        {
            bool viewActivated = false;

            var regionManager = new MockRegionManager();
            var control = new object();
            IRegion region = new RegionBase(control);
            region.RegionManager = regionManager;
            IView myView = new MockView();

            region.ActiveViews.CollectionChanged += (o, e) =>
                                                        {
                                                            if (e.Action == NotifyCollectionChangedAction.Add && e.NewItems.Contains(myView))
                                                                viewActivated = true;
                                                        };
            region.Add(myView);
            Assert.IsFalse(viewActivated);

            region.Activate(myView);

            Assert.IsTrue(viewActivated);
        }

        [TestMethod]
        public void AddingSameViewTwiceThrows()
        {
            var regionManager = new MockRegionManager();
            var control = new object();
            IRegion region = new RegionBase(control);
            region.RegionManager = regionManager;
            IView myView = new MockView();

            region.Add(myView);

            try
            {
                region.Add(myView);
                Assert.Fail();
            }
            catch (InvalidOperationException ex)
            {
                Assert.AreEqual("View already exists in region.", ex.Message);
            }
            catch
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void RemovingViewAlsoRemovesItFromActiveViews()
        {
            var regionManager = new MockRegionManager();
            var control = new object();
            IRegion region = new RegionBase(control);
            region.RegionManager = regionManager;
            IView myView = new MockView();

            region.Add(myView);
            region.Activate(myView);
            Assert.IsTrue(region.ActiveViews.Contains(myView));

            region.Remove(myView);

            Assert.IsFalse(region.ActiveViews.Contains(myView));
        }

        [TestMethod]
        public void ShouldGetNotificationWhenContextChanges()
        {
            var regionManager = new MockRegionManager();
            var control = new object();
            IRegion region = new RegionBase(control);
            region.RegionManager = regionManager;
            IView myView = new MockView();

            bool contextChanged = false;
            region.PropertyChanged += (s, args) => { if (args.PropertyName == "Context") contextChanged = true; };

            region.Context = "MyNewContext";

            Assert.IsTrue(contextChanged);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void ChangingNameOnceItIsSetThrows()
        {
            var regionManager = new MockRegionManager();
            var control = new object();
            IRegion region = new RegionBase(control);
            region.RegionManager = regionManager;

            region.Name = "MyRegion";

            region.Name = "ChangedRegionName";
        }

        private class MockRegionManager : IRegionManager
        {
            public bool CreateRegionManagerCalled;

            public IRegionManager CreateRegionManager()
            {
                CreateRegionManagerCalled = true;
                return new MockRegionManager();
            }

            public IRegionCollection Regions
            {
                get { throw new NotImplementedException(); }
            }

            public IRegion AttachNewRegion(object regionTarget, string regionName)
            {
                throw new NotImplementedException();
            }

            public bool Navigate(Uri source)
            {
                throw new NotImplementedException();
            }
        }
    }
}
