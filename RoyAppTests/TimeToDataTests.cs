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
            string enteredBedtimeRaw = "1345";
            double expectedBedtimeDec = 13.75;

            double actual = TimeToData.TimeToDecimal(enteredBedtimeRaw);
            Assert.AreEqual(expectedBedtimeDec, actual);
        }
        [TestMethod()]
        public void BedtimeRaw_AMPM_LessThanTwelve_ShouldEqual_ExpectedDecimal()
        {
            string enteredBedtimeRaw = "145PM";
            double expectedBedtimeDec = 13.75;

            double actual = TimeToData.TimeToDecimal(enteredBedtimeRaw);
            Assert.AreEqual(expectedBedtimeDec, actual);
        }
        [TestMethod()]
        public void BedtimeRaw_LessThanThreeChars_ShouldReturnZero()
        {
            string enteredBedtimeRaw = "1";
            double expectedResult = 0;

            double actual = TimeToData.TimeToDecimal(enteredBedtimeRaw);
            Assert.AreEqual(expectedResult, actual);
        }
    }
}