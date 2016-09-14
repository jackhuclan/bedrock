using System;
using Bedrock.Events;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bedrock.Tests.Events
{
    [TestClass]
    class MockDelegateReference : IDelegateReference
    {
        public Delegate Target { get; set; }

        public MockDelegateReference()
        {

        }

        public MockDelegateReference(Delegate target)
        {
            Target = target;
        }
    }
}