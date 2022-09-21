using System.Windows.Forms;

namespace RoyApp.Interfaces
{
    public interface IMessageBoxService
    {
        DialogResult Show(string message, string title, MessageBoxButtons buttons, MessageBoxIcon icon);
    }
}