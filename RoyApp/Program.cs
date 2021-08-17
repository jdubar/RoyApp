using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RoyApp
{
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
