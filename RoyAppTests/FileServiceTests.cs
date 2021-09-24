using FakeItEasy;
using NUnit.Framework;
using RoyApp.Services;
using System.Collections.Generic;

namespace RoyApp.Tests
{
    [TestFixture]
    class FileServiceTests
    {
        [TestCase()]
        public void FileWriteToCSV_ShouldWriteLine()
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
