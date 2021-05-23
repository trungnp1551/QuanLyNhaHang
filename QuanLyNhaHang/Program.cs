using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyNhaHang
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //loginForm loginForm = new loginForm();
            //mainscreenForm mainscreenForm = new mainscreenForm();
            //menuForm 
            Application.Run(new loginForm());
            
        }
    }
}
