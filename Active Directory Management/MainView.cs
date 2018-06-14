using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.DirectoryServices;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Active_Directory_Management
{
	public partial class MainView : Form
	{
		private XDocument doc;
		private User user;
		static public string[] cities = new string[]
		{
			"Актау",
			"Уральск",
			"Алматы",
			"Минск"
		};
		static public Dictionary<string, string> usersPath = new Dictionary<string, string>
		{
			["Актау"] = Properties.Addresses.AktauUsers,
			["Уральск"] = Properties.Addresses.UralskUsers,
			["Алматы"] = Properties.Addresses.AlmatyUsers,
			["Минск"] = Properties.Addresses.MinskUsers
		};
		static public Dictionary<string, string> domainPath = new Dictionary<string, string>
		{
			["Актау"] = Properties.Addresses.AktauDomain,
			["Уральск"] = Properties.Addresses.UralskDomain,
			["Алматы"] = Properties.Addresses.AlmatyDomain,
			["Минск"] = Properties.Addresses.MinskDomain
		};


		// Массив атрибутов для выгрузки из AD
		// Используется в DumpADtoXML()

		private string[] props = Properties.Resources.PropertiesToLoad.Split(',');


        public MainView()
        {
            InitializeComponent();
			
			citySelector.Items.AddRange(cities);
			citySelector.SelectedIndex = 0;

			RenderTree();

			

		}
		
		private XDocument DumpToXml(string city)
		{
			this.Enabled = false;
			

			XDocument dump;
			try
			{
				dump = XDocument.Load(Properties.Resources.XmlFile);
			}
			catch
			{
				Debug.WriteLine("Не нашел документа, создал новый", "Info");
				dump = new XDocument();
				dump.Add(new XElement("company"));
			}
			XElement cityElem;
			try
			{
				dump.Root.Elements("city")
					.Where(t => t.Attribute("nameRU").Value == city)
					.First()
					.Remove();
			}
			catch (Exception ex)
			{
				Debug.WriteLine("Попытка удалить ветвь города, город не найден", "Info");
			}

			cityElem = new XElement("city",
				new XAttribute("name", ""),
				new XAttribute("nameRU", city),
				new XAttribute("dn", domainPath[city]));
			dump.Root.Add(cityElem);

			DirectoryEntry cityEntry = new DirectoryEntry("LDAP://" + usersPath[city]);
			cityElem.Attribute("name").Value = cityEntry.Parent.Properties["name"].Value.ToString();
			DirectorySearcher searcher = new DirectorySearcher()
			{
				SearchRoot = cityEntry,
				SearchScope = SearchScope.OneLevel
			};
			cityEntry.Dispose();

			
			searcher.PropertiesToLoad.Add("name");
			searcher.PropertiesToLoad.Add("memberOf");
			searcher.PropertiesToLoad.Add("userAccountControl");
			searcher.PropertiesToLoad.AddRange(props);

			searcher.Filter = "(objectClass=organizationalUnit)";
			SearchResultCollection departmentsResponse = searcher.FindAll();

			searcher.Filter = "(objectClass=user)";
			SearchResultCollection usersResponse = searcher.FindAll();

			foreach (SearchResult res in departmentsResponse)
			{
				DirectoryEntry dept = res.GetDirectoryEntry();

				XElement deptElem = new XElement("dept",
					new XAttribute("name", dept.Properties["name"].Value),
					new XAttribute("nameRU", dept.Properties["description"].Value),
					new XAttribute("dn", dept.Properties["distinguishedName"].Value));

				searcher.SearchRoot = dept;
				foreach (SearchResult userRes in searcher.FindAll())
				{
					DirectoryEntry user = userRes.GetDirectoryEntry();

					XElement userElem = new XElement("user",
						new XAttribute("name", user.Properties["name"].Value),
						new XAttribute("dn", user.Properties["distinguishedName"].Value));


					foreach (string prop in props)
						userElem.Add(new XElement(prop, user.Properties[prop].Value));

					XElement memberOf = new XElement("memberOf");
					foreach (string group in user.Properties["memberOf"])
						memberOf.Add(new XElement("group", group));
					userElem.Add(memberOf);

					deptElem.Add(userElem);
				}
				cityElem.Add(deptElem);
			}
			foreach (SearchResult userRes in usersResponse)
			{
				DirectoryEntry user = userRes.GetDirectoryEntry();

				XElement userElem = new XElement("user",
					new XAttribute("name", user.Properties["name"].Value),
					new XAttribute("dn", user.Properties["distinguishedName"].Value));


				foreach (string prop in props)
					userElem.Add(new XElement(prop, user.Properties[prop].Value));

				XElement memberOf = new XElement("memberOf");
				foreach (string group in user.Properties["memberOf"])
					memberOf.Add(new XElement("group", group));
				userElem.Add(memberOf);

				cityElem.Add(userElem);
			}

			searcher.Dispose();
			dump.Save(Properties.Resources.XmlFile);
			User.XmlFileLocation = Properties.Resources.XmlFile;
			this.Enabled = true;
			return dump;
		}
		               
        private void RenderTree(string query = "")
        {
			string select = treeView.SelectedNode != null ? treeView.SelectedNode.Text : string.Empty;

			treeView.BeginUpdate();
            treeView.Nodes.Clear();

			XElement root = doc.Root.Elements("city")
				.Where(t => t.Attribute("nameRU").Value == citySelector.Text)
				.First();

			if (query == string.Empty)
			{
				foreach (XElement dept in root.Elements("dept"))
				{

					TreeNode deptNode = treeView.Nodes.Add(dept.Attribute("nameRU").Value);
					foreach (XElement userElem in dept.Elements("user"))
					{
						User user = User.Load(userElem.Attribute("dn").Value);

						string displayName = String.Concat(
							user.GetProperty("sn"),
							" ",
							user.GetProperty("givenName"));

						displayName = displayName.Trim();

						if (displayName == string.Empty)
							displayName = user.Name;
						
						TreeNode userNode = deptNode.Nodes.Add(user.Dn, displayName);
						userNode.NodeFont = new Font(
						treeView.Font, user.Enabled ?
							FontStyle.Regular:
							FontStyle.Italic);

						if (displayName == select)
							treeView.SelectedNode = userNode;
					}
				}
				foreach(XElement userElem in root.Elements("user"))
				{
					User user = User.Load(userElem.Attribute("dn").Value);

					string displayName = String.Concat(
						user.GetProperty("sn"),
						" ",
						user.GetProperty("givenName"));

					displayName = displayName.Trim();

					if (displayName == string.Empty)
						displayName = user.Name;

					TreeNode userNode = treeView.Nodes.Add(user.Dn, displayName);
					userNode.NodeFont = new Font(
					treeView.Font, user.Enabled ?
						FontStyle.Regular :
						FontStyle.Italic);

					if (displayName == select)
						treeView.SelectedNode = userNode;
				}
			}
			else
			{
				query = query.ToLower();
				var response = root.Descendants("user")
					.Where(t => t.Element("givenName").Value.ToLower().StartsWith(query)
						|| t.Element("sn").Value.ToLower().StartsWith(query)
						|| t.Attribute("name").Value.ToLower().StartsWith(query)
						|| t.Attribute("name").Value.ToLower().Contains(" " + query));
				foreach(XElement userElem in response)
				{
					User user = User.Load(userElem.Attribute("dn").Value);

					string displayName = String.Concat(
						user.GetProperty("sn"),
						" ",
						user.GetProperty("givenName"));

					displayName = displayName.Trim();

					if (displayName == string.Empty)
						displayName = user.Name;

					TreeNode userNode = treeView.Nodes.Add(user.Dn, displayName);
					userNode.NodeFont = new Font(
						treeView.Font, user.Enabled ?
						FontStyle.Regular :
						FontStyle.Italic);

					if (displayName == select)
						treeView.SelectedNode = userNode;
				}
			}
			treeView.Sort();
			treeView.EndUpdate();
        }
        
		

        private void SearchBox_TextChanged(object sender, EventArgs e)
        {
			RenderTree(searchBox.Text);
        }

        // Buttons behaviour

        private void CreateBtn_Click(object sender, EventArgs e)
        {
            DetailView detailView = new DetailView(citySelector.Text);
            detailView.ShowDialog();
			if (detailView.success == DialogResult.OK)
			{
				doc = XDocument.Load(Properties.Resources.XmlFile);
				RenderTree();
			}
        }

        private void DetailBtn_Click(object sender, EventArgs e)
        {
            Form detailView = new DetailView(citySelector.Text, user);
            detailView.ShowDialog(this);
			RenderTree();
        }

        private void TreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (treeView.SelectedNode.Name.StartsWith("CN"))
            {
				treeView.BeginUpdate();
				treeView.SelectedNode.NodeFont = new Font(treeView.Font,
					treeView.SelectedNode.NodeFont.Style | FontStyle.Bold);
				treeView.EndUpdate();

				user = User.Load(treeView.SelectedNode.Name);

				firstBox.Text = user.GetProperty("givenName");
				lastBox.Text = user.GetProperty("sn");
                
                cdCheck.Checked = user.MemberOf(Properties.Groups.DvdDrives);
                usbDiskCheck.Checked = user.MemberOf(Properties.Groups.UsbDrives);
                usbDeviceCheck.Checked = user.MemberOf(Properties.Groups.UsbDevices);

                if (user.MemberOf(Properties.Groups.InternetFull))
                    internetCombo.SelectedIndex = 2;
                else if (user.MemberOf(Properties.Groups.InternetLimited))
					internetCombo.SelectedIndex = 1;
				else
					internetCombo.SelectedIndex = 0;
			
				disableBtn.Enabled = true;

                if (user.Enabled)
                {
					disableReason.Visible = false;
                    switchPanel.Enabled = true;
                    disableBtn.Text = "Отключить аккаунт";
					
				}
                else
                {
					disableReason.Text = user.GetProperty("description").Trim('(').Split(')')[0];
					disableReason.Visible = true;
					switchPanel.Enabled = false;
                    disableBtn.Text = "Активировать аккаунт";

                }
			}
            else
            {
                firstBox.Text = string.Empty;
                lastBox.Text = string.Empty;

                cdCheck.Checked = false;
                usbDiskCheck.Checked = false;
                usbDeviceCheck.Checked = false;

                internetCombo.SelectedIndex = 0;
                switchPanel.Enabled = false;
                disableBtn.Enabled = false;
            }
			
        }

        private void UpdBtn_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
			doc = DumpToXml(citySelector.Text);
			RenderTree();

			this.Enabled = true;

		}


        private void SaveBtn_Click(object sender, EventArgs e)
        {
			try
			{
				user.SetMembership(Properties.Groups.DvdDrives, cdCheck.Checked);
				user.SetMembership(Properties.Groups.UsbDrives, usbDiskCheck.Checked);
				user.SetMembership(Properties.Groups.UsbDevices,
					usbDeviceCheck.Checked);

				user.SetMembership(Properties.Groups.InternetLimited, internetCombo.SelectedIndex == 1);
				user.SetMembership(Properties.Groups.InternetFull, internetCombo.SelectedIndex == 2);

			}
			catch
			{
				Debug.WriteLine("Невозможно выставить группы", "Warning");
				MessageBox.Show("Невозможно сохранить, недостаточно прав");
			}
		}

        private void DisableBtn_Click(object sender, EventArgs e)
        {
			string descr = user.GetProperty("description");
			try
			{
				if (user.Enabled)
				{
					DialogResult fullDeactivation = MessageBox.Show(
							"Желаете полностью деактивировать учетную запись (увольнение) ?",
							"Увольнение?",
							MessageBoxButtons.YesNoCancel,
							MessageBoxIcon.Asterisk,
							MessageBoxDefaultButton.Button2);

					if (fullDeactivation == DialogResult.Yes)
					{
						DisableReasonForm form = new DisableReasonForm();
						form.reasonTextBox.Text = "Уволен" + (user.GetProperty("extensionAttribute3")[0] == 'F' ? "a" : "");
						form.ShowDialog(this);
						if(form.DialogResult == DialogResult.OK)
						{
							descr = String.Format("(Отключен{0} {1}, {2}, причина: {3}) {4}",
								(user.GetProperty("extensionAttribute3")[0] == 'F' ? "a" : ""),
								DateTime.Today.ToShortDateString(),
								Environment.UserName,
								form.reasonTextBox.Text,
								descr);

							user.Properties["description"] = descr;
							user.CommitChanges();
							user.Enabled = false;
							user.Remove();

							treeView.SelectedNode.Remove();
						}
					}
					if (fullDeactivation == DialogResult.No)
					{

						DisableReasonForm form = new DisableReasonForm();
						form.ShowDialog(this);

						if (form.DialogResult == DialogResult.OK)
						{

							descr = String.Format("(Временно отключен{0} {1}, {2}, причина: {3}) {4}",
								(user.GetProperty("extensionAttribute3")[0] == 'F' ? "a" : ""),
								DateTime.Today.ToShortDateString(),
								Environment.UserName,
								form.reasonTextBox.Text,
								descr);

							user.Properties["description"] = descr;
							user.CommitChanges();
							user.Enabled = false;

							disableReason.Text = descr.Trim('(').Split(')')[0];
							disableReason.Visible = true;
							switchPanel.Enabled = false;
							disableBtn.Text = "Активировать аккаунт";
							treeView.SelectedNode.NodeFont = new Font(treeView.Font, FontStyle.Italic | FontStyle.Bold);
						}
					}
				}
				else
				{
					descr = descr.Substring(descr.LastIndexOf(')') + 2);

					user.Properties["description"] = descr;
					user.CommitChanges();
					user.Enabled = true;

					disableReason.Visible = false;
					switchPanel.Enabled = true;
					disableBtn.Text = "Отключить аккаунт";
					treeView.SelectedNode.NodeFont = new Font(treeView.Font, FontStyle.Bold);
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine("Не может выключить учетку, возможно нет прав", "Waning");
				MessageBox.Show("Недостаточно прав");
			}
        }

		private void treeView_BeforeSelect(object sender, TreeViewCancelEventArgs e)
		{

			if (treeView.SelectedNode != null && treeView.SelectedNode.Name.StartsWith("CN"))
			{
				treeView.BeginUpdate();
				treeView.SelectedNode.NodeFont = new Font(treeView.Font,
					treeView.SelectedNode.NodeFont.Style & ~FontStyle.Bold);
				treeView.EndUpdate();
			}
		}

		private void treeView_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
		{
			detailBtn.PerformClick();
		}

		private void citySelector_SelectedIndexChanged(object sender, EventArgs e)
		{
			doc = DumpToXml(citySelector.Text);
			RenderTree();
		}
	}
}
