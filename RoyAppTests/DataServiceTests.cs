using FluentAssertions;
using RoyApp.Services;
using Xunit;

namespace RoyApp.Tests
{
    public class DataServiceTests
    {
        [Theory]
        [InlineData("12:00 AM", 0.00)]
        [InlineData("11:59 AM", 11.98)]
        [InlineData("1345", 13.75)]
        [InlineData("145PM", 13.75)]
        [InlineData("11:59 PM", 23.98)]
        [InlineData("1", 0)]
        public void BedtimeRaw_AMPM_Should_ConvertToDecimal(string timeAsString, decimal expected)
        {
            var dataService = new DataService();
            dataService.TimeToDecimal(timeAsString).Should().Be(expected);
        }

        [Theory]
        [InlineData("12:00 AM", 12.00)]
        [InlineData("1:45 AM", 13.75)]
        [InlineData("101 AM", 1)]
        public void BedtimeRaw_AMPM_ShouldNotEqual_ExpectedDecimal(string timeAsString, decimal expected)
        {
            var dataService = new DataService();
            dataService.TimeToDecimal(timeAsString).Should().NotBe(expected);
        }

        [Theory]
        [InlineData(50, 5, 10)]
        [InlineData(10, 5, 2)]
        public void TimeAverage_ShouldEqual_ExpectedAverage(decimal bedtimeTotal, int bedtimeCount, decimal expected)
        {
            var dataService = new DataService();
            dataService.TimeAverage(bedtimeTotal, bedtimeCount).Should().Be(expected);
        }

        [Theory]
        [InlineData(50, 5, 5)]
        [InlineData(28, 7, 14)]
        [InlineData(28, 0, 14)]
        public void TimeAverage_ShouldNotEqual_ExpectedAverage(decimal bedtimeTotal, int bedtimeCount, decimal expected)
        {
            var dataService = new DataService();
            dataService.TimeAverage(bedtimeTotal, bedtimeCount).Should().NotBe(expected);
        }
        [Theory]
        [InlineData("22", "8", 10)]
        [InlineData("20", "4", 8)]
        [InlineData("6", "12", 6)]
        [InlineData("6.5", "12", 5.5)]
        public void TimeDuration_ShouldEqual_ExpectedDuration(string enteredBedtimeRec, string enteredWaketimeRec, decimal expected)
        {
            var dataService = new DataService();
            dataService.TimeDuration(enteredBedtimeRec, enteredWaketimeRec).Should().Be(expected);
        }

        [Theory]
        [InlineData("22", "8",  12)]
        [InlineData("14", "13", 12)]
        [InlineData("6", "12",  12)]
        [InlineData("6", null,  12)]
        [InlineData(null, "6",  12)]
        public void TimeDuration_ShouldNotEqual_ExpectedDuration(string enteredBedtimeRec, string enteredWaketimeRec, decimal expected)
        {
            var dataService = new DataService();
            dataService.TimeDuration(enteredBedtimeRec, enteredWaketimeRec).Should().NotBe(expected);
        }

        [Theory]
        [InlineData("1,12:00 PM,8:00 AM", new string[] { "1", "12:00 PM", "12", "8:00 AM", "8", "20" })]
        public void LineData_ShouldEqual_ExpectedArray(string enteredLineData, string[] expected)
        {
            var dataService = new DataService();
            dataService.SplitLineData(enteredLineData).Should().Equal(expected);
        }
    }
}