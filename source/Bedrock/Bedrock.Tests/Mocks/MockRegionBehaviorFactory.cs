using System;
using System.Collections;
using System.Collections.Generic;
using Bedrock.Regions;
using Moq;

namespace Bedrock.Tests.Mocks
{
    internal class MockRegionBehaviorFactory : IRegionBehaviorFactory
    {
        private Dictionary<string, Type> behaviorTypes = new Dictionary<string, Type>();
         
        public IEnumerator<string> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void AddIfMissing(string behaviorKey, Type behaviorType)
        {
            if (!behaviorTypes.ContainsKey(behaviorKey))
            {
                behaviorTypes.Add(behaviorKey, behaviorType);
            }
        }

        public bool ContainsKey(string behaviorKey)
        {
            return behaviorTypes.ContainsKey(behaviorKey);
        }

        public IRegionBehavior CreateFromKey(string key)
        {
            if (!behaviorTypes.ContainsKey(key))
            {
                behaviorTypes.Add(key, typeof(MockRegionBehavior));
            }

            return (IRegionBehavior)Activator.CreateInstance(behaviorTypes[key]);
        }
    }
}
