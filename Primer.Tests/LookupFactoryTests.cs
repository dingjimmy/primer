using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Primer.Tests
{
    [TestClass]
    public class LookupFactoryTests
    {
        [TestMethod]
        [TestCategory("Primer.LookupFactory")]
        public void From_Returns_Lookup_With_Correct_Number_Of_Items()
        {

            // arrange
            Widget[] widgets = { new Widget(101, "WidgetOne"), new Widget(102, "WidgetTwo"), new Widget(103, "WidgetThree"), new Widget(104, "WidgetFour"), new Widget(105, "WidgetFive")};
            var factory = new LookupFactory();


            // action
            var result = factory.From(widgets, w => w.ID.ToString(), w => w.Name, w => w);


            // assert
            Assert.IsNotNull(result);
            Assert.AreEqual(5, result.Count);

        }


        [TestMethod]
        [TestCategory("Primer.LookupFactory")]
        public void From_Returns_Lookup_With_Correct_Item_Values()
        {

            // arrange
            Widget[] widgets = { new Widget(101, "WidgetOne"), new Widget(102, "WidgetTwo"), new Widget(103, "WidgetThree"), new Widget(104, "WidgetFour"), new Widget(105, "WidgetFive") };
            var factory = new LookupFactory();


            // action
            var result = factory.From(widgets, w => w.ID.ToString(), w => w.Name, w => w);


            // assert
            Assert.IsNotNull(result);

            Assert.AreEqual("101", result[0].Key);
            Assert.AreEqual("WidgetOne", result[0].Description);
            Assert.AreEqual(widgets[0], result[0].Entity);

            Assert.AreEqual("102", result[1].Key);
            Assert.AreEqual("WidgetTwo", result[1].Description);
            Assert.AreEqual(widgets[1], result[1].Entity);

            Assert.AreEqual("103", result[2].Key);
            Assert.AreEqual("WidgetThree", result[2].Description);
            Assert.AreEqual(widgets[2], result[2].Entity);

            Assert.AreEqual("104", result[3].Key);
            Assert.AreEqual("WidgetFour", result[3].Description);
            Assert.AreEqual(widgets[3], result[3].Entity);

            Assert.AreEqual("105", result[4].Key);
            Assert.AreEqual("WidgetFive", result[4].Description);
            Assert.AreEqual(widgets[4], result[4].Entity);

        }
    }


 #region Widget Class Used For Testing

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

#endregion
}
