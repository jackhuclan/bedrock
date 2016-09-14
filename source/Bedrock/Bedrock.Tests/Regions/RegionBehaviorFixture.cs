using System;
using Bedrock.Regions;
using Bedrock.Tests.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bedrock.Tests.Regions
{
    [TestClass]
    public class RegionBehaviorFixture
    {
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void CannotChangeRegionAfterAttach()
        {
            TestableRegionBehavior regionBehavior = new TestableRegionBehavior();

            regionBehavior.Region = new MockPresentationRegion();

            regionBehavior.Attach();
            regionBehavior.Region = new MockPresentationRegion();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void ShouldFailWhenAttachedWithoutRegion()
        {
            TestableRegionBehavior regionBehavior = new TestableRegionBehavior();
            regionBehavior.Attach();
        }

        [TestMethod]
        public void ShouldCallOnAttachWhenAttachMethodIsInvoked()
        {
            TestableRegionBehavior regionBehavior = new TestableRegionBehavior();

            regionBehavior.Region = new MockPresentationRegion();

            regionBehavior.Attach();

            Assert.IsTrue(regionBehavior.onAttachCalled);
        }

        private class TestableRegionBehavior : RegionBehavior
        {
            public bool onAttachCalled;

            protected override void OnAttach()
            {
                onAttachCalled = true;
            }
        }
    }

    
}
