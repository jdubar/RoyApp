using Moq;
using RoyApp.Interfaces;
using RoyApp.Services;
using System.Windows.Forms;
using Xunit;

namespace RoyApp.Tests
{
    public class MessageBoxServiceTests
    {
        [Fact]
        public void MessageBoxShouldValidateSut()
        {
            // Arrange
            var mockMsgBox = new Mock<IMessageBoxService>();
            mockMsgBox.Setup(m => m.Show("Text", "Title", MessageBoxButtons.OK, MessageBoxIcon.None)).Verifiable();

            var sut = new Sut(mockMsgBox.Object);

            // Act
            sut.MessageBoxSut();

            // Verify
            mockMsgBox.Verify();
        }

        [Fact]
        public void MessageBoxShouldValidateSutResult()
        {
            // Arrange
            var mockMsgBox = new Mock<IMessageBoxService>();
            mockMsgBox.Setup(m => m.Show("Text", "Title", MessageBoxButtons.OK, MessageBoxIcon.None)).Returns(DialogResult.None);

            var sut = new Sut(mockMsgBox.Object);

            // Act
            var actual = sut.MessageBoxSut();

            // Verify
            Assert.True(Equals(DialogResult.None, actual));
        }
    }
}
