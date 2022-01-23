using Moq;
using RoyApp.Interfaces;
using RoyApp.Services;
using System.Collections.Generic;
using Xunit;

namespace RoyApp.Tests
{
    public class FileServiceTests
    {
        [Fact]
        public void EmptyFile_Should_ReturnFalse()
        {
            // Arrange
            string filePath = @"E:\test.csv";
            var mock = new Mock<IFileService>();
            mock.Setup(_ => _.Exists(filePath)).Returns(false);

            // Act
            var fs = new FileService(mock.Object);

            //Assert
            Assert.False(fs.Exists(filePath));
        }

        [Fact]
        public void WriteLine_Should_WriteToFile()
        {
            // Arrange
            string[] headers = { "id", "data1", "data2" };
            string filePath = @"C:\test.csv";
            var mock = new Mock<IFileService>();
            mock.Setup(_ => _.WriteLine(filePath, headers)).Returns(true);

            // Act
            var fs = new FileService(mock.Object);

            // Assert
            Assert.True(fs.WriteLine(filePath, headers));
        }

        [Fact]
        public void WriteLine_ShouldNot_WriteToFile()
        {
            // Arrange
            string[] headers = { "id", "data1", "data2" };
            string filePath = @"C:\test.csv";
            var mock = new Mock<IFileService>();
            mock.Setup(_ => _.WriteLine(filePath, headers)).Returns(false);

            // Act
            var fs = new FileService(mock.Object);

            // Assert
            Assert.False(fs.WriteLine(filePath, headers));
        }

        [Theory]
        [InlineData(@"C:\test.csv", new string[] { "id", "bedtime", "waketime" }, new List<List<string>> { "1", "10:00 PM", "8:00 AM" })]
        public void WriteDataToFile_Should_WriteToFile(string filePath, string[] headers, List<List<string>> itemList)
        {
            // Arrange
            var mock = new Mock<IFileService>();
            mock.Setup(_ => _.)
        }
    }
}
