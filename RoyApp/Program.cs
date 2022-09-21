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
            var listViewService = new ListviewService();

            MainForm f1 = new MainForm(dataService, listViewService)
            {
                Text = Application.ProductName + " v" + Application.ProductVersion
            };
            Application.Run(f1);
        }
    }
}