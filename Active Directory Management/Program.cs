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
			if (!System.IO.File.Exists("cities.csv")
				|| !System.IO.File.Exists("groups.csv"))
			{
				MessageBox.Show("Не удается найти файлы \"groups.csv\", \"cities.csv\"\n" +
					"Проверьте наличие файлов в папке с приложением",
					"Ошибка",
					MessageBoxButtons.OK,
					MessageBoxIcon.Error);
			}
			else
			{
				Application.EnableVisualStyles();
				Application.SetCompatibleTextRenderingDefault(false);
				Application.Run(new MainView());
			}
        }
        
    }
}
