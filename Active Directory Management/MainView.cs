using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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

        // Массив атрибутов для выгрузки из AD
        // Используется в DumpADtoXML()

        private string[] props = new string[]
        {
			"sn",
			"givenName",
			"middleName",
			"mobile",
			"extensionAttribute2",
			"title",
			"description",
			"department",
			"physicalDeliveryOfficeName",
			"telephoneNumber",
			"userAccountControl"
        };


        public MainView()
        {
            InitializeComponent();

			doc = DumpToXml();
			FillTree();
			
        }
		
		private XDocument DumpToXml()
		{
			XDocument dump = new XDocument();

			DirectorySearcher searcher = new DirectorySearcher()
			{
				SearchRoot = new DirectoryEntry("LDAP://OU=Users,OU=Aktau,DC=nng,DC=kz"),
				SearchScope = SearchScope.OneLevel
			};
			searcher.PropertiesToLoad.Add("name");
			searcher.PropertiesToLoad.Add("memberOf");
			searcher.PropertiesToLoad.Add("userAccountControl");
			searcher.PropertiesToLoad.AddRange(props);

			
			dump.Add(new XElement("company"));

			searcher.Filter = "(objectClass=organizationalUnit)";
			SearchResultCollection departmentsResponse = searcher.FindAll();

			searcher.Filter = "(objectClass=user)";
			foreach(SearchResult res in departmentsResponse)
			{
				DirectoryEntry dept = res.GetDirectoryEntry();

				XElement deptElem = new XElement("dept",
					new XAttribute("name", dept.Properties["name"].Value),
					new XAttribute("nameRU", dept.Properties["description"].Value),
					new XAttribute("dn", dept.Properties["distinguishedName"].Value));

				searcher.SearchRoot = dept;
				SearchResultCollection usersResponse = searcher.FindAll();
				foreach(SearchResult userRes in usersResponse)
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
				dump.Root.Add(deptElem);
			}
			dump.Save(Properties.Resources.XmlFile);
			return dump;
		}
		               
        private void FillTree(string query = "")
        {
			query = query.Trim();

			treeView.BeginUpdate();
            treeView.Nodes.Clear();

			if (query == string.Empty)
			{
				foreach (XElement dept in doc.Root.Elements("dept"))
				{

					TreeNode deptNode = new TreeNode(dept.Attribute("nameRU").Value);
					foreach (XElement user in dept.Elements("user"))
					{
						string displayName = String.Concat(
							user.Element("sn").Value,
							" ",
							user.Element("givenName").Value);

						displayName = displayName.Trim();

						if (displayName == string.Empty)
							displayName = user.Attribute("name").Value;

						deptNode.Nodes.Add(user.Attribute("dn").Value, displayName);
					}
					treeView.Nodes.Add(deptNode);
				}
			}
			else
			{
				query = query.ToLower();
				var response = doc.Root.Descendants("user")
					.Where(t => t.Element("givenName").Value.ToLower().StartsWith(query)
						|| t.Element("sn").Value.ToLower().StartsWith(query)
						|| t.Attribute("name").Value.ToLower().StartsWith(query)
						|| t.Attribute("name").Value.ToLower().Contains(" " + query));
				foreach(XElement user in response)
				{
					string displayName = String.Concat(
						user.Element("sn").Value,
						" ",
						user.Element("givenName").Value);

					displayName = displayName.Trim();

					if (displayName == string.Empty)
						displayName = user.Attribute("name").Value;

					treeView.Nodes.Add(user.Attribute("dn").Value, displayName);
				}
			}
			treeView.Sort();
			treeView.EndUpdate();
        }
        
		

        private void SearchBox_TextChanged(object sender, EventArgs e)
        {
			FillTree(searchBox.Text);
        }

        // Buttons behaviour

        private void CreateBtn_Click(object sender, EventArgs e)
        {
            Form detailView = new DetailView();
            detailView.ShowDialog();
        }

        private void DetailBtn_Click(object sender, EventArgs e)
        {
            Form detailView = new DetailView(user);
            detailView.ShowDialog(this);
        }

        private void TreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (treeView.SelectedNode.Name.StartsWith("CN"))
            {
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

			string sel = treeView.SelectedNode.Name;
			doc = DumpToXml();
			searchBox.Clear();
			
			foreach(TreeNode node in treeView.Nodes)
				foreach (TreeNode user in node.Nodes)
					if (user.Name == sel)
					{
						treeView.SelectedNode = user;
						user.EnsureVisible();
					}

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
				MessageBox.Show("Невозможно сохранить, возможно не хватает прав");
			}
		}

        private void DisableBtn_Click(object sender, EventArgs e)
        {
			string descr = user.GetProperty("description");
			try
			{
				if (user.Enabled)
				{
					DisableReasonForm form = new DisableReasonForm();
					form.ShowDialog(this);

					if (form.DialogResult == DialogResult.OK)
					{
						descr = String.Format("(Временно отключена {0}, {1}, причина: {2}) {3}",
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
				}
			}
			catch
			{
				MessageBox.Show("Недостаточно прав");
			}
        }
	}
}
