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
        private XElement SelectedUser;
        private DirectoryEntry SelectedEntry;

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
            "samaccountname",
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
            Form detailView = new DetailView(SelectedUser);
            detailView.ShowDialog(this);
        }

        private void TreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (treeView.SelectedNode.Name.StartsWith("CN"))
            {

                SelectedUser = doc.Root.Descendants("user")
                    .Where(t => t.Element("sn").Value + " " + t.Element("givenName").Value == treeView.SelectedNode.Text)
                    .First();

                SelectedEntry = new DirectoryEntry("LDAP://" + SelectedUser.Attribute("dn").Value,
                    "bazhr1",
                    "1234567Br",
                    AuthenticationTypes.Secure);


                firstBox.Text = SelectedUser.Element("givenName").Value;
                lastBox.Text = SelectedUser.Element("sn").Value;


                
                cdCheck.Checked = SelectedUser.Element("memberOf").Value.Contains(Properties.Resources.cdGroup);
                usbDiskCheck.Checked = SelectedUser.Element("memberOf").Value.Contains(Properties.Resources.usbDiskGroup);
                usbDeviceCheck.Checked = SelectedUser.Element("memberOf").Value.Contains(Properties.Resources.usbDeviceGroup);

                if (SelectedUser.Element("memberOf").Value.Contains(Properties.Resources.internetFullAccessGroup))
                    internetCombo.SelectedIndex = 2;
                else
                {
                    if (SelectedUser.Element("memberOf").Value.Contains(Properties.Resources.internetLimitedAccessGroup))
                        internetCombo.SelectedIndex = 1;
                    else
                        internetCombo.SelectedIndex = 0;
                }

                disableBtn.Enabled = true;

                if (!Convert.ToBoolean(int.Parse(SelectedUser.Element("userAccountControl").Value) & 0x2))
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

        private void AddGroup(DirectoryEntry user, string groupDN)
        {
            DirectoryEntry group = new DirectoryEntry("LDAP://" + groupDN);
            group.Properties["member"].Add((string)user.Properties["distinguishedName"].Value);
            group.CommitChanges();
            group.Close();
            user.Close();
        }

        private void RemoveGroup(DirectoryEntry user, string groupDN)
        {
            DirectoryEntry group = new DirectoryEntry("LDAP://" + groupDN);
            group.Properties["member"].Remove((string)user.Properties["distinguishedName"].Value);
            group.CommitChanges();
            group.Close();
            user.Close();
        }


        private void SaveBtn_Click(object sender, EventArgs e)
        {
            if (cdCheck.Checked)
                AddGroup(SelectedEntry, Properties.Resources.cdGroup);
            else
                RemoveGroup(SelectedEntry, Properties.Resources.cdGroup);


            if (usbDiskCheck.Checked)
                AddGroup(SelectedEntry, Properties.Resources.usbDiskGroup);
            else
                RemoveGroup(SelectedEntry, Properties.Resources.usbDeviceGroup);


            if (usbDeviceCheck.Checked)
                AddGroup(SelectedEntry, Properties.Resources.usbDeviceGroup);
            else
                RemoveGroup(SelectedEntry, Properties.Resources.usbDeviceGroup);


            if (internetCombo.SelectedIndex == 1)
                AddGroup(SelectedEntry, Properties.Resources.internetLimitedAccessGroup);
            else
                RemoveGroup(SelectedEntry, Properties.Resources.internetLimitedAccessGroup);


            if (internetCombo.SelectedIndex == 2)
                AddGroup(SelectedEntry, Properties.Resources.internetFullAccessGroup);
            else
                RemoveGroup(SelectedEntry, Properties.Resources.internetFullAccessGroup);
        }

        private void DisableBtn_Click(object sender, EventArgs e)
        {
            int val = (int)SelectedEntry.Properties["userAccountControl"].Value;
            string currDescription = SelectedEntry.Properties["description"].Value.ToString();

            if (switchPanel.Enabled)
            {
                DisableReasonForm form = new DisableReasonForm();
                form.ShowDialog(this);
                if (form.DialogResult == DialogResult.OK)
                {
                    SelectedEntry.Properties["userAccountControl"].Value = val | 0x2;

                    SelectedEntry.Properties["description"].Value = String.Format(
                        "(Временно отключена {0}, {1}, причина: {2}) {3}",
                        DateTime.Today.ToShortDateString(),
                        SelectedEntry.Username,
                        form.reasonTextBox.Text.ToLower(),
                        currDescription);


                    SelectedUser.Element("userAccountControl").Value = (val | 0x2).ToString();
                    disableBtn.Text = "Активировать аккаунт";
                    switchPanel.Enabled = false;
                }
            }
            else
            {
                SelectedEntry.Properties["userAccountControl"].Value = val & ~0x2;
                SelectedEntry.Properties["description"].Value =
                    currDescription.Substring(currDescription.LastIndexOf(')') + 2);

                SelectedUser.Element("userAccountControl").Value = (val & ~0x2).ToString();
                disableBtn.Text = "Отключить аккаунт";
                switchPanel.Enabled = true;
            }
            SelectedEntry.CommitChanges();
        }
    }
}
