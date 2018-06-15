using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.DirectoryServices;

namespace Active_Directory_Management
{
    class Program
    {
        
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
        
    }
}
