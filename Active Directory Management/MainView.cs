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
			"sn", // Фамилия
			"givenName", // Имя
			"middleName", // Отчество
			"extensionAttribute3", //  Пол
			"mobile", // Мобильный телефон
			"extensionAttribute2", // Дата рождения
			"title", // Должность
			"description", // Отдел\направление
			"department", // Департамент
			"physicalDeliveryOfficeName", // Кабинет
			"manager", // Руководитель
			"telephoneNumber", // Внутренний телефон
			"ipPhone", // Внутренний телефон
			"userAccountControl" // Флаги аккаунта
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
			foreach (SearchResult res in departmentsResponse)
			{
				DirectoryEntry dept = res.GetDirectoryEntry();

				XElement deptElem = new XElement("dept",
					new XAttribute("name", dept.Properties["name"].Value),
					new XAttribute("nameRU", dept.Properties["description"].Value),
					new XAttribute("dn", dept.Properties["distinguishedName"].Value));

				searcher.SearchRoot = dept;
				SearchResultCollection usersResponse = searcher.FindAll();
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

					deptElem.Add(userElem);
				}
				dump.Root.Add(deptElem);
			}
			searcher.Dispose();
			dump.Save(Properties.Resources.XmlFile);
			User.XmlFileLocation = Properties.Resources.XmlFile;
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
					}
					
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
            DetailView detailView = new DetailView();
            detailView.ShowDialog();
			if (detailView.success == DialogResult.OK)
			{
				doc = XDocument.Load(Properties.Resources.XmlFile);
				FillTree();
			}
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
			doc = DumpToXml();
			

			if (treeView.SelectedNode != null)
			{
				string sel = treeView.SelectedNode.Name;
				searchBox.Clear();
				FillTree();

				foreach (TreeNode node in treeView.Nodes)
					foreach (TreeNode user in node.Nodes)
						if (user.Name == sel)
						{
							Debug.WriteLine(user.Name);
							treeView.SelectedNode = user;
							user.EnsureVisible();
						}
			}
			else
			{
				searchBox.Clear();
				FillTree();
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
						form.reasonTextBox.Text = "уволен";
						form.ShowDialog(this);
						if(form.DialogResult == DialogResult.OK)
						{
							descr = String.Format("(Временно отключена {0}, {1}, причина: {2}) {3}",
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
			catch
			{
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
	}
}
