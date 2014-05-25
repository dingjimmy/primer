using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Primer.Tests
{
    [TestClass]
    public class LookupTests
    {
        [TestMethod]
        [TestCategory("Primer.Lookup")]
        public void ApplyFilter_Hides_Items_That_Match_Criteria()
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
            Assert.AreSame(thriteen, lookup[0]);
            Assert.AreSame(fourteen, lookup[1]);
            Assert.AreSame(fifteen, lookup[2]);


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
