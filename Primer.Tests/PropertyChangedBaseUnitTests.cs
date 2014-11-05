using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Primer.Tests
{
    [TestClass]
    public class PropertyChangedBaseUnitTests
    {

        [TestMethod]
        [TestCategory("PropertyChangedBase")]
        public void RaisePropertyChanged_UsingPropertyNameString_RaisesPropertyChangedEvent()
        {

            // Arrange
            var mockPropertyChangedBase = new Mock<PropertyChangedBase>() { CallBase = true };
            var eventRaised = false;
            var propertyChangedBase = mockPropertyChangedBase.Object;
            propertyChangedBase.PropertyChanged += (s, p) => eventRaised = true;


            // Action
            propertyChangedBase.RaisePropertyChanged(new object(), "TheNameOfAPropertyThatHasChanged");


            // Assert
            Assert.IsTrue(eventRaised);

        }



        [TestMethod]
        [TestCategory("PropertyChangedBase")]
        public void RaisePropertyChanged_UsingPropertyExpression_RaisesPropertyChangedEvent()
        {

            // Arrange
            var mockPropertyChangedBase = new Mock<PropertyChangedBase>() { CallBase = true };
            var eventRaised = false;
            var testObject = new String('A', 1);
            var propertyChangedBase = mockPropertyChangedBase.Object;
            propertyChangedBase.PropertyChanged += (s, p) => eventRaised = true;


            // Action
            propertyChangedBase.RaisePropertyChanged(() => testObject.Length);


            // Assert
            Assert.IsTrue(eventRaised);

        }



        [TestMethod]
        [TestCategory("PropertyChangedBase")]
        public void RaisePropertyChanged_UsingPropertyExpressionAndSender_RaisesPropertyChangedEvent()
        {

            // Arrange
            var mockPropertyChangedBase = new Mock<PropertyChangedBase>() { CallBase = true };
            var eventRaised = false;
            var testObject = new String('A', 1);
            var propertyChangedBase = mockPropertyChangedBase.Object;
            propertyChangedBase.PropertyChanged += (s, p) => eventRaised = true;


            // Action
            propertyChangedBase.RaisePropertyChanged(new object(), () => testObject.Length);


            // Assert
            Assert.IsTrue(eventRaised);

        }



        [TestMethod]
        [TestCategory("PropertyChangedBase")]
        [ExpectedException(typeof(ArgumentException), "An 'ArgumentException' should have been thrown.")]
        public void RaisePropertyChanged_UsingEmptyPropertyNameString_ThrowsException()
        {

            // Arrange
            var mockPropertyChangedBase = new Mock<PropertyChangedBase>() { CallBase = true };
            var propertyChangedBase = mockPropertyChangedBase.Object;


            // Action
            propertyChangedBase.RaisePropertyChanged(new object(), string.Empty);


            // Assert - Exception

        }

    }
}
