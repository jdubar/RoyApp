using RoyApp.Interfaces;
using System;
using System.Windows.Forms;

namespace RoyApp.Services
{
    public class ViewMessageBox
    {
        private readonly IMessageBoxService _messageBox;
        public ViewMessageBox(IMessageBoxService messageBox) => _messageBox = messageBox;

        public void Error(Exception ex)
        {
            _messageBox.Show(ex.Message,
                            "Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
        }

        public void ExportSuccess()
        {
            _messageBox.Show("File successfully exported!",
                            "Success",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
        }

        public void ValueMissing()
        {
            _messageBox.Show("Please enter a value",
                            "Value missing",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
        }
    }

    public class MessageBoxService : IMessageBoxService
    {
        public DialogResult Show(string message, string title, MessageBoxButtons buttons, MessageBoxIcon icon) => MessageBox.Show(message, title, buttons, icon);
    }

    public class Sut
    {
        private readonly IMessageBoxService _messageBox;
        public Sut(IMessageBoxService messageBox) => _messageBox = messageBox;

        public void MessageBoxSut()
        {
            _messageBox.Show("Text", "Title", MessageBoxButtons.OK, MessageBoxIcon.None);
        }
    }
}
