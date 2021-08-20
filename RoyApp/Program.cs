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
            GUI_Main f1 = new GUI_Main();
            f1.Text = System.Windows.Forms.Application.ProductName + " v" + System.Windows.Forms.Application.ProductVersion;
            Application.Run(f1);

        }
    }
}
