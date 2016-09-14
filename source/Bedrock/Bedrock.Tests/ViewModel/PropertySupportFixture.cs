using System;
using System.Linq.Expressions;
using Bedrock.TestSupport;
using Bedrock.ViewModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bedrock.Tests.ViewModel
{
    [TestClass]
    public class PropertySupportFixture
    {
        [TestMethod]
        public virtual void WhenExtractingNameFromAValidPropertyExpression_ThenPropertyNameReturned()
        {
            var propertyName = PropertySupport.ExtractPropertyName(() => this.InstanceProperty);
            Assert.AreEqual("InstanceProperty", propertyName);
        }

        [TestMethod]
        public void WhenExpressionRepresentsAStaticProperty_ThenExceptionThrown()
        {
            ExceptionAssert.Throws<ArgumentException>(() => PropertySupport.ExtractPropertyName(() => StaticProperty));
        }

        [TestMethod]
        public void WhenExpressionIsNull_ThenAnExceptionIsThrown()
        {
            ExceptionAssert.Throws<ArgumentNullException>(() => PropertySupport.ExtractPropertyName<int>(null));
        }

        [TestMethod]
        public void WhenExpressionRepresentsANonMemberAccessExpression_ThenAnExceptionIsThrown()
        {
            ExceptionAssert.Throws<ArgumentException>(
                () => PropertySupport.ExtractPropertyName(() => this.GetHashCode())
                );

        }

        [TestMethod]
        public void WhenExpressionRepresentsANonPropertyMemberAccessExpression_ThenAnExceptionIsThrown()
        {
            ExceptionAssert.Throws<ArgumentException>(() => PropertySupport.ExtractPropertyName(() => this.InstanceField));
        }

        public static int StaticProperty { get; set; }
        public int InstanceProperty { get; set; }
        public int InstanceField;
        public static int SetOnlyStaticProperty { set { } }
    }
}
