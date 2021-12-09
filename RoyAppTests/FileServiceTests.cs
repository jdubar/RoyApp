using FakeItEasy;
using RoyApp.Interfaces;
using Xunit;

namespace RoyApp.Tests
{
    public class FileServiceTests
    {
        [Fact]
        public void EmptyFile_Should_ReturnFalse()
        {
            //Arrange
            var fileService = A.Fake<IFileService>();

            string FilePath = "";

            // Act
            var fileExists = fileService.IsFileExists(FilePath);

            //Assert
            Assert.False(fileExists);
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
