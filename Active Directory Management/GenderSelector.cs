using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Active_Directory_Management
{
	public partial class GenderSelector : UserControl
	{
		public GenderSelector()
		{
			InitializeComponent();
			maleBtn.Checked = true;
		}
		/// <summary>
		/// Возвращает или задает значение пола (Male/Female)
		/// </summary>
		public string Value
		{
			get
			{
				if (maleBtn.Checked)
					return "Male";
				else
					return "Female";
			}
			set
			{			
				if (value.ToLower()[0] == 'm')
					maleBtn.Checked = true;
				else if (value.ToLower()[0] == 'f')
					femaleBtn.Checked = true;
				else
					throw new Exception("Unacceptable value");

			}
		}

	}
}
