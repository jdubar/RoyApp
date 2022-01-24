using Moq;
using RoyApp.Interfaces;
using RoyApp.Services;
using System;
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
        public void WriteHeader_Should_WriteToFile()
        {
            // Arrange
            string[] headers = { "id", "data1", "data2" };
            string filePath = @"C:\test.csv";
            var mock = new Mock<IFileService>();
            mock.Setup(_ => _.WriteHeader(filePath, headers)).Returns(true);

            // Act
            var fs = new FileService(mock.Object);

            // Assert
            Assert.True(fs.WriteHeader(filePath, headers));
        }

        [Fact]
        public void WriteHeader_ShouldNot_WriteToFile()
        {
            // Arrange
            string[] headers = { "id", "data1", "data2" };
            string filePath = @"C:\test.csv";
            var mock = new Mock<IFileService>();
            mock.Setup(_ => _.WriteHeader(filePath, headers)).Returns(false);

            // Act
            var fs = new FileService(mock.Object);

            // Assert
            Assert.False(fs.WriteHeader(filePath, headers));
        }

        [Fact]
        public void WriteData_Should_WriteToFile()
        {
            // Arrange
            string dataLine = "aaa, 1234, 12.57, 1235, 12.58, 0.01";
            string filePath = @"C:\test.csv";
            var mock = new Mock<IFileService>();
            mock.Setup(_ => _.WriteData(filePath, dataLine)).Returns(true);

            // Act
            var fs = new FileService(mock.Object);

            // Assert
            Assert.True(fs.WriteData(filePath, dataLine));
        }

        [Fact]
        public void WriteData_ShouldNot_WriteToFile()
        {
            // Arrange
            string dataLine = "aaa, 1234, 12.57, 1235, 12.58, 0.01";
            string filePath = @"C:\test.csv";
            var mock = new Mock<IFileService>();
            mock.Setup(_ => _.WriteData(filePath, dataLine)).Returns(false);

            // Act
            var fs = new FileService(mock.Object);

            // Assert
            Assert.False(fs.WriteData(filePath, dataLine));
        }

        [Fact]
        public void WriteDataToFile_ShouldFail_NullItemList()
        {
            // Arrange
            string filePath = @"C:\test.csv";
            string[] headers = { "id", "data1", "data2" };
            string itemList = null;
            string paramName = "itemList";
            var mock = new Mock<IFileService>();
            var fileService = new FileService(mock.Object);

            // Act & Assert
            try
            {
                fileService.WriteDataToFile(filePath, headers, itemList);
            }
            catch (ArgumentNullException ex)
            {
                Assert.Equal(paramName, ex.ParamName);
            }
        }

        [Theory]
        [InlineData(@"C:\test.csv", new string[] { }, "aaa, 1234, 12.57, 1235, 12.58, 0.01")]
        [InlineData(@"C:\test.csv", new string[] { null }, "aaa, 1234, 12.57, 1235, 12.58, 0.01")]
        [InlineData(@"C:\test.csv", new string[] { "id" }, "aaa, 1234, 12.57, 1235, 12.58, 0.01")]
        public void WriteDataToFile_ShouldFail_EmptyHeaderList(string filePath, string[] headers, string itemList)
        {
            // Arrange
            string paramName = "headers";
            var mock = new Mock<IFileService>();
            var fileService = new FileService(mock.Object);

            // Act & Assert
            try
            {
                fileService.WriteDataToFile(filePath, headers, itemList);
            }
            catch (ArgumentNullException ex)
            {
                Assert.Equal(paramName, ex.ParamName);
            }
        }

        [Fact]
        public void WriteDataToFile_ShouldPass()
        {
            // Arrange
            string filePath = @"C:\test.csv";
            string[] headers = { "id", "data1", "data2" };
            string itemList = "aaa, 1234, 12.57, 1235, 12.58, 0.01";
            var mock = new Mock<IFileService>();
            var fileService = new FileService(mock.Object);

            // Act
            var exception = Record.Exception(() => fileService.WriteDataToFile(filePath, headers, itemList));

            // Assert
            Assert.Null(exception);
        }

        [Fact]
        public void ReadImportData_Should_ReadData()
        {
            // Arrange
            string[] fakeRow = { "id", "data1", "data2" };
            List<string[]> fakeDataList = new List<string[]> { fakeRow, fakeRow, fakeRow };
            string fakeFilePath = @"C:\test.csv";
            var mock = new Mock<IFileService>();
            mock.Setup(fileService => fileService.ReadImportData(fakeFilePath)).Returns(fakeDataList);

            // Act
            var fs = new FileService(mock.Object);
            var actual = fs.ReadImportData(fakeFilePath);

            // Assert
            Assert.Equal(fakeRow, actual[0]);
        }
    }
}
