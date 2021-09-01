using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RoyApp.Services;

namespace RoyApp.Tests
{
    [TestClass()]
    public class DataServiceTests
    {
        private readonly IDataService _dataServiceFake;

        public DataServiceTests()
        {
            _dataServiceFake = A.Fake<IDataService>();
        }

        [TestMethod()]
        public void BedtimeRaw_AMPM_Should_ConvertToDecimal()
        {
            string enteredBedtimeRaw = "12:45 PM";
            double expectedBedtimeDec = 12.75;

            double actual = _dataServiceFake.TimeToDecimal(enteredBedtimeRaw);
            Assert.AreEqual(expectedBedtimeDec, actual);
        }
        [TestMethod()]
        public void BedtimeRaw_AMPM_ShouldNotEqual_ExpectedDecimal()
        {
            string enteredBedtimeRaw = "1:45 AM";
            double expectedBedtimeDec = 12.75;

            double actual = _dataServiceFake.TimeToDecimal(enteredBedtimeRaw);
            Assert.AreNotEqual(expectedBedtimeDec, actual);
        }
        [TestMethod()]
        public void BedtimeRaw_GreaterThanTwelve_ShouldEqual_ExpectedDecimal()
        {
            string enteredBedtimeRaw = "1345";
            double expectedBedtimeDec = 13.75;

            double actual = _dataServiceFake.TimeToDecimal(enteredBedtimeRaw);
            Assert.AreEqual(expectedBedtimeDec, actual);
        }
        [TestMethod()]
        public void BedtimeRaw_AMPM_LessThanTwelve_ShouldEqual_ExpectedDecimal()
        {
            string enteredBedtimeRaw = "145PM";
            double expectedBedtimeDec = 13.75;

            double actual = _dataServiceFake.TimeToDecimal(enteredBedtimeRaw);
            Assert.AreEqual(expectedBedtimeDec, actual);
        }
        [TestMethod()]
        public void BedtimeRaw_LessThanThreeChars_ShouldReturnZero()
        {
            string enteredBedtimeRaw = "1";
            double expectedResult = 0;

            double actual = _dataServiceFake.TimeToDecimal(enteredBedtimeRaw);
            Assert.AreEqual(expectedResult, actual);
        }

        [TestMethod()]
        public void TimeDuration_ShouldEqual_ExpectedDuration()
        {
            string enteredBedtimeRec = "22";
            string enteredWaketimeRec = "8";
            double expectedDuration = 10;

            double actual = _dataServiceFake.TimeDuration(enteredBedtimeRec, enteredWaketimeRec);
            Assert.AreEqual(expectedDuration, actual);
        }
        [TestMethod()]
        public void TimeDuration_ShouldNotEqual_ExpectedDuration()
        {
            string enteredBedtimeRec = "12.75";
            string enteredWaketimeRec = "13.75";
            double expectedDuration = 2;

            double actual = _dataServiceFake.TimeDuration(enteredBedtimeRec, enteredWaketimeRec);
            Assert.AreNotEqual(expectedDuration, actual);
        }

        [TestMethod()]
        public void TimeAverage_ShouldEqual_ExpectedAverage()
        {
            double bedtimeTotal = 50;
            int bedtimeCount = 5;
            double expectedAverage = 10;

            double actual = _dataServiceFake.TimeAverage(bedtimeTotal, bedtimeCount);
            Assert.AreEqual(expectedAverage, actual);
        }
        [TestMethod()]
        public void TimeAverage_ShouldNotEqual_ExpectedAverage()
        {
            double bedtimeTotal = 50;
            int bedtimeCount = 5;
            double expectedAverage = 5;

            double actual = _dataServiceFake.TimeAverage(bedtimeTotal, bedtimeCount);
            Assert.AreNotEqual(expectedAverage, actual);
        }
    }
}