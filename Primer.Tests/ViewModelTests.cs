//// Copyright (c) James Dingle

//using System;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using Moq;

//namespace Primer.Tests
//{
//    [TestClass]
//    public class ViewModelTests
//    {

//        [TestMethod]
//        [TestCategory("Primer.ViewModel")]
//        public void UpdateProperty_Updates_Property_Value_When_ForceUpdate_Is_True()
//        {

//            // Arrange
//            var currentValue = 12345;
//            var proposedValue = 12345;
//            var vm = Mock.Of<ViewModel<>();
//            Mock.Get(vm).CallBase = true;


//            // Action
//            var hasUpdated = vm.UpdateProperty("TestProperty", ref currentValue, proposedValue, true);


//            // Assert
//            Assert.IsTrue(hasUpdated);

//        }



//        [TestMethod]
//        [TestCategory("Primer.ViewModel")]
//        public void UpdateProperty_Updates_Property_Value_When_ForceUpdate_Is_False_And_Values_Are_Different()
//        {

//            // Arrange
//            var currentValue = 12345;
//            var proposedValue = 6789;
//            var vm = Mock.Of<ViewModel>();
//            Mock.Get(vm).CallBase = true;


//            // Action
//            var hasUpdated = vm.UpdateProperty("TestProperty", ref currentValue, proposedValue, false);


//            // Assert
//            Assert.IsTrue(hasUpdated);

//        }



//        [TestMethod]
//        [TestCategory("Primer.ViewModel")]
//        public void UpdateProperty_Does_Not_Update_Property_Value_When_ForceUpdate_Is_False_And_Values_Are_Same()
//        {

//            // Arrange
//            var currentValue = 12345;
//            var proposedValue = 12345;
//            var vm = Mock.Of<ViewModel>();
//            Mock.Get(vm).CallBase = true;


//            // Action
//            var hasUpdated = vm.UpdateProperty("TestProperty", ref currentValue, proposedValue, false);


//            // Assert
//            Assert.IsFalse(hasUpdated);

//        }
//    }
//}
