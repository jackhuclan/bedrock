using System;
using Bedrock.Regions;

namespace Bedrock.Tests.Mocks
{
    public class MockRegionBehavior : IRegionBehavior
    {
        public const string BehaviorKey = "MockRegionBehavior";

        public IRegion Region
        {
            get; set;
        }

        public Func<object> OnAttach;

        public void Attach()
        {
            if (OnAttach != null)
                OnAttach();
            
        }
    }
}
