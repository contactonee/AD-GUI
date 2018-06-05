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
            "description",
            "title",
            "physicaldeliveryofficename",
            "telephoneNumber",
            "givenName",
            "sn",
            "department",
            "memberOf",
            "middleName",
            "mobile",
            "extensionAttribute2",
            "userAccountControl",
            "name"
        };


        public MainView()
        {
            InitializeComponent();

            try
            {
                doc = XDocument.Load(Properties.Resources.usersXML);
                FillTree();
            }
            catch
            {
                DumpADtoXML();

                doc = XDocument.Load(Properties.Resources.usersXML);
                FillTree();
                
            }
            finally
            {
                Application.Exit();
            }
        }


        //TODO Сделать нормальный метод вытягивания данных с AD
        private void AppendUsers(SearchResultCollection response, XElement parent)
        {
            foreach(SearchResult res in response)
            {
                DirectoryEntry entry = res.GetDirectoryEntry();
                XElement elem = new XElement("user",
                    new XAttribute("name", entry.Properties["name"]));
                
                foreach(string prop in props)
                    elem.Add(new XElement(prop), entry.Properties[prop]);

                parent.Add(elem);

                entry.Dispose();
                
            }
        }

        

        private void DumpADtoXML()
        {
            // Настройка поисковика по записям
            // Поиск департаментов 
            DirectorySearcher searcher = new DirectorySearcher(
                new DirectoryEntry(Properties.Resources.devAddr))
                {
                    SearchScope = SearchScope.OneLevel
                };

            searcher.PropertiesToLoad.Add("name");
            searcher.PropertiesToLoad.AddRange(props);

            // Создание нового документа XML и создание в нем корня company
            XDocument xmlfile = new XDocument();
            xmlfile.Add(new XElement("company"));

            searcher.Filter = "(objectClass=organizationalUnit)";
            var departmentSearchResult = searcher.FindAll();

            // Переход на уровень глубже по департаментам
            foreach (SearchResult departmentRes in departmentSearchResult)
            {

                // Поиск субдепартаментов
                DirectoryEntry deptEntry = departmentRes.GetDirectoryEntry();

                

                string deptName = deptEntry.Properties["name"].Value.ToString();
                XElement deptNode = new XElement("dept",
                    new XAttribute("name", deptName),
                    new XAttribute("russName", ""),
                    new XAttribute("dn", deptEntry.Properties["distinguishedName"].Value.ToString()));

                searcher.SearchRoot = deptEntry;
                searcher.Filter = "(objectClass=organizationalUnit)";
                var divResults = searcher.FindAll();
                
                // Поиск людей в субдепартаментах
                foreach(SearchResult divRes in divResults)
                {
                    DirectoryEntry divEntry = divRes.GetDirectoryEntry();

                    string divName = divEntry.Properties["name"].Value.ToString();
                    XElement divNode = new XElement("subdept",
                        new XAttribute("name", divName),
                        new XAttribute("dn", divEntry.Properties["distinguishedName"].Value.ToString()));


                    searcher.Filter = "(objectClass=user)";
                    searcher.SearchRoot = divEntry;
                    var userResults = searcher.FindAll();

                    foreach (SearchResult userRes in userResults)
                    {
                        DirectoryEntry userEntry = userRes.GetDirectoryEntry();

                        XElement userNode = new XElement("user",
                            new XAttribute("name", userEntry.Properties["name"].Value),
                            new XAttribute("dn", userEntry.Properties["distinguishedName"].Value));

                        foreach(string prop in props)
                            userNode.Add(new XElement(prop, userEntry.Properties[prop]));

                        if (deptNode.Attribute("russName").Value == string.Empty)
                            deptNode.Attribute("russName").SetValue(userNode.Element("department").Value);
                        

                        divNode.Add(userNode);
                    }
                    deptNode.Add(divNode);
                }



                searcher.Filter = "(objectClass=user)";
                searcher.SearchRoot = deptEntry;

                var deptUserResults = searcher.FindAll();
                

                foreach (SearchResult userRes in deptUserResults)
                {
                    DirectoryEntry userEntry = userRes.GetDirectoryEntry();

                    XElement userNode = new XElement("user",
                        new XAttribute("name", userEntry.Properties["name"].Value),
                        new XAttribute("dn", userEntry.Properties["distinguishedName"].Value));

                    foreach (string prop in props)
                        userNode.Add(new XElement(prop, userEntry.Properties[prop]));

                    if (deptNode.Attribute("russName").Value == string.Empty)
                        deptNode.Attribute("russName").SetValue(userNode.Element("department").Value);

                    deptNode.Add(userNode);
                }

                xmlfile.Root.Add(deptNode);
            }

            
            xmlfile.Save(Properties.Resources.usersXML);
        }
               
        private void FillTree()
        {
            treeView.Nodes.Clear();


            foreach(XElement deptNode in doc.Root.Elements())
            {
                
                treeView.Nodes.Add(deptNode.Attribute("dn").Value, deptNode.Attribute("russName").Value);
                var divNodes = deptNode.Elements("subdept");
                foreach(XElement divNode in divNodes)
                {
                    treeView.Nodes[deptNode.Attribute("dn").Value].Nodes.Add(divNode.Attribute("dn").Value,
                           divNode.Attribute("name").Value);

                    var userNodes = divNode.Elements();
                    foreach(XElement userNode in userNodes)
                    {
                        treeView.Nodes[deptNode.Attribute("dn").Value].Nodes[divNode.Attribute("dn").Value].Nodes.Add(userNode.Attribute("dn").Value,
                           userNode.Element("sn").Value + " " + userNode.Element ("givenName").Value);
                    }
                }
                var users = deptNode.Elements("user");
                foreach(XElement userNode in users)
                {
                    treeView.Nodes[deptNode.Attribute("dn").Value].Nodes.Add(userNode.Attribute("dn").Value,
                           userNode.Element("sn").Value + " " + userNode.Element ("givenName").Value);
                }
            }
            
        }
        
        private void SearchBox_TextChanged(object sender, EventArgs e)
        {
            

            treeView.BeginUpdate();
            treeView.Nodes.Clear();

            if (searchBox.Text == string.Empty)
            {
                FillTree();
            }
            else
            {
                var users = doc.Root.Descendants("user")
                    .Where(t => t.Element("givenName").Value.ToLower().StartsWith(searchBox.Text.ToLower())
                    || t.Element("sn").Value.ToLower().StartsWith(searchBox.Text.ToLower())
                    || t.Attribute("name").Value.ToLower().StartsWith(searchBox.Text.ToLower()))
                    .ToList();

                foreach (XElement elem in users)
                    treeView.Nodes.Add(elem.Attribute("dn").Value, elem.Element("sn").Value + " " + elem.Element("givenName").Value);
                
            }

            treeView.EndUpdate();
        }

        // Buttons behaviour

        private void CreateBtn_Click(object sender, EventArgs e)
        {
            Form detailView = new DetailView();
            detailView.ShowDialog();
        }

        private void DetailBtn_Click(object sender, EventArgs e)
        {
            //Form detailView = new DetailView(user);
            //detailView.ShowDialog(this);
        }

        private void TreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (treeView.SelectedNode.Name.StartsWith("CN"))
            {

                user = new User(treeView.SelectedNode.Text);


				firstBox.Text = user.Properties["givenName"];
				lastBox.Text = user.Properties["sn"];


                
                cdCheck.Checked = user.MemberOf(Properties.Resources.cdGroup);
                usbDiskCheck.Checked = user.MemberOf(Properties.Resources.usbDiskGroup);
                usbDeviceCheck.Checked = user.MemberOf(Properties.Resources.usbDeviceGroup);

                if (user.MemberOf(Properties.Resources.internetFullAccessGroup))
                    internetCombo.SelectedIndex = 2;
                else
                {
                    if (user.MemberOf(Properties.Resources.internetLimitedAccessGroup))
                        internetCombo.SelectedIndex = 1;
                    else
                        internetCombo.SelectedIndex = 0;
                }

                disableBtn.Enabled = true;

                if (user.Enabled)
                {
                    switchPanel.Enabled = true;
                    disableBtn.Text = "Отключить аккаунт";
                }
                else
                {
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
            DumpADtoXML();
            doc = XDocument.Load(Properties.Resources.usersXML);
            FillTree();
            this.Enabled = true;
        }


        private void SaveBtn_Click(object sender, EventArgs e)
        {
            user.SetMembership(Properties.Resources.cdGroup, cdCheck.Checked);
			user.SetMembership(Properties.Resources.usbDiskGroup, usbDiskCheck.Checked);
			user.SetMembership(Properties.Resources.usbDeviceGroup,
				usbDeviceCheck.Checked);


			if (internetCombo.SelectedIndex == 1)
				user.AddGroup(Properties.Resources.internetLimitedAccessGroup);
            else
				user.RemoveGroup(Properties.Resources.internetLimitedAccessGroup);
			

			if (internetCombo.SelectedIndex == 2)
				user.AddGroup(Properties.Resources.internetFullAccessGroup);
			else
				user.RemoveGroup(Properties.Resources.internetFullAccessGroup);

			user.CommitChanges();
		}

        private void DisableBtn_Click(object sender, EventArgs e)
        {
            if (user.Enabled)
            {
                DisableReasonForm form = new DisableReasonForm();
                form.ShowDialog(this);

                if (form.DialogResult == DialogResult.OK)
                {
					user.Enabled = false;
                    switchPanel.Enabled = false;

					disableBtn.Text = "Активировать аккаунт";
				}
            }
            else
            {
				user.Enabled = true;
                switchPanel.Enabled = true;

				disableBtn.Text = "Отключить аккаунт";
			}
        }
    }
}
