using RoyApp.Interfaces;
using System.Windows.Forms;

namespace RoyApp.Gui.Components
{
    internal class DialogWrapper : IViewDialog
    {
        public string Open()
        {
            using OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = "";
            openFileDialog.Filter = Properties.Resources.filefilterExt;
            openFileDialog.FilterIndex = 2;
            openFileDialog.RestoreDirectory = true;

            return openFileDialog.ShowDialog() == DialogResult.OK ? openFileDialog.FileName : null;
        }

        public string Save()
        {
            using SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Filter = Properties.Resources.filefilterExt;
            saveDialog.RestoreDirectory = true;

            return saveDialog.ShowDialog() == DialogResult.OK ? saveDialog.FileName : null;
        }
    }
}
