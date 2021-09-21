using NUnit.Framework;
using RoyApp.Services;

namespace RoyApp.Tests
{
    public class DataServiceTests
    {
        [TestCase("12:00 AM", 0.00)]
        [TestCase("11:59 AM", 11.98)]
        [TestCase("1345", 13.75)]
        [TestCase("145PM", 13.75)]
        [TestCase("11:59 PM", 23.98)]
        [TestCase("1", 0)]
        public void BedtimeRaw_AMPM_Should_ConvertToDecimal(string timeAsString, decimal expected)
        {
            // Arrange
            var dataService = new DataService();

            // Act
            var actual = dataService.TimeToDecimal(timeAsString);

            // Assert
            Assert.That(actual, Is.EqualTo(expected));
        }

        [TestCase("12:00 AM", 12.00)]
        [TestCase("1:45 AM", 13.75)]
        [TestCase("1", 1)]
        public void BedtimeRaw_AMPM_ShouldNotEqual_ExpectedDecimal(string timeAsString, decimal expected)
        {
            // Arrange
            var dataService = new DataService();

            // Act
            var actual = dataService.TimeToDecimal(timeAsString);

            // Assert
            Assert.That(actual, Is.Not.EqualTo(expected));
        }

        [TestCase("22", "8", 10)]
        [TestCase("20", "4", 8)]
        [TestCase("6", "12", 6)]
        public void TimeDuration_ShouldEqual_ExpectedDuration(string enteredBedtimeRec, string enteredWaketimeRec, double expected)
        {
            // Arrange
            var dataService = new DataService();

            // Act
            var actual = dataService.TimeDuration(enteredBedtimeRec, enteredWaketimeRec);

            // Assert
            Assert.That(actual, Is.EqualTo(expected));
        }

        [TestCase("22", "8", 12)]
        [TestCase("6", "12", 12)]
        [TestCase("6", null, 12)]
        public void TimeDuration_ShouldNotEqual_ExpectedDuration(string enteredBedtimeRec, string enteredWaketimeRec, double expected)
        {
            // Arrange
            var dataService = new DataService();

            // Act
            var actual = dataService.TimeDuration(enteredBedtimeRec, enteredWaketimeRec);

            // Assert
            Assert.That(actual, Is.Not.EqualTo(expected));
        }

        [TestCase(50, 5, 10)]
        [TestCase(10, 5, 2)]
        public void TimeAverage_ShouldEqual_ExpectedAverage(decimal bedtimeTotal, int bedtimeCount, double expected)
        {
            // Arrange
            var dataService = new DataService();

            // Act
            var actual = dataService.TimeAverage(bedtimeTotal, bedtimeCount);

            // Assert
            Assert.That(actual, Is.EqualTo(expected));
        }

        [TestCase(50, 5, 5)]
        [TestCase(28, 7, 14)]
        public void TimeAverage_ShouldNotEqual_ExpectedAverage(decimal bedtimeTotal, int bedtimeCount, double expected)
        {
            // Arrange
            var dataService = new DataService();

            // Act
            var actual = dataService.TimeAverage(bedtimeTotal, bedtimeCount);

            // Assert
            Assert.That(actual, Is.Not.EqualTo(expected));
        }
    }
}