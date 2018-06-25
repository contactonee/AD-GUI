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

		public void AddGroup(string name, Guid guid, CheckState check = CheckState.Unchecked)
		{
			Groups.Add(name, guid);

			groupBox.BeginUpdate();

			groupBox.Items.Add(name, check);
			
			groupBox.EndUpdate();
		}
		public void RemoveGroup(string name)
		{
			Groups.Remove(name);

			groupBox.BeginUpdate();
			groupBox.Items.Remove(name);
			groupBox.EndUpdate();
		}
	}
}
