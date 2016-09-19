using System;
using System.Linq;
using Bedrock.Regions;
using Bedrock.Regions.Behaviors;
using Bedrock.Tests.Mocks;
using Microsoft.Practices.ServiceLocation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bedrock.Tests.Regions
{
    [TestClass]
    public class RegionViewRegistryFixture
    {
        [TestMethod]
        public void CanRegisterContentAndRetrieveIt()
        {
            MockServiceLocator locator = GetServiceLocator(new[] { "MyRegion" });
            var registry = (IRegionViewRegistry)locator.GetInstance(typeof(IRegionViewRegistry));

            registry.RegisterViewWithRegion("MyRegion", typeof(MockView));
            var result = registry.GetContents("MyRegion");

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count());
            Assert.IsInstanceOfType(result.ElementAt(0), typeof(MockView));
        }

        [TestMethod]
        public void ShouldRaiseEventWhenAddingContent()
        {
            MockServiceLocator locator = GetServiceLocator(new[] { "MyRegion" });
            var listener = new MySubscriberClass();
            var registry = (IRegionViewRegistry)locator.GetInstance(typeof(IRegionViewRegistry));

            registry.ViewRegistered += listener.OnViewRegistered;

            registry.RegisterViewWithRegion("MyRegion", typeof(MockView));

            Assert.IsNotNull(listener.onViewRegisteredArguments);
            Assert.IsNotNull(listener.onViewRegisteredArguments.RegisteredView);

            var result = listener.onViewRegisteredArguments.Region;
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void CanRegisterContentAsDelegateAndRetrieveIt()
        {
            MockServiceLocator locator = GetServiceLocator(new[] { "MyRegion" });
            var registry = (IRegionViewRegistry)locator.GetInstance(typeof(IRegionViewRegistry));
            var content = new MockView();

            registry.RegisterViewWithRegion("MyRegion", () => content);
            var result = registry.GetContents("MyRegion");

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count());
            Assert.AreSame(content, result.ElementAt(0));
        }

        [TestMethod]
        public void ShouldNotPreventSubscribersFromBeingGarbageCollected()
        {
            var registry = new RegionViewRegistry(null);
            var subscriber = new MySubscriberClass();
            registry.ViewRegistered += subscriber.OnViewRegistered;

            WeakReference subscriberWeakReference = new WeakReference(subscriber);

            subscriber = null;
            GC.Collect();

            Assert.IsFalse(subscriberWeakReference.IsAlive);
        }

        [TestMethod]
        public void OnRegisterErrorShouldGiveClearException()
        {
            MockServiceLocator locator = GetServiceLocator(new[] { "R1" });
            var registry = (IRegionViewRegistry)locator.GetInstance(typeof(IRegionViewRegistry));
            registry.ViewRegistered += new EventHandler<ViewRegisteredEventArgs>(FailWithInvalidOperationException);

            try
            {
                registry.RegisterViewWithRegion("R1", typeof(object));
                Assert.Fail();
            }
            catch (ViewRegistrationException ex)
            {
                Assert.IsTrue(ex.Message.Contains("Dont do this"));
                Assert.IsTrue(ex.Message.Contains("R1"));
                Assert.AreEqual(ex.InnerException.Message, "Dont do this");
            }
            catch (Exception)
            {
                Assert.Fail("Wrong exception type");
            }
        }

        [TestMethod]
        public void OnRegisterErrorShouldSkipFrameworkExceptions()
        {
            ExceptionExtensions.RegisterFrameworkExceptionType(typeof(FrameworkException));
            MockServiceLocator locator = GetServiceLocator(new[] { "R1" });
            var registry = (IRegionViewRegistry)locator.GetInstance(typeof(IRegionViewRegistry));
            registry.ViewRegistered += new EventHandler<ViewRegisteredEventArgs>(FailWithFrameworkException);

            try
            {
                registry.RegisterViewWithRegion("R1", typeof(object));
                Assert.Fail();
            }
            catch (ViewRegistrationException ex)
            {
                Assert.IsTrue(ex.Message.Contains("Dont do this"));
                Assert.IsTrue(ex.Message.Contains("R1"));
            }
            catch (Exception)
            {
                Assert.Fail("Wrong exception type");
            }
        }

        public void FailWithFrameworkException(object sender, ViewRegisteredEventArgs e)
        {
            try
            {
                FailWithInvalidOperationException(sender, e);
            }
            catch (Exception ex)
            {

                throw new FrameworkException(ex);
            }
        }

        public void FailWithInvalidOperationException(object sender, ViewRegisteredEventArgs e)
        {
            throw new InvalidOperationException("Dont do this");
        }

        private MockServiceLocator GetServiceLocator(string[] defaultRegions)
        {
            MockServiceLocator locator = new MockServiceLocator();
            var regionViewRegistry = new RegionViewRegistry(locator);
            var behavior = new AutoPopulateRegionBehavior(regionViewRegistry);
            var regionBehaviorFactory = new RegionBehaviorFactory(locator);
            regionBehaviorFactory.AddIfMissing(AutoPopulateRegionBehavior.BehaviorKey, typeof(AutoPopulateRegionBehavior));
            var regionManager = new RegionManager(regionBehaviorFactory);
            
            locator.GetInstance = (type) =>
            {
                if (type == typeof(IRegionManager))
                {
                    return regionManager;
                }
                if (type == typeof(IRegionViewRegistry))
                {
                    return regionViewRegistry;
                }
                if (type == typeof(AutoPopulateRegionBehavior))
                {
                    return behavior;
                }
                if (type == typeof(IRegionBehaviorFactory))
                {
                    return regionBehaviorFactory;
                }
                if (type == typeof(IServiceLocator))
                {
                    return locator;
                }
                if (type == typeof(MockView))
                {
                    return new MockView();
                }
                if (type == typeof (object))
                {
                    return new object();
                }
                return null;
            };

            foreach (var region in defaultRegions)
            {
                regionManager.Regions.Add(new MockRegion { Name = region });
            }

            return locator;
        }

        public class MySubscriberClass
        {
            public ViewRegisteredEventArgs onViewRegisteredArguments;
            public void OnViewRegistered(object sender, ViewRegisteredEventArgs e)
            {
                onViewRegisteredArguments = e;
            }
        }

        class FrameworkException : Exception
        {
            public FrameworkException(Exception innerException)
                : base("", innerException)
            {

            }
        }

    }
}
