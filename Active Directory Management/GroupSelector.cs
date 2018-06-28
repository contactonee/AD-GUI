using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.DirectoryServices;

namespace Active_Directory_Management
{
	public partial class GroupSelector : UserControl
	{
		public Dictionary<string, Guid> Groups { get; set; } = new Dictionary<string, Guid>();
		public string[] File { get; set; }


		public GroupSelector()
		{
			InitializeComponent();
		}


		public Dictionary<Guid,bool> SelectedGroups()
		{
			Dictionary<Guid, bool> state = new Dictionary<Guid, bool>();

			foreach (Guid guid in Groups.Values)
				state[guid] = false;

			foreach(string checkedGroup in groupBox.CheckedItems)
				state[Groups[checkedGroup]] = true;

			return state;
		}

		public void RenderList(string cityRU, Guid department)
		{
			groupBox.BeginUpdate();

			groupBox.Items.Clear();
			Groups.Clear();
			foreach (string line in File)
			{
				string[] words = line.Split(';');
				string groupCity = words[0];
				string name = words[1];
				string guid = words[2];
				string type = words[3];

				if(groupCity == cityRU)
				{
					Groups.Add(words[1], new Guid(words[2]));
					if (type == "All")
						groupBox.Items.Add(name, CheckState.Checked);
					if (type == "Option")
						groupBox.Items.Add(name, CheckState.Unchecked);
					if (department != Guid.Empty && type.StartsWith("department"))
					{
						type = type.Substring(type.IndexOf('-') + 1);
						if (new Guid(type) == department)
							groupBox.Items.Add(name, CheckState.Checked);
					}
				}
			}
			groupBox.EndUpdate();
		}

		public void SetValue(Guid groupGuid, bool checkState)
		{
			groupBox.BeginUpdate();

			foreach (string currGroup in groupBox.Items)
			{
				if (Groups[currGroup] == groupGuid)
				{
					groupBox.SetItemChecked(groupBox.Items.IndexOf(currGroup), checkState);
					break;
				}
			}
			groupBox.EndUpdate();
		}
	}
}
