using RoyApp.Interfaces;
using System;
using System.Windows.Forms;

namespace RoyApp.Services
{
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
