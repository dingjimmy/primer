using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Primer.Tests
{
    [TestClass]
    public class ViewModelTests
    {

        [TestMethod]
        [TestCategory("ViewModel")]
        public void RaisePropertyChanged_UsingPropertyNameString_RaisesPropertyChangedEvent()
        {

            // Arrange
            var viewModel = new ViewModel();
            var eventRaised = false;
            viewModel.PropertyChanged += (s, p) => eventRaised = true;


            // Action
            viewModel.RaisePropertyChanged("TheNameOfAPropertyThatHasChanged");


            // Assert
            Assert.IsTrue(eventRaised);
           
        }


        [TestMethod]
        [TestCategory("ViewModel")]
        public void RaisePropertyChanged_UsingPropertyNameStringAndSender_RaisesPropertyChangedEvent()
        {

            // Arrange
            var viewModel = new ViewModel();
            var eventRaised = false;
            viewModel.PropertyChanged += (s, p) => eventRaised = true;


            // Action
            viewModel.RaisePropertyChanged(new object(), "TheNameOfAPropertyThatHasChanged");


            // Assert
            Assert.IsTrue(eventRaised);

        }



        [TestMethod]
        [TestCategory("ViewModel")]
        [ExpectedException(typeof(ArgumentException), "An 'ArgumentException' should have been thrown.")]
        public void RaisePropertyChanged_UsingEmptyPropertyNameString_ThrowsException()
        {

            // Arrange
            var viewModel = new ViewModel();


            // Action
            viewModel.RaisePropertyChanged(new object(), string.Empty);


            // Assert - Exception

        }



        [TestMethod]
        [TestCategory("ViewModel")]
        public void SetProperty_WhenCurrentAndProposedAreDifferent_SetsCurrentToProposedValue()
        {

            // Arrange
            var viewModel = new ViewModel();
            var currentValue = "Current Value";


            // Action
            viewModel.SetProperty(() => viewModel.DisplayName, ref currentValue, "Proposed Value");


            // Assert
            Assert.AreEqual("Proposed Value", currentValue);

        }


        [TestMethod]
        [TestCategory("ViewModel")]
        public void SetProperty_WhenCurrentAndProposedAreDifferent_RaisesPropertyChangedEvent()
        {

            // Arrange
            var viewModel = Mock.Of<ViewModel>();
            Mock.Get(viewModel).CallBase = true;
            var currentValue = "Current Value";


            // Action
            viewModel.SetProperty(() => viewModel.DisplayName, ref currentValue, "Proposed Value");


            // Assert
            Mock.Get(viewModel).Verify((vm) => vm.RaisePropertyChanged(It.IsAny<string>()), Times.Once);

        }


        [TestMethod]
        [TestCategory("ViewModel")]
        public void SetProperty_WhenCurrentAndProposedAreSameButForceUpdateIsTrue_RaisesPropertyChangedEvent()
        {

            // Arrange
            var viewModel = Mock.Of<ViewModel>();
            Mock.Get(viewModel).CallBase = true;
            var currentValue = "Current Value";


            // Action
            viewModel.SetProperty(() => viewModel.DisplayName, ref currentValue, "Current Value", true);


            // Assert
            Mock.Get(viewModel).Verify((vm) => vm.RaisePropertyChanged(It.IsAny<string>()), Times.Once);

        }


        [TestMethod]
        [TestCategory("ViewModel")]
        public void SetProperty_WhenCurrentAndProposedAreSame_DoesNotRaisePropertyChangedEvent()
        {

            // Arrange
            var viewModel = Mock.Of<ViewModel>();
            Mock.Get(viewModel).CallBase = true;
            var currentValue = "Current Value";


            // Action
            viewModel.SetProperty(() => viewModel.DisplayName, ref currentValue, "Current Value");


            // Assert
            Mock.Get(viewModel).Verify((vm) => vm.RaisePropertyChanged(It.IsAny<string>()), Times.Never);

        }



        [TestMethod]
        [TestCategory("ViewModel")]
        public void SetProperty_WhenCurrentAndProposedAreDifferent_BroadcastsPropertyChangedMessage()
        {

            // Arrange
            var channel = Mock.Of<IMessagingChannel>();
            var viewModel = new ViewModel();
            viewModel.Channel = channel;
            var currentValue = "Current Value";


            // Action
            viewModel.SetProperty(() => viewModel.DisplayName, ref currentValue, "Proposed Value");


            // Assert
            Mock.Get(channel).Verify((c) => c.Broadcast(It.IsAny<PropertyChangedMessage>()));

        }

    }
}
