using RoyApp.Interfaces;
using System;
using System.Windows.Forms;

namespace RoyApp.Services
{
    public class MessageBoxService : IMessageBoxService
    {
        public void Error(Exception ex)
        {
            MessageBox.Show(ex.Message,
                            "Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
        }

        public void ExportSuccess()
        {
            MessageBox.Show("File successfully exported!",
                            "Success",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
        }

        public void ValueMissing()
        {
            MessageBox.Show("Please enter a value",
                            "Value missing",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
        }
    }
}
