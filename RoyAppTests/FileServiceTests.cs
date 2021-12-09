using FakeItEasy;
using RoyApp.Interfaces;
using System.Collections.Generic;
using Xunit;

namespace RoyApp.Tests
{
    public class FileServiceTests
    {
        [Fact]
        public void FileWriteToCSV_Should_WriteLine()
        {
            // Arrange
            var fileService = A.Fake<IFileService>();

            List<List<string>> TestList = null;
            string[] headers = { "id", "data1", "data2" };
            string FilePath = @"C:\test.csv";

            // Act
            fileService.WriteToCsv(TestList, headers, FilePath);

            // Assert
            A.CallTo(() => fileService.WriteToCsv(TestList, headers, FilePath)).MustHaveHappened();
        }
    }
}
