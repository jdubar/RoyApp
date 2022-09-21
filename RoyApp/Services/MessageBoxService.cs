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
}
