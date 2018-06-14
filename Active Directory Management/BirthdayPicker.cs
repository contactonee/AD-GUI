using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Active_Directory_Management
{
	public partial class BirthdayPicker : UserControl
	{
		public BirthdayPicker()
		{
			InitializeComponent();

			for (int i = 1; i <= 31; i++)
				dayBox.Items.Add(i);

			monthBox.Items.AddRange(new string[] {
				"январь",
				"февраль",
				"март",
				"апрель",
				"май",
				"июнь",
				"июль",
				"август",
				"сентябрь",
				"октябрь",
				"ноябрь",
				"декабрь"
			});

			for(int i = 16; i <= 90; i++)
				yearBox.Items.Add(DateTime.Today.Year - i);

			Clear();
		}

		public DateTime Value
		{
			get
			{
				try
				{
					return new DateTime(
						int.Parse(yearBox.Text),
						monthBox.Items.IndexOf(monthBox.Text) + 1,
						int.Parse(dayBox.Text));
				}
				catch
				{
					return DateTime.MinValue;
				}
			}
			set
			{
				if (value == DateTime.MinValue)
					Clear();

				else
				{
					dayBox.Text = value.Day.ToString();
					monthBox.SelectedIndex = monthBox.Items.IndexOf(value.Month);
					yearBox.Text = value.Year.ToString();
				}
			}
		}

		public void Clear()
		{
			dayBox.Text = string.Empty;
			monthBox.Text = string.Empty;
			yearBox.Text = string.Empty;
		}

	}
}
