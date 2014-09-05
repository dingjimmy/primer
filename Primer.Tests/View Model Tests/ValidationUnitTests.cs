using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Primer.Tests.Widgets;
using Moq;
using FluentValidation;

namespace Primer.Tests
{
    [TestClass]
    public class ViewModelValidationUnitTests
    {

        [TestMethod]
        [TestCategory("Primer.ViewModel.Validation")]
        public void TestMethod1()
        {

            // arrange
            var vm = Mock.Of<ViewModel<Widget>>();
            var validator = Mock.Of<AbstractValidator<Widget>>();
            var failures = new[] {new FluentValidation.Results.ValidationFailure("Name", "Test Error")};
            var failureResult = new FluentValidation.Results.ValidationResult(failures);
            Mock.Get(vm).CallBase = true;
            Mock.Get(validator).Setup(v => v.Validate(It.IsAny<Widget>(), "Name")).Returns(failureResult);


            // action
            

            // assert

        }
    }
}

#region Widget Class Used For Testing

namespace Primer.Tests.Widgets
{

    class Widget
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Info { get; set; }
        public Widget(int id, string name)
        {
            this.ID = id;
            this.Name = name;
        }
    }

}

#endregion
