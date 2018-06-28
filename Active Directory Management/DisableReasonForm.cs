using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Active_Directory_Management
{
    public partial class DisableReasonForm : Form
    {
        public DisableReasonForm()
        {
            InitializeComponent();
        }

		private void reasonTextBox_KeyUp(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
				button1.PerformClick();
		}
	}
}
