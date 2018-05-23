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
        
        

        public MainView()
        {
            InitializeComponent();
            // DumpADtoXML();
            FillTree();

        }

        

        private void DumpADtoXML()
        {
            // Настройка поисковика по записям
            // Поиск департаментов 
            DirectorySearcher searcher = new DirectorySearcher(entry);
            searcher.Filter = "(objectClass=organizationalUnit)";
            searcher.PropertiesToLoad.AddRange(new string[] {
                "name",
                "description",
                "title",
                "physicaldeliveryofficename",
                "telephoneNumber",
                "givenName",
                "sn",
                "department",
                "userPrincipalName",
                "memberOf",
            });
            searcher.SearchScope = SearchScope.OneLevel;

            var deptResults = searcher.FindAll();

            // Создание нового документа XML и создание в нем корня company
            XDocument doc = new XDocument();
            doc.Add(new XElement("company"));
            

            // Переход на уровень глубже по департаментам
            foreach (SearchResult deptRes in deptResults)
            {

                // Поиск субдепартаментов
                DirectoryEntry deptEntry = deptRes.GetDirectoryEntry();

                string deptName = deptEntry.Properties["name"].Value.ToString();
                XElement deptNode = new XElement("department",
                    new XAttribute("name", deptName),
                    new XAttribute("dn", deptEntry.Properties["distinguishedName"].Value.ToString()));

                searcher.SearchRoot = deptEntry;
                searcher.Filter = "(objectClass=organizationalUnit)";
                var divResults = searcher.FindAll();
                
                // Поиск людей в субдепартаментах
                foreach(SearchResult divRes in divResults)
                {
                    DirectoryEntry divEntry = divRes.GetDirectoryEntry();

                    string divName = divEntry.Properties["name"].Value.ToString();
                    XElement divNode = new XElement("subdepartment",
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
                            new XAttribute("dn", userEntry.Properties["distinguishedName"].Value.ToString()),
                            new XElement("firstName", userEntry.Properties["givenName"].Value),
                            new XElement("lastName", userEntry.Properties["sn"]),
                            new XElement("telephone", userEntry.Properties["telephoneNumber"]),
                            new XElement("room", userEntry.Properties["physicaldeliveryofficename"]),
                            new XElement("position", userEntry.Properties["title"]),
                            new XElement("group", userEntry.Properties["description"]),
                            new XElement("departmentRU", userEntry.Properties["department"]),
                            new XElement("logonName", userEntry.Properties["userPrincipalName"]),
                            new XElement("memberof", userEntry.Properties["memberof"]));
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
                            new XAttribute("dn", userEntry.Properties["distinguishedName"].Value.ToString()),
                            new XElement("firstName", userEntry.Properties["givenName"].Value),
                            new XElement("lastName", userEntry.Properties["sn"]),
                            new XElement("telephone", userEntry.Properties["telephoneNumber"]),
                            new XElement("room", userEntry.Properties["physicaldeliveryofficename"]),
                            new XElement("position", userEntry.Properties["title"]),
                            new XElement("group", userEntry.Properties["description"]),
                            new XElement("departmentRU", userEntry.Properties["department"]),
                            new XElement("logonName", userEntry.Properties["userPrincipalName"]),
                            new XElement("memberof", userEntry.Properties["memberof"]));

                    deptNode.Add(userNode);
                }

                doc.Root.Add(deptNode);
            }

            
            doc.Save("users.xml");
        }
               
        private void FillTree()
        {
            XDocument doc = XDocument.Load("users.xml");
            foreach(XElement deptNode in doc.Root.Elements())
            {
                treeView.Nodes.Add(deptNode.Attribute("dn").Value, deptNode.Attribute("name").Value);
                var divNodes = deptNode.Elements("subdepartment");
                foreach(XElement divNode in divNodes)
                {
                    treeView.Nodes[deptNode.Attribute("dn").Value].Nodes.Add(divNode.Attribute("dn").Value,
                           divNode.Attribute("name").Value);

                    var userNodes = divNode.Elements();
                    foreach(XElement userNode in userNodes)
                    {
                        treeView.Nodes[deptNode.Attribute("dn").Value].Nodes[divNode.Attribute("dn").Value].Nodes.Add(userNode.Attribute("dn").Value,
                           userNode.Attribute("name").Value);
                    }
                }
                var users = deptNode.Elements("user");
                foreach(XElement userNode in users)
                {
                    treeView.Nodes[deptNode.Attribute("dn").Value].Nodes.Add(userNode.Attribute("dn").Value,
                           userNode.Attribute("name").Value);
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
                XDocument doc = XDocument.Load("users.xml");
                


                var users = doc.Root.Descendants("user")
                    .Where(t => t.Element("firstName").Value.ToLower().StartsWith(searchBox.Text.ToLower())
                    || t.Element("lastName").Value.ToLower().StartsWith(searchBox.Text.ToLower())
                    || t.Attribute("name").Value.ToLower().StartsWith(searchBox.Text.ToLower())
                    || String.Concat(t.Element("firstName").Value, " ", t.Element("lastName").Value).ToLower().StartsWith(searchBox.Text.ToLower()))
                    .ToList();

                foreach (XElement elem in users)
                    treeView.Nodes.Add(elem.Attribute("dn").Value, elem.Attribute("name").Value);
                
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
            Form detailView = new DetailView(new DirectoryEntry());
            detailView.ShowDialog();
        }

        private void treeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (treeView.SelectedNode.Name.StartsWith("CN"))
            {
                XDocument doc = XDocument.Load("users.xml");
                switchPanel.Enabled = false;

                var res = doc.Root.Descendants("user")
                    .Where(t => t.Attribute("name").Value == treeView.SelectedNode.Text)
                    .Select(x => new { first = x.Element("firstName").Value, last = x.Element("lastName").Value })
                    .First();

                firstBox.Text = res.first;
                lastBox.Text = res.last;
                
                
                
                cdCheck.Checked = InGroup(treeView.SelectedNode.Text, Properties.Resources.cdGroup);
                usbDiskCheck.Checked = InGroup(treeView.SelectedNode.Text, Properties.Resources.usbDiskGroup);
                usbDeviceCheck.Checked = InGroup(treeView.SelectedNode.Text, Properties.Resources.usbDeviceGroup);

                if(InGroup(treeView.SelectedNode.Text, Properties.Resources.internetFullAccessGroup))
                    internetCombo.SelectedIndex = 2;
                else
                {
                    if (InGroup(treeView.SelectedNode.Text, Properties.Resources.internetLimitedAccessGroup))
                        internetCombo.SelectedIndex = 1;
                    else
                        internetCombo.SelectedIndex = 0;
                }

                switchPanel.Enabled = true;

            }
            else
                switchPanel.Enabled = false;
        }

        private bool InGroup(string name, string group)
        {
            DirectorySearcher searcher = new DirectorySearcher(Properties.Resources.devAddr);

            searcher.Filter = String.Format("(&(name={0})(memberof={1}))", name, group);
            SearchResult result = searcher.FindOne();
            searcher.Dispose();
            if (result != null)
                return true;
            else
                return false;

        }
        
        
    }
}
