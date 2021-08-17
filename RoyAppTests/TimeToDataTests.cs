using Microsoft.VisualStudio.TestTools.UnitTesting;
using RoyApp;
using System;
using System.Collections.Generic;
using System.Text;

namespace RoyApp.Tests
{
    [TestClass()]
    public class TimeToDataTests
    {
        [TestMethod()]
        public void TimeToDecimalTest()
        {
            string enteredBedtimeRaw = "12:45 PM";
            double expectedBedtimeDec = 12.75;

            double actual = TimeToData.TimeToDecimal(enteredBedtimeRaw);
            Assert.AreEqual(expectedBedtimeDec, actual);
        }
    }
}