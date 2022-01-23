using RoyApp.Interfaces;
using System.IO;
using System.Windows.Forms;

namespace RoyApp.Gui.Components
{
    internal class DialogWrapper : IViewDialog
    {
        public Stream Open()
        {
            using OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = "";
            openFileDialog.Filter = Properties.Resources.filefilterExt;
            openFileDialog.FilterIndex = 2;
            openFileDialog.RestoreDirectory = true;

            return openFileDialog.ShowDialog() switch
            {
                DialogResult.OK => openFileDialog.OpenFile(),
                _ => null,
            };
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
