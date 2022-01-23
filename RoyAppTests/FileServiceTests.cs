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
    }
}
