using Moq;
using RoyApp.Interfaces;
using System.Windows.Forms;
using Xunit;

namespace RoyApp.Tests
{
    public class MessageBoxServiceTests
    {
        [Fact]
        public void Test()
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
    }
}
