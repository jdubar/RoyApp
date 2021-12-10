using Moq;
using RoyApp.Interfaces;
using RoyApp.Services;
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
    }
}
