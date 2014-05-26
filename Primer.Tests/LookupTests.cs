using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Primer.Tests
{
    [TestClass]
    public class LookupTests
    {
        [TestMethod]
        [TestCategory("Primer.Lookup")]
        public void ApplyFilter_Hides_Items_That_Do_Not_Match_Criteria()
        {

            // Arrange
            var lookup = new Lookup<int>();
            var ten = lookup.Add("10", 10, "Ten");
            var eleven = lookup.Add("11", 11, "Eleven");
            var twelve = lookup.Add("12", 12, "Twelve");
            var thriteen = lookup.Add("13", 13, "Thirteen");
            var fourteen = lookup.Add("14", 14, "Fourteen");
            var fifteen = lookup.Add("15", 15, "Fifteen");


            // Action
            lookup.ApplyFilter(item => item.Entity <= 12);


            // Assert
            Assert.AreEqual(3, lookup.Count);
            Assert.AreSame(ten, lookup[0]);
            Assert.AreSame(eleven, lookup[1]);
            Assert.AreSame(twelve, lookup[2]);


        }



        [TestMethod]
        [TestCategory("Primer.Lookup")]
        public void Special_Bug_Investigating_Test()
        {

            // Arrange
            var lookup = new Lookup<int>();
            var Jan = lookup.Add("1", 1, "January");
            var Feb = lookup.Add("2", 2, "February");
            var Mar = lookup.Add("3", 3, "March");
            var Apr = lookup.Add("4", 4, "April");
            var May = lookup.Add("5", 5, "May");
            var Jun = lookup.Add("6", 6, "June");
            var Jul = lookup.Add("7", 7, "July");
            var Aug = lookup.Add("8", 8, "August");
            var Sep = lookup.Add("9", 9, "September");
            var Oct = lookup.Add("10", 10, "October");
            var Nov = lookup.Add("11", 11, "November");
            var Dec = lookup.Add("12", 12, "December");


            // Action
            lookup.ApplyFilter(item => item.Entity <= 6);
            lookup.ApplyFilter(item => item.Entity <= 12);


            // Assert
            Assert.AreEqual(12, lookup.Count);

            Assert.IsTrue(lookup.Contains(Jan));
            Assert.IsTrue(lookup.Contains(Feb));
            Assert.IsTrue(lookup.Contains(Mar));
            Assert.IsTrue(lookup.Contains(Apr));
            Assert.IsTrue(lookup.Contains(May));
            Assert.IsTrue(lookup.Contains(Jun));
            Assert.IsTrue(lookup.Contains(Jul));
            Assert.IsTrue(lookup.Contains(Aug));
            Assert.IsTrue(lookup.Contains(Sep));
            Assert.IsTrue(lookup.Contains(Oct));
            Assert.IsTrue(lookup.Contains(Nov));
            Assert.IsTrue(lookup.Contains(Dec));

            //Assert.AreSame(Jan, lookup[0]);
            //Assert.AreSame(Feb, lookup[1]);
            //Assert.AreSame(Mar, lookup[2]);
            //Assert.AreSame(Apr, lookup[3]);
            //Assert.AreSame(May, lookup[4]);
            //Assert.AreSame(Jun, lookup[5]);
            //Assert.AreSame(Jul, lookup[6]);
            //Assert.AreSame(Aug, lookup[7]);
            //Assert.AreSame(Sep, lookup[8]);
            //Assert.AreSame(Oct, lookup[9]);
            //Assert.AreSame(Nov, lookup[10]);
            //Assert.AreSame(Dec, lookup[11]);


        }



        [TestMethod]
        [TestCategory("Primer.Lookup")]
        public void ClearFilter_Reveals_Hidden_Items()
        {

            // Arrange
            var lookup = new Lookup<int>();
            var ten = lookup.Add("10", 10, "Ten");
            var eleven = lookup.Add("11", 11, "Eleven");
            var twelve = lookup.Add("12", 12, "Twelve");
            var thriteen = lookup.Add("13", 13, "Thirteen");
            var fourteen = lookup.Add("14", 14, "Fourteen");
            var fifteen = lookup.Add("15", 15, "Fifteen");
            lookup.ApplyFilter(item => item.Entity <= 12);


            // Action
            lookup.ClearFilter();


            // Assert
            Assert.AreEqual(6, lookup.Count);
            Assert.AreSame(ten, lookup[0]);
            Assert.AreSame(eleven, lookup[1]);
            Assert.AreSame(twelve, lookup[2]);
            Assert.AreSame(thriteen, lookup[3]);
            Assert.AreSame(fourteen, lookup[4]);
            Assert.AreSame(fifteen, lookup[5]);

        }

    }
}
