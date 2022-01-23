using RoyApp.Interfaces;
using System.IO;

namespace RoyApp.Gui.Components
{
    internal class ViewDialog
    {
        private readonly IViewDialog _viewDialog;
        public ViewDialog(IViewDialog viewDialog) => _viewDialog = viewDialog;

        public Stream ShowOpenDialog()
        {
            return _viewDialog.Open();
        }

        public string ShowSaveDialog()
        {
            return _viewDialog.Save();
        }
    }
}
