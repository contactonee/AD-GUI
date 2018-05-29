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
using System.Xml.XPath;


namespace Active_Directory_Management
{
    public partial class DetailView : Form
    {
        private DirectoryEntry ldapConnection = new DirectoryEntry(Properties.Resources.devAddr);
        private XDocument doc = XDocument.Load("users.xml");


        public DetailView()
        {
            InitializeComponent();
            OnLoad();
        }
        public DetailView(XElement user)
        {
            InitializeComponent();
            OnLoad();
            LoadUser(user);

            
        }

        private void OnLoad()
        {
            cityCombo.Items.Add("Актау");
            cityCombo.Enabled = false;


            unlimitedRadio.Select();
            cityCombo.SelectedIndex = 0;
            internetCombo.SelectedIndex = 0;
            expirationDatePicker.Value = DateTime.Today.AddMonths(1);
            mobileTextBox.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;

            var res = doc.Root.Elements("dept")
                .Select(t => t.Attribute("russName").Value)
                .ToArray();
            departmentCombo.Items.AddRange(res);                                                                                       

        }

        private void LoadUser(XElement user)
        {
            // Rename window heading
            this.Text = user.Element("sn").Value + " " + user.Element("givenName").Value;

            // Rename submit button
            create.Text = "Сохранить изменения";

            // Fill name fields
            nameTextBox.Text = user.Element("givenName").Value;
            surnameTextBox.Text = user.Element("sn").Value;
            middleNameTextBox.Text = user.Element("middleName").Value;


            // Перезапись после автотранслита для точного соответствия с базой
            try
            {
                surnameTranslitTextBox.Text = user.Attribute("name").Value.Split(' ')[0];
                nameTranslitTextBox.Text = user.Attribute("name").Value.Split(' ')[1];
            }
            catch
            {
                nameTranslitTextBox.Text = user.Attribute("name").Value.Split(' ')[0];
                surnameTranslitTextBox.Clear();
            }

            // Mobile number
            mobileTextBox.Text = user.Element("mobile").Value;

            // Try to set birthday, if not specified, set empty field
            try
            {
                birthdayDatePicker.Value = DateTime.Parse(user.Element("extensionAttribute2").Value);
            }
            catch
            {
                birthdayDatePicker.Format = DateTimePickerFormat.Custom;
                birthdayDatePicker.CustomFormat = " ";
            }

            // Select department
            if(user.Parent.Name == "dept")
            {
                departmentCombo.SelectedIndex = departmentCombo.Items.IndexOf(user.Parent.Attribute("russName").Value);
            }
            else
            {
                departmentCombo.SelectedIndex = departmentCombo.Items.IndexOf(user.Parent.Parent.Attribute("russName").Value);
                subdepartmentCombo.SelectedIndex = subdepartmentCombo.Items.IndexOf(user.Parent.Attribute("name").Value);
            }

            // Lock department combos
            departmentCombo.Enabled = false;
            subdepartmentCombo.Enabled = false;

            

            // Fill editable fields regarding position in company
            divCombo.Text = user.Element("description").Value;
            posCombo.Text = user.Element("title").Value;
            roomCombo.Text = user.Element("physicaldeliveryofficename").Value;
            telCombo.Text = user.Element("telephoneNumber").Value;

            // Second tab (options)

            // Check membership in groups and tick checkboxes
            cdCheck.Checked = user.Element("memberOf").Value.Contains(Properties.Resources.cdGroup);
            usbDiskCheck.Checked = user.Element("memberOf").Value.Contains(Properties.Resources.usbDiskGroup);
            usbDeviceCheck.Checked = user.Element("memberOf").Value.Contains(Properties.Resources.usbDeviceGroup);

            // Determine the level of internet access and set combobox
            if (user.Element("memberOf").Value.Contains(Properties.Resources.internetFullAccessGroup))
                internetCombo.SelectedIndex = 2;
            else
            {
                if (user.Element("memberOf").Value.Contains(Properties.Resources.internetLimitedAccessGroup))
                    internetCombo.SelectedIndex = 1;
                else
                    internetCombo.SelectedIndex = 0;
            }

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


            XElement deptElem = doc.Root.Elements("dept")
                .Where(t => t.Attribute("russName").Value == departmentCombo.Text)
                .First();

            

            subdepartmentCombo.BeginUpdate();
            subdepartmentCombo.Items.Clear();
            subdepartmentCombo.Items.Add("-");
            subdepartmentCombo.SelectedIndex = 0;

            foreach (XElement elem in deptElem.Elements("subdept"))
                subdepartmentCombo.Items.Add(elem.Attribute("name").Value);
            
            subdepartmentCombo.EndUpdate();

            UpdateCombos(deptElem.Descendants("user").ToArray());
            

        }

        private void UpdateCombos(XElement[] users)
        {
            // Common code after department or subdepartment change

            HashSet<string> diffDivs = new HashSet<string>();
            HashSet<string> diffPoss = new HashSet<string>();
            HashSet<string> diffRooms = new HashSet<string>();
            HashSet<string> diffTels = new HashSet<string>();

            foreach (XElement elem in users)
            {
                if (elem.Element("description").Value.Trim() != string.Empty
                        && !elem.Element("description").Value.StartsWith("("))
                    diffDivs.Add(elem.Element("description").Value);

                if (elem.Element("title").Value.Trim() != string.Empty
                        && !elem.Element("title").Value.StartsWith("("))
                    diffPoss.Add(elem.Element("title").Value);

                if (elem.Element("physicaldeliveryofficename").Value.Trim() != string.Empty
                        && !elem.Element("physicaldeliveryofficename").Value.StartsWith("("))
                    diffRooms.Add(elem.Element("physicaldeliveryofficename").Value);

                if (elem.Element("telephoneNumber").Value.Trim() != string.Empty
                        && !elem.Element("telephoneNumber").Value.StartsWith("("))
                    diffTels.Add(elem.Element("telephoneNumber").Value);
            }

            divCombo.Items.Clear();
            posCombo.Items.Clear();
            roomCombo.Items.Clear();
            telCombo.Items.Clear();

            divCombo.Items.AddRange(diffDivs.ToArray());
            posCombo.Items.AddRange(diffPoss.ToArray());
            roomCombo.Items.AddRange(diffRooms.ToArray());
            telCombo.Items.AddRange(diffTels.ToArray());

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

        private void roomCombo_TextChanged(object sender, EventArgs e)
        {
            var res = doc.Root.Descendants("user")
                .Where(t => t.Element("physicaldeliveryofficename").Value == roomCombo.Text)
                .Select(t => t.Element("telephoneNumber").Value);

            HashSet<string> diffTels = new HashSet<string>();

            foreach (string elem in res)
                diffTels.Add(elem);

            telCombo.Items.Clear();
            telCombo.Items.AddRange(diffTels.ToArray());
        }

        private void birthdayDatePicker_ValueChanged(object sender, EventArgs e)
        {
            birthdayDatePicker.Format = DateTimePickerFormat.Short;
        }

        private void subdepartmentCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (subdepartmentCombo.SelectedIndex > 0)
            {
                
                UpdateCombos(doc.Root.Descendants("subdept")
                    .Where(t => t.Attribute("name").Value == subdepartmentCombo.Text)
                    .Select(t => t.Descendants("user"))
                    .First()
                    .ToArray());
            }
            else
            {
                UpdateCombos(doc.Root.Elements("dept")
                    .Where(t => t.Attribute("russName").Value == departmentCombo.Text)
                    .Select(t => t.Descendants("user"))
                    .First()
                    .ToArray());
            }
            
        }
    }
}
