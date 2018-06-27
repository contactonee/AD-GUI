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
		public static XDocument xmlDoc;
		private User selectedUser;
		
		static public Dictionary<string, string> cityOuPath;


		// Массив атрибутов для выгрузки из AD
		// Используется в DumpADtoXML()
		private string[] fields = Properties.Resources.PropertiesToLoad.Split(',');


        public MainView()
        {
            InitializeComponent();
			

			// Подгрузка списка городов
			cityOuPath = new Dictionary<string, string>();
			foreach (string line in System.IO.File.ReadAllLines("cities.csv"))
			{
				string name = line.Split(';')[0];
				string dn = line.Split(';')[1];

				cityOuPath.Add(name,dn);
			}

			try
			{
				xmlDoc = XDocument.Load(Properties.Resources.XmlFile);
				DateTime lastUpd = DateTime.Parse(xmlDoc.Root.Attribute("lastUpdated").Value);
				if (DateTime.Now.Subtract(lastUpd).TotalSeconds > 30)
					throw new Exception("Too old file! (Last Update >30 seconds ago)");
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.Message);
				xmlDoc = DumpToXml(cityOuPath.Keys.First());
			}

			// Заполнение выпадающего списка городов
			citySelector.Items.AddRange(cityOuPath.Keys.ToArray());



			citySelector.SelectedIndex = 0;
		}

		/// <summary>
		/// Выгрузка записей из Active Directory в XML-файл по городу
		/// </summary>
		/// <param name="cityRU">Название города на русском</param>
		/// <returns>Ссылка на XDocument</returns>
		private XDocument DumpToXml(string cityRU)
		{
			this.Enabled = false;

			XDocument dump;

			try
			{
				dump = new XDocument(xmlDoc);
			}
			catch
			{
				Debug.WriteLine("Не нашел документа, создал новый", "Info");
				dump = new XDocument();

				// Добавление корня
				dump.Add(new XElement("company"));
			}

			XElement cityElem;
			try
			{
				// Поиск узла города и его удаление
				dump.Root.Elements("city")
					.Where(t => t.Attribute("nameRU").Value == cityRU)
					.First()
					.Remove();
			}
			catch
			{
				Debug.WriteLine("Попытка удалить ветвь города, город не найден", "Info");
			}

			cityElem = new XElement("city",
				new XAttribute("name", ""),
				new XAttribute("nameRU", cityRU),
				new XAttribute("country", ""),
				new XAttribute("postalCode", ""),
				new XAttribute("addr", ""));

			dump.Root.Add(cityElem);

			using (DirectoryEntry cityEntry = new DirectoryEntry("LDAP://OU=Users," + cityOuPath[cityRU]))
			{

				cityElem.Attribute("name").Value = cityEntry.Parent.Properties["name"].Value.ToString();

				try
				{
					cityElem.Attribute("postalCode").Value = cityEntry.Parent.Properties["postalCode"].Value.ToString();
					cityElem.Attribute("addr").Value = cityEntry.Parent.Properties["street"].Value.ToString();
					cityElem.Attribute("country").Value = cityEntry.Parent.Properties["c"].Value.ToString();
				}
				catch
				{
					Debug.WriteLine("Нет адресных данных города, пропуск");
				}
				

				using (DirectorySearcher searcher = new DirectorySearcher())
				{
					searcher.SearchRoot = cityEntry;
					searcher.SearchScope = SearchScope.OneLevel;

					// Manually add service properties
					searcher.PropertiesToLoad.Add("name");
					searcher.PropertiesToLoad.Add("memberOf");
					searcher.PropertiesToLoad.Add("userAccountControl");

					searcher.PropertiesToLoad.AddRange(fields);

					searcher.Filter = "(objectClass=organizationalUnit)";
					using (SearchResultCollection departmentsResponse = searcher.FindAll())
					{
						foreach (SearchResult res in departmentsResponse)
						{
							using (DirectoryEntry dept = res.GetDirectoryEntry())
							{

								XElement deptElem = new XElement("dept",
									new XAttribute("name", dept.Properties["name"].Value),
									new XAttribute("nameRU", dept.Properties["description"].Value),
									new XAttribute("dn", dept.Properties["distinguishedName"].Value),
									new XAttribute("guid", dept.Guid.ToString()));

								searcher.SearchRoot = dept;
								searcher.Filter = "(objectClass=user)";
								foreach (SearchResult userRes in searcher.FindAll())
								{
									using (DirectoryEntry user = userRes.GetDirectoryEntry())
									{

										XElement userElem = new XElement("user",
											new XAttribute("name", user.Properties["name"].Value),
											new XAttribute("dn", user.Properties["distinguishedName"].Value),
											new XAttribute("guid", user.Guid.ToString()));


										foreach (string prop in fields)
											userElem.Add(new XElement(prop, user.Properties[prop].Value));

										XElement memberOf = new XElement("memberOf");
										foreach (string groupDN in user.Properties["memberOf"])
											memberOf.Add(new XElement("group", groupDN));
										userElem.Add(memberOf);

										deptElem.Add(userElem);
									}
								}
								cityElem.Add(deptElem);
							}
						}
					}

					searcher.SearchRoot = cityEntry;
					searcher.Filter = "(objectClass=user)";
					using (SearchResultCollection usersResponse = searcher.FindAll())
					{
						foreach (SearchResult userRes in usersResponse)
						{
							using (DirectoryEntry user = userRes.GetDirectoryEntry())
							{

								XElement userElem = new XElement("user",
									new XAttribute("name", user.Properties["name"].Value),
									new XAttribute("dn", user.Properties["distinguishedName"].Value),
									new XAttribute("guid", user.Guid.ToString()));


								foreach (string prop in fields)
									userElem.Add(new XElement(prop, user.Properties[prop].Value));

								XElement memberOf = new XElement("memberOf");
								foreach (string groupDN in user.Properties["memberOf"])
									memberOf.Add(new XElement("group", groupDN));
								userElem.Add(memberOf);


								cityElem.Add(userElem);
							}
						}
					}
				}
			}

			// Когда было произведено последнее обновление
			try
			{
				dump.Root.Attribute("lastUpdated").Value = DateTime.Now.ToString();
			}
			catch
			{
				Debug.WriteLine("Нет атрибута для даты, создаем новый");
				dump.Root.Add(new XAttribute("lastUpdated", DateTime.Now.ToString()));
			}

			dump.Save(Properties.Resources.XmlFile);
			this.Enabled = true;
			return dump;
		}

		/// <summary>
		/// Отрисовка дерева организации из XML-файла
		/// </summary>
		/// <param name="city">Название города на русском</param>
		/// <param name="query">Искомый пользователь</param>
		private void RenderTree(XElement city, string query = "")
        {
			treeView.BeginUpdate();
            treeView.Nodes.Clear();

			if (query == string.Empty)
			{
				foreach (XElement deptElem in city.Elements("dept"))
					FillNode(treeView.Nodes.Add(deptElem.Attribute("nameRU").Value),
						deptElem.Elements("user").ToArray());

				FillNode(treeView, city.Elements("user").ToArray());
			}
			else
			{
				query = query.ToLower();
				var response = city.Descendants("user")
					.Where(t => t.Element("givenName").Value.ToLower().StartsWith(query)
						|| t.Element("sn").Value.ToLower().StartsWith(query)
						|| t.Attribute("name").Value.ToLower().StartsWith(query)
						|| t.Attribute("name").Value.ToLower().Contains(" " + query));
				FillNode(treeView, response.ToArray());
			}
			treeView.Sort();
			treeView.EndUpdate();
        }
        
		/// <summary>
		/// Служебный метод для отрисовки дерева
		/// </summary>
		/// <param name="node">Родительский узел</param>
		/// <param name="elements">Элементы для добавления</param>
		private void FillNode(Object node, XElement[] elements)
		{
			foreach (XElement userElem in elements)
			{
				string displayName = String.Concat(
						userElem.Element("sn").Value,
						" ",
						userElem.Element("givenName").Value);

				displayName = displayName.Trim();

				if (displayName == string.Empty)
					displayName = userElem.Attribute("name").Value;

				TreeNode userNode;

				if(node is TreeNode)
					userNode = ((TreeNode)node).Nodes.Add(displayName);
				else
					userNode = ((TreeView)node).Nodes.Add(displayName);
				

				userNode.Tag = new Guid(userElem.Attribute("guid").Value);

				if (!Convert.ToBoolean(int.Parse(userElem.Element("userAccountControl").Value) & 0x2))
					userNode.NodeFont = new Font(treeView.Font, FontStyle.Regular);
				else
					userNode.NodeFont = new Font(treeView.Font, FontStyle.Italic);
				if (selectedUser != null && (Guid)userNode.Tag == selectedUser.Guid)
				{
					treeView.SelectedNode = userNode;
					userNode.EnsureVisible();
				}
			}
		}

        private void SearchBox_TextChanged(object sender, EventArgs e)
        {
			RenderTree(xmlDoc.Root.Elements("city")
					.Where(t => t.Attribute("nameRU").Value == citySelector.Text)
					.First(), searchBox.Text);
        }


        // Buttons behaviour
        private void CreateBtn_Click(object sender, EventArgs e)
        {
            DetailView detailView = new DetailView(xmlDoc.Root.Elements("city")
					.Where(t => t.Attribute("nameRU").Value == citySelector.Text)
					.First());
            detailView.ShowDialog();
			
			xmlDoc = XDocument.Load(Properties.Resources.XmlFile);
			selectedUser = null;
			RenderTree(xmlDoc.Root.Elements("city")
					.Where(t => t.Attribute("nameRU").Value == citySelector.Text)
					.First());
        }

        private void DetailBtn_Click(object sender, EventArgs e)
        {
            Form detailView = new DetailView(xmlDoc.Root.Elements("city")
					.Where(t => t.Attribute("nameRU").Value == citySelector.Text)
					.First(), selectedUser);
            detailView.ShowDialog(this);

			RenderTree(xmlDoc.Root.Elements("city")
					.Where(t => t.Attribute("nameRU").Value == citySelector.Text)
					.First());
			
        }

		private void TreeView_BeforeSelect(object sender, TreeViewCancelEventArgs e)
		{

			
		}

		private void TreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (treeView.SelectedNode.Tag != null)
            {
				treeView.BeginUpdate();

				if (treeView.Tag != null && ((TreeNode)treeView.Tag).Tag != null)
					((TreeNode)treeView.Tag).NodeFont = new Font(treeView.Font,
						treeView.SelectedNode.NodeFont.Style & ~FontStyle.Bold);

				treeView.Tag = treeView.SelectedNode;

				treeView.SelectedNode.NodeFont = new Font(treeView.Font,
					treeView.SelectedNode.NodeFont.Style | FontStyle.Bold);

				treeView.EndUpdate();

				selectedUser = User.Load((Guid)treeView.SelectedNode.Tag);

				firstBox.Text = selectedUser.GetProperty("givenName");
				lastBox.Text = selectedUser.GetProperty("sn");

				disableBtn.Enabled = true;

                if (selectedUser.Enabled)
                {
					disableReason.Visible = false;
                    switchPanel.Enabled = true;
                    disableBtn.Text = "Отключить аккаунт";
					
				}
                else
                {
					disableReason.Text = selectedUser.GetProperty("description").Trim('(').Split(')')[0];
					disableReason.Visible = true;
					switchPanel.Enabled = false;
                    disableBtn.Text = "Активировать аккаунт";

                }
			}
            else
            {
                firstBox.Text = string.Empty;
                lastBox.Text = string.Empty;

                
                switchPanel.Enabled = false;
                disableBtn.Enabled = false;

				selectedUser = null;
            }
			
        }

        private void UpdBtn_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
			xmlDoc = DumpToXml(citySelector.Text);
			RenderTree(xmlDoc.Root.Elements("city")
					.Where(t => t.Attribute("nameRU").Value == citySelector.Text)
					.First());

			this.Enabled = true;
		}


        private void SaveBtn_Click(object sender, EventArgs e)
        {
			
		}

        private void DisableBtn_Click(object sender, EventArgs e)
        {
			string descr = selectedUser.GetProperty("description");
			try
			{
				if (selectedUser.Enabled)
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
						form.reasonTextBox.Text = "Уволен" + (selectedUser.GetProperty("extensionAttribute3")[0] == 'F' ? "a" : "");
						form.ShowDialog(this);
						if(form.DialogResult == DialogResult.OK)
						{
							descr = String.Format("(Отключен{0} {1}, {2}, причина: {3}) {4}",
								(selectedUser.GetProperty("extensionAttribute3")[0] == 'F' ? "a" : ""),
								DateTime.Today.ToShortDateString(),
								Environment.UserName,
								form.reasonTextBox.Text,
								descr);

							selectedUser.Properties["description"] = descr;
							selectedUser.CommitChanges();
							selectedUser.Enabled = false;
							selectedUser.Remove();

							selectedUser = null;
							RenderTree(xmlDoc.Root.Elements("city")
					.Where(t => t.Attribute("nameRU").Value == citySelector.Text)
					.First());
							
						}
					}
					if (fullDeactivation == DialogResult.No)
					{

						DisableReasonForm form = new DisableReasonForm();
						form.ShowDialog(this);

						if (form.DialogResult == DialogResult.OK)
						{

							descr = String.Format("(Временно отключен{0} {1}, {2}, причина: {3}) {4}",
								(selectedUser.GetProperty("extensionAttribute3")[0] == 'F' ? "a" : ""),
								DateTime.Today.ToShortDateString(),
								Environment.UserName,
								form.reasonTextBox.Text,
								descr);

							selectedUser.Properties["description"] = descr;
							selectedUser.CommitChanges();
							selectedUser.Enabled = false;

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

					selectedUser.Properties["description"] = descr;
					selectedUser.CommitChanges();
					selectedUser.Enabled = true;

					disableReason.Visible = false;
					switchPanel.Enabled = true;
					disableBtn.Text = "Отключить аккаунт";
					treeView.SelectedNode.NodeFont = new Font(treeView.Font, FontStyle.Bold);
				}
			}
			catch
			{
				Debug.WriteLine("Не может выключить учетку, возможно нет прав", "Waning");
				MessageBox.Show("Недостаточно прав");
			}
        }

		

		private void TreeView_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
		{
			detailBtn.PerformClick();
		}

		private void CitySelector_SelectedIndexChanged(object sender, EventArgs e)
		{
			try
			{
				RenderTree(xmlDoc.Root.Elements("city")
					.Where(t => t.Attribute("nameRU").Value == citySelector.Text)
					.First());
			}
			catch
			{
				Debug.WriteLine("Не может отрисовать дерево, возможно нет данных в XML");
				xmlDoc = DumpToXml(citySelector.Text);
				RenderTree(xmlDoc.Root.Elements("city")
					.Where(t => t.Attribute("nameRU").Value == citySelector.Text)
					.First());
			}
		}
	}
}
