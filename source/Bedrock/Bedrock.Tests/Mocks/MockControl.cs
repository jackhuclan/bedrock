using System;

namespace Bedrock.Tests.Mocks
{
    public class MockControl
    {
        private string _name;

        public string Name
        {
            get
            {
                if (string.IsNullOrEmpty(_name))
                {
                    _name = Guid.NewGuid().ToString();
                }

                return _name;
            }
            set { _name = value; }
        }
    }
}
