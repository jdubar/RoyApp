using RoyApp.Services;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Forms;

namespace RoyApp
{
    [ExcludeFromCodeCoverage]
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var dataService = new DataService();
            var fileService = new FileService();
            var listViewService = new ListviewService();

            Main f1 = new Main(dataService, fileService, listViewService);
            f1.Text = Application.ProductName + " v" + Application.ProductVersion;
            Application.Run(f1);

        }
    }
}
