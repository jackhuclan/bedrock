using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Bedrock.Regions;

namespace Bedrock.Tests.Mocks
{
    internal class MockRegionManager : IRegionManager
    {
        private IRegionCollection regions = new MockRegionCollection();
        internal MockRegionCollection MockRegionCollection
        {
            get
            {
                return regions as MockRegionCollection;
            }
        }

        public IRegionCollection Regions
        {
            get { return regions; }
        }

        public IRegionManager CreateRegionManager()
        {
            return new MockRegionManager();
        }

    }

    internal class MockRegionCollection : List<IRegion>, IRegionCollection
    {
        IEnumerator<IRegion> IEnumerable<IRegion>.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IRegion this[string regionName]
        {
            get { return this.FirstOrDefault(x => x.Name == regionName); }
        }

        void IRegionCollection.Add(IRegion region)
        {
            Add(region);
        }

        public bool Remove(string regionName)
        {
            throw new System.NotImplementedException();
        }

        public bool ContainsRegionWithName(string regionName)
        {
            if (GetRegionByName(regionName) == null)
                return false;
            return this.Contains(GetRegionByName(regionName));
        }

        public IRegion GetRegionByName(string regionName)
        {
            return this[regionName];
        }

        public event System.Collections.Specialized.NotifyCollectionChangedEventHandler CollectionChanged;
    }
}
