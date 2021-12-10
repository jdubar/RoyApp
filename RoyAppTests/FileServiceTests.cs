using FakeItEasy;
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
            var fileService = A.Fake<IFileService>();

            string[] headers = { "id", "data1", "data2" };
            string filePath = @"C:\test.csv";

            // Act
            fileService.WriteLine(filePath, headers);

            // Assert
            A.CallTo(() => fileService.WriteLine(filePath, headers)).MustHaveHappenedOnceExactly();
        }
    }
}
