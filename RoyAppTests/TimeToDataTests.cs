using RoyApp;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RoyApp.Tests
{
    [TestClass()]
    public class TimeToDataTests
    {
        [TestMethod()]
        public void BedtimeRaw_AMPM_Should_ConvertToDecimal()
        {
            string enteredBedtimeRaw = "12:45 PM";
            double expectedBedtimeDec = 12.75;

            double actual = TimeToData.TimeToDecimal(enteredBedtimeRaw);
            Assert.AreEqual(expectedBedtimeDec, actual);
        }
        [TestMethod()]
        public void BedtimeRaw_AMPM_ShouldNotEqual_ExpectedDecimal()
        {
            string enteredBedtimeRaw = "1:45 AM";
            double expectedBedtimeDec = 12.75;

            double actual = TimeToData.TimeToDecimal(enteredBedtimeRaw);
            Assert.AreNotEqual(expectedBedtimeDec, actual);
        }
        [TestMethod()]
        public void BedtimeRaw_GreaterThanTwelve_ShouldEqual_ExpectedDecimal()
        {
            string enteredBedtimeRaw = "13:45";
            double expectedBedtimeDec = 13.75;

            double actual = TimeToData.TimeToDecimal(enteredBedtimeRaw);
            Assert.AreEqual(expectedBedtimeDec, actual);
        }
    }
}