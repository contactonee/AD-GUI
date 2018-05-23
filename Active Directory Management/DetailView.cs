using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.DirectoryServices;
using System.Xml;
using System.Xml.Linq;
using System.Linq;
using System.Diagnostics;

namespace Active_Directory_Management
{
    public partial class DetailView : Form
    {
        private DirectoryEntry ldapConnection = new DirectoryEntry(Properties.Resources.devAddr);
        


        public DetailView()
        {
            InitializeComponent();
            OnLoad();
            
        }
        public DetailView(DirectoryEntry user)
        {
            InitializeComponent();
            OnLoad();
        }

        private void OnLoad()
        {
            ldapConnection.AuthenticationType = AuthenticationTypes.Secure;


            cityCombo.Items.Add("Aktau");

            DirectorySearcher searcher = new DirectorySearcher(ldapConnection);
            searcher.Filter = "(&(objectClass=organizationalUnit))";
            searcher.PropertiesToLoad.Add("name");
            searcher.SearchScope = SearchScope.OneLevel;

            SearchResultCollection res = searcher.FindAll();
            foreach(SearchResult entry in res)
            {
                departmentCombo.Items.Add(entry.GetDirectoryEntry().Properties["name"].Value.ToString());
                
            }

            res.Dispose();
            searcher.Dispose();
            

            unlimitedRadio.Select();
            cityCombo.SelectedIndex = 0;
            internetCombo.SelectedIndex = 0;
            expirationDatePicker.Value = DateTime.Today.AddMonths(1);
            birthdayDatePicker.Value = DateTime.Today.AddYears(-70);
        }

        private bool CheckChar(string input)
        {
            foreach(char ch in input)
                if((ch < 'А' || ch > 'я') && ch != 'ё' && ch != 'Ё')
                    return false;
            return true;
        }
        private void MakeTranslit(TextBox sender, TextBox translit)
        {
            string input = sender.Text;
            if (!CheckChar(input))
            {
                sender.ForeColor = Color.Red;
                translit.Text = "";
                return;
            }
            sender.ForeColor = Color.Black;
            Dictionary<char, string> dict = new Dictionary<char, string>();
            dict['Й'] = "Y";
            dict['Ц'] = "C";
            dict['У'] = "U";
            dict['К'] = "K";
            dict['Е'] = "Ye";
            dict['Н'] = "N";
            dict['Г'] = "G";
            dict['Ш'] = "Sh";
            dict['Щ'] = "Shh";
            dict['З'] = "Z";
            dict['Х'] = "Kh";
            dict['Ф'] = "F";
            dict['Ы'] = "I";
            dict['В'] = "V";
            dict['А'] = "A";
            dict['П'] = "P";
            dict['Р'] = "R";
            dict['О'] = "O";
            dict['Л'] = "L";
            dict['Д'] = "D";
            dict['Ж'] = "Zh";
            dict['Э'] = "E";
            dict['Я'] = "Ya";
            dict['Ч'] = "Ch";
            dict['С'] = "S";
            dict['М'] = "M";
            dict['И'] = "I";
            dict['Т'] = "T";
            dict['Б'] = "B";
            dict['Ю'] = "Yu"; // Yuriyev
            dict['Ё'] = "Yo"; // Yozhikov

            dict['й'] = "y";
            dict['ц'] = "c";
            dict['у'] = "u";
            dict['к'] = "k";
            dict['е'] = "e";
            dict['н'] = "n";
            dict['г'] = "g";
            dict['ш'] = "sh";
            dict['щ'] = "shh";
            dict['з'] = "z";
            dict['х'] = "kh";
            dict['ф'] = "f";
            dict['ы'] = "i";
            dict['в'] = "v";
            dict['а'] = "a";
            dict['п'] = "p";
            dict['р'] = "r";
            dict['о'] = "o";
            dict['л'] = "l";
            dict['д'] = "d";
            dict['ж'] = "zh";
            dict['э'] = "e";
            dict['я'] = "ya";
            dict['ч'] = "ch";
            dict['с'] = "s";
            dict['м'] = "m";
            dict['и'] = "i";
            dict['т'] = "t";
            dict['ь'] = "i";
            dict['б'] = "b";
            dict['ю'] = "yu";
            dict['ё'] = "yo";


            string result = "";
            foreach (char ch in input)
                result += dict[ch];

            translit.Text = result;
        }
        private void CloseApplication(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void AddGroup(DirectoryEntry user, string groupDN)
        {
            DirectoryEntry group = new DirectoryEntry("LDAP://" + groupDN);
            group.Properties["member"].Add((string)user.Properties["distinguishedName"].Value);
            group.CommitChanges();
            group.Close();
            user.Close();
        }
        private void CreateButton(object sender, EventArgs e)
        {
            CreateUser();
        }
        private void CreateUser()
        {

            // Проверка занятости имени
            DirectorySearcher searcher = new DirectorySearcher("LDAP://DC=nng,DC=kz");
            string samAccountName = surnameTranslitTextBox.Text.Substring(0, Math.Min(surnameTranslitTextBox.Text.Length, 4)).ToLower() + nameTranslitTextBox.Text.Substring(0, 1).ToLower();
            int cnt = 1;
            searcher.Filter = "samAccountName=" + samAccountName + cnt.ToString();
            while (searcher.FindOne() != null)
            {
                cnt++;
                searcher.Filter = "samAccountName=" + samAccountName + cnt.ToString();
            }

            samAccountName += cnt.ToString();
            // Свободное имя найдено, создание аккаунта
            DirectoryEntry newUser = ldapConnection.Children.Add("cn=" + surnameTranslitTextBox.Text + " " + nameTranslitTextBox.Text, "user");

            // Set personal information
            newUser.Properties["samAccountName"].Value = samAccountName;
            newUser.Properties["userPrincipalName"].Value = samAccountName + "@nng.kz";
            newUser.Properties["givenName"].Value = nameTextBox.Text;
            newUser.Properties["sn"].Value = surnameTextBox.Text;
            newUser.Properties["displayName"].Value = surnameTranslitTextBox.Text + " " + nameTranslitTextBox.Text;
            newUser.Properties["middleName"].Value = middleNameTextBox.Text;
            newUser.Properties["mobile"].Value = mobileTextBox.Text;
            newUser.Properties["streetAddress"].Value = adressTextBox.Text;
            newUser.Properties["l"].Value = cityCombo.Text;
            if (cityCombo.SelectedIndex < 4)
                newUser.Properties["c"].Value = "KZ";
            else if (cityCombo.SelectedIndex == 4)
                newUser.Properties["c"].Value = "BY";
            else
                newUser.Properties["c"].Value = "RU";
            newUser.Properties["telephoneNumber"].Value = telCombo.Text;
            newUser.Properties["physicaldeliveryofficename"].Value = roomCombo.Text;
            newUser.Properties["extensionAttribute1"].Value = birthdayDatePicker.Value.ToString("dd.MM.yyyy");
            try
            {
                newUser.CommitChanges();
            }
            catch
            {
                MessageBox.Show("Проверьте правильность введенных данных", "Внимание", MessageBoxButtons.OK);
                return;
            }
            // End set personal information


            // Set password
            newUser.Invoke("SetPassword", new object[] { "12345678" });
            newUser.Properties["pwdLastSet"].Value = 0;
            newUser.CommitChanges();
            // End set password

            // Enable user
            newUser.Properties["userAccountControl"].Value = 0x200;
            newUser.CommitChanges();
            // End enable user

            // Add to groups
            if (cdCheck.Checked)
                AddGroup(newUser, "CN=CD,OU=TestOU,OU=Users,OU=Aktau,DC=nng,DC=kz");

            if (usbDiskCheck.Checked)
                AddGroup(newUser, "CN=USB Disk,OU=TestOU,OU=Users,OU=Aktau,DC=nng,DC=kz");

            if (usbDeviceCheck.Checked)
                AddGroup(newUser, "CN=USB Device,OU=TestOU,OU=Users,OU=Aktau,DC=nng,DC=kz");

            if (internetCombo.SelectedIndex == 1)
                AddGroup(newUser, "CN=Limited Access,OU=TestOU,OU=Users,OU=Aktau,DC=nng,DC=kz");

            if (internetCombo.SelectedIndex == 2)
                AddGroup(newUser, "CN=Full Access,OU=TestOU,OU=Users,OU=Aktau,DC=nng,DC=kz");

            if (limitedRadio.Checked)
            {
                newUser.Properties["accountExpires"].Value = expirationDatePicker.Value.AddDays(1).ToFileTime().ToString();
                newUser.CommitChanges();
            }
            // End add to groups

            newUser.Close();
        }

        private void limitedRadio_CheckedChanged(object sender, EventArgs e)
        {
            expirationDatePicker.Enabled = true;
        }

        private void unlimitedRadio_CheckedChanged(object sender, EventArgs e)
        {
            expirationDatePicker.Enabled = false;
        }

        private void cityCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            // departmentCombo List Update
            // if departments exist, enable departmentLabel and departmentCombo
            departmentLabel.Enabled = true;
            departmentCombo.Enabled = true;
            
            // otherwise disable
        }
        


        private void departmentCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Update divCombo
            //Update posCombo
            //Update rooms
            //Update telephones


            DirectorySearcher searcher = new DirectorySearcher(new DirectoryEntry("LDAP://OU=" + departmentCombo.Text + ",OU=Users,OU=Aktau,DC=nng,DC=kz"));

            searcher.Filter = "(&(objectClass=user))";
            searcher.PropertiesToLoad.Add("description");
            searcher.PropertiesToLoad.AddRange(new string[] {"description","title","physicaldeliveryofficename", "telephoneNumber" });
            searcher.SearchScope = SearchScope.OneLevel;

            var res = searcher.FindAll();
            HashSet<string> divs = new HashSet<string>();
            HashSet<string> poss = new HashSet<string>();
            HashSet<string> rooms = new HashSet<string>();
            HashSet<string> tels = new HashSet<string>();

            foreach (SearchResult entry in res)
            {
                DirectoryEntry trueEntry = entry.GetDirectoryEntry();

                if(trueEntry.Properties["title"].Value != null)
                    poss.Add(trueEntry.Properties["title"].Value.ToString());
                if (trueEntry.Properties["physicaldeliveryofficename"].Value != null)
                    rooms.Add(trueEntry.Properties["physicaldeliveryofficename"].Value.ToString());
                if (trueEntry.Properties["telephoneNumber"].Value != null)
                    tels.Add(trueEntry.Properties["telephoneNumber"].Value.ToString());
                if (trueEntry.Properties["description"].Value != null
                        && !trueEntry.Properties["description"].Value.ToString().StartsWith("("))
                    divs.Add(trueEntry.Properties["description"].Value.ToString());


            }

            divCombo.Items.Clear();
            posCombo.Items.Clear();
            roomCombo.Items.Clear();
            telCombo.Items.Clear();

            divCombo.ResetText();
            posCombo.ResetText();
            roomCombo.ResetText();
            telCombo.ResetText();


            if (divs.Count > 0)
            {
                divLabel.Enabled = true;
                divCombo.Enabled = true;
                foreach (string entry in divs)
                    divCombo.Items.Add(entry);
            }
            else
            {
                divLabel.Enabled = false;
                divCombo.Enabled = false;
            }

            foreach (string entry in poss)
                posCombo.Items.Add(entry);

            foreach (string entry in rooms)
                roomCombo.Items.Add(entry);

            foreach (string entry in tels)
                telCombo.Items.Add(entry);

        }
        private void nameTextBox_TextChanged(object sender, EventArgs e)
        {
            MakeTranslit(nameTextBox, nameTranslitTextBox);
            
            
        }
        private void surnameTextBox_TextChanged(object sender, EventArgs e)
        {
            MakeTranslit(surnameTextBox, surnameTranslitTextBox);
            
        }

        private void divCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            

        }
    }
}
