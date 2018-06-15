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
		private Dictionary<string, Guid> groups;
		private Dictionary<Guid, bool> state;

		public GroupSelector()
		{
			InitializeComponent();
		}

		public Dictionary<string, Guid> Groups
		{
			set
			{
				foreach(string name in value.Keys)
				{
					groupBox.Items.Add(name);
				}
			}
		}

		public Dictionary<Guid,bool> SelectedGroups()
		{
			foreach (Guid guid in groups.Values)
				state[guid] = false;

			foreach(string checkedGroup in groupBox.CheckedItems)
				state[groups[checkedGroup]] = true;

			return state;
		}

		public void SetValue(Guid groupGuid, bool checkState)
		{
			foreach (string currGroup in groupBox.Items)
			{
				if (groups[currGroup] == groupGuid)
				{
					groupBox.SetItemChecked(groupBox.Items.IndexOf(currGroup), checkState);
					break;
				}
			}
		}

	}
}
