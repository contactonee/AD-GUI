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
            DumpADtoXML();
            

        }

        

        private void DumpADtoXML()
        {
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
                "memberOf"
            });
            searcher.SearchScope = SearchScope.OneLevel;

            var deptResults = searcher.FindAll();

            XDocument doc = new XDocument();
            doc.Add(new XElement("company"));
            


            foreach (SearchResult deptRes in deptResults)
            {
                DirectoryEntry deptEntry = deptRes.GetDirectoryEntry();

                string deptName = deptEntry.Properties["name"].Value.ToString();
                XElement deptNode = new XElement("department", new XAttribute("name", deptName));

                searcher.SearchRoot = deptEntry;
                searcher.Filter = "(objectClass=organizationalUnit)";
                var divResults = searcher.FindAll();
                
                foreach(SearchResult divRes in divResults)
                {
                    DirectoryEntry divEntry = divRes.GetDirectoryEntry();

                    string divName = divEntry.Properties["name"].Value.ToString();
                    XElement divNode = new XElement("subdepartment", new XAttribute("name", divName));


                    searcher.Filter = "(objectClass=user)";
                    searcher.SearchRoot = divEntry;
                    var userResults = searcher.FindAll();

                    foreach (SearchResult userRes in userResults)
                    {
                        DirectoryEntry userEntry = userRes.GetDirectoryEntry();


                        XElement userNode = new XElement("user",
                            new XAttribute("name", userEntry.Properties["name"].Value),
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
               

        

        /*
        private void PopulateTree()
        {

            DirectorySearcher searcher = new DirectorySearcher(entry);
            searcher.Filter = "(&(objectClass=user))";
            searcher.PropertiesToLoad.Add("name");
            searcher.SearchScope = SearchScope.Subtree;

            var res = searcher.FindAll();
            List <string> names = new List<string>();
            foreach(SearchResult curr_res in res)
            {
                DirectoryEntry user = curr_res.GetDirectoryEntry();
                names.Add(user.Properties["name"].Value.ToString());
               
            }
            names.Sort();
            foreach(string name in names)
            {
                listView.Items.Add(name);
                listViewCache.Items.Add(name);
            }
            



            
        }
       
        
        private void SearchBox_TextChanged(object sender, EventArgs e)
        {
            if (searchBox.Text == string.Empty)
            {
                searchBoxPlaceholder.Visible = true;
            }
            else
                searchBoxPlaceholder.Visible = false;

            listView.BeginUpdate();
            listView.Items.Clear();

            if (searchBox.Text == "Поиск" || searchBox.Text == string.Empty)
            {
                listView.Items.AddRange(listViewCache.Items);
            }
            else
            {
                foreach(ListViewItem item in listViewCache.Items)
                {
                    if(item.Text.StartsWith(searchBox.Text))
                    {
                        listView.Items.Add(item);
                    }
                }
            }

            listView.EndUpdate();
        }

        
    */
       

        

        private void listView_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            cdCheck.Enabled = true;
            usbDiskCheck.Enabled = true;
            usbDeviceCheck.Enabled = true;
            cloudCheck.Enabled = true;
            internetLabel.Enabled = true;
            internetCombo.Enabled = true;
            detailBtn.Enabled = true;
            saveBtn.Enabled = true;
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
    }
}
