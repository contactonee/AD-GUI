using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.DirectoryServices;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Active_Directory_Management
{
    public partial class MainView : Form
    {

        private DirectoryEntry entry = new DirectoryEntry(Properties.Resources.devAddr);
        private XDocument doc = XDocument.Load("users.xml");
        private XElement currUser;

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
        };


        public MainView()
        {
            InitializeComponent();
            FillTree();
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
            DirectorySearcher searcher = new DirectorySearcher(entry);
            searcher.SearchScope = SearchScope.OneLevel;

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

            
            xmlfile.Save("users.xml");
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
            Form detailView = new DetailView(currUser);
            detailView.ShowDialog();
        }

        private void treeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (treeView.SelectedNode.Name.StartsWith("CN"))
            {

                currUser = doc.Root.Descendants("user")
                    .Where(t => t.Element("sn").Value + " " + t.Element("givenName").Value == treeView.SelectedNode.Text)
                    .First();

                
                firstBox.Text = currUser.Element("givenName").Value;
                lastBox.Text = currUser.Element("sn").Value;

                
                cdCheck.Checked = InGroup(currUser, Properties.Resources.cdGroup);
                usbDiskCheck.Checked = InGroup(currUser, Properties.Resources.usbDiskGroup);
                usbDeviceCheck.Checked = InGroup(currUser, Properties.Resources.usbDeviceGroup);

                if(InGroup(currUser, Properties.Resources.internetFullAccessGroup))
                    internetCombo.SelectedIndex = 2;
                else
                {
                    if (InGroup(currUser, Properties.Resources.internetLimitedAccessGroup))
                        internetCombo.SelectedIndex = 1;
                    else
                        internetCombo.SelectedIndex = 0;
                }

                switchPanel.Enabled = true;

            }
            else
                switchPanel.Enabled = false;
        }

        private bool InGroup(XElement user, string group)
        {


            String groups = user.Element("memberOf").Value;

            return groups.Contains(group);

        }

        private void updBtn_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            DumpADtoXML();
            FillTree();
            this.Enabled = true;
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            

        }
    }
}
