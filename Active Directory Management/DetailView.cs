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
        private DirectoryEntry userEntry;


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
            // Set directory entry value
            userEntry = new DirectoryEntry("LDAP://" + user.Attribute("dn").Value);

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
			if (sender.ForeColor == Color.Black)
			{
				Dictionary<char, string> dict = new Dictionary<char, string>
				{
					['Й'] = "Y",
					['Ц'] = "C",
					['У'] = "U",
					['К'] = "K",
					['Е'] = "Ye",
					['Н'] = "N",
					['Г'] = "G",
					['Ш'] = "Sh",
					['Щ'] = "Shh",
					['З'] = "Z",
					['Х'] = "Kh",
					['Ф'] = "F",
					['Ы'] = "I",
					['В'] = "V",
					['А'] = "A",
					['П'] = "P",
					['Р'] = "R",
					['О'] = "O",
					['Л'] = "L",
					['Д'] = "D",
					['Ж'] = "Zh",
					['Э'] = "E",
					['Я'] = "Ya",
					['Ч'] = "Ch",
					['С'] = "S",
					['М'] = "M",
					['И'] = "I",
					['Т'] = "T",
					['Б'] = "B",
					['Ю'] = "Yu", // Yuriyev
					['Ё'] = "Yo", // Yozhikov

					['й'] = "y",
					['ц'] = "c",
					['у'] = "u",
					['к'] = "k",
					['е'] = "e",
					['н'] = "n",
					['г'] = "g",
					['ш'] = "sh",
					['щ'] = "shh",
					['з'] = "z",
					['х'] = "kh",
					['ф'] = "f",
					['ы'] = "i",
					['в'] = "v",
					['а'] = "a",
					['п'] = "p",
					['р'] = "r",
					['о'] = "o",
					['л'] = "l",
					['д'] = "d",
					['ж'] = "zh",
					['э'] = "e",
					['я'] = "ya",
					['ч'] = "ch",
					['с'] = "s",
					['м'] = "m",
					['и'] = "i",
					['т'] = "t",
					['ь'] = "i",
					['б'] = "b",
					['ю'] = "yu",
					['ё'] = "yo"
				};


				string result = "";
				foreach (char ch in input)
					result += dict[ch];

				translit.Text = result;
			}
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

        private void CreateButton(object sender, EventArgs e)
        {
            CreateUser();
        }

        private void CreateUser()
        {
            bool isNewUser = false;

            // If creating new user, create their entry first
            if (userEntry == null)
            {
                isNewUser = true;

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

                // Get path for organizational unit, where user will be created
                string ou_dn;
                if (subdepartmentCombo.SelectedIndex > 0)
                {
                    ou_dn = doc.Root.Descendants("subdept")
                        .Where(t => t.Attribute("name").Value == subdepartmentCombo.Text)
                        .Select(t => t.Attribute("dn").Value)
                        .First();
                }
                else
                {
                    ou_dn = doc.Root.Elements()
                        .Where(t => t.Attribute("russName").Value == departmentCombo.Text)
                        .Select(t => t.Attribute("dn").Value)
                        .First();
                }
                DirectoryEntry ou = new DirectoryEntry("LDAP://" + ou_dn);
                userEntry = ou.Children.Add("cn=" + surnameTranslitTextBox.Text + " " + nameTranslitTextBox.Text, "user");
                ou.Dispose();
                ou_dn = null;

                userEntry.Properties["samAccountName"].Value = samAccountName;
                userEntry.Properties["userPrincipalName"].Value = samAccountName + "@nng.kz";

                userEntry.Properties["givenName"].Value = nameTextBox.Text;
                userEntry.Properties["sn"].Value = surnameTextBox.Text;
                userEntry.Properties["displayName"].Value = surnameTranslitTextBox.Text + " " + nameTranslitTextBox.Text;
                userEntry.Properties["middleName"].Value = middleNameTextBox.Text;
                userEntry.Properties["mobile"].Value = mobileTextBox.Text;
                userEntry.Properties["l"].Value = cityCombo.Text;
                userEntry.Properties["department"].Value = departmentCombo.Text;
                userEntry.Properties["description"].Value = divCombo.Text;
                userEntry.Properties["title"].Value = posCombo.Text;

                userEntry.CommitChanges();



                // Set password
                userEntry.Invoke("SetPassword", new object[] { "1234567Bv" });
                userEntry.Properties["pwdLastSet"].Value = 0;
                userEntry.CommitChanges();
                // End set password

                // Enable user
                userEntry.Properties["userAccountControl"].Value = 0x200;
                userEntry.CommitChanges();
                // End enable user

            }

            // Set personal information

            userEntry.Properties["givenName"].Value = nameTextBox.Text;
            userEntry.Properties["sn"].Value = surnameTextBox.Text;
            userEntry.Properties["displayName"].Value = surnameTranslitTextBox.Text + " " + nameTranslitTextBox.Text;
            userEntry.Properties["middleName"].Value = middleNameTextBox.Text;
            userEntry.Properties["mobile"].Value = mobileTextBox.Text;
            userEntry.Properties["l"].Value = cityCombo.Text;
            userEntry.Properties["department"].Value = departmentCombo.Text;
            userEntry.Properties["description"].Value = divCombo.Text;
            userEntry.Properties["title"].Value = posCombo.Text;

            userEntry.CommitChanges();

            if (cityCombo.SelectedIndex < 4)
                userEntry.Properties["c"].Value = "KZ";
            else if (cityCombo.SelectedIndex == 4)
                userEntry.Properties["c"].Value = "BY";
            else
                userEntry.Properties["c"].Value = "RU";

            userEntry.Properties["telephoneNumber"].Value = telCombo.Text;
            userEntry.Properties["physicaldeliveryofficename"].Value = roomCombo.Text;
            userEntry.Properties["extensionAttribute2"].Value = birthdayDatePicker.Value.ToString("dd.MM.yyyy");
            try
            {
                userEntry.CommitChanges();
            }
            catch
            {
                MessageBox.Show("Ошибка, проверьте правильность введенных данных");
                return;
            }
            // End set personal information


            // Add to groups
            // HACK Temporarily disabled groups management - no access to modify membership
            if (cdCheck.Checked)
                AddGroup(userEntry, Properties.Resources.cdGroup);
            else
                RemoveGroup(userEntry, Properties.Resources.cdGroup);


            if (usbDiskCheck.Checked)
                AddGroup(userEntry, Properties.Resources.usbDiskGroup);
            else
                RemoveGroup(userEntry, Properties.Resources.usbDeviceGroup);


            if (usbDeviceCheck.Checked)
                AddGroup(userEntry, Properties.Resources.usbDeviceGroup);
            else
                RemoveGroup(userEntry, Properties.Resources.usbDeviceGroup);


            if (internetCombo.SelectedIndex == 1)
                AddGroup(userEntry, Properties.Resources.internetLimitedAccessGroup);
            else
                RemoveGroup(userEntry, Properties.Resources.internetLimitedAccessGroup);


            if (internetCombo.SelectedIndex == 2)
                AddGroup(userEntry, Properties.Resources.internetFullAccessGroup);
            else
                RemoveGroup(userEntry, Properties.Resources.internetFullAccessGroup);


            if (limitedRadio.Checked)
            {
                userEntry.Properties["accountExpires"].Value = expirationDatePicker.Value.AddDays(1).ToFileTime().ToString();
                userEntry.CommitChanges();
            }
			else
			{
				userEntry.Properties["accountExpires"].Value = new DateTime(5000, 1, 1);
			}
            // End add to groups

            if (isNewUser)
                MessageBox.Show("Пользователь был успешно создан!");
            else
                MessageBox.Show("Изменения были успешно сохранены!");
            this.Close();
        }

        private void LimitedRadio_CheckedChanged(object sender, EventArgs e)
        {
            expirationDatePicker.Enabled = true;
        }

        private void UnlimitedRadio_CheckedChanged(object sender, EventArgs e)
        { 
            expirationDatePicker.Enabled = false;
        }

        private void CityCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            // departmentCombo List Update
            // if departments exist, enable departmentLabel and departmentCombo
            departmentLabel.Enabled = true;
            departmentCombo.Enabled = true;
            
            // otherwise disable
        }

        

        private void DepartmentCombo_SelectedIndexChanged(object sender, EventArgs e)
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


        private void NameTextBox_TextChanged(object sender, EventArgs e)
        {
			if (nameTextBox.Text.ToLower().Min() < 'а' || nameTextBox.Text.ToLower().Max() > 'я')
				nameTextBox.ForeColor = Color.Red;
			else
				nameTextBox.ForeColor = Color.Black;

            MakeTranslit(nameTextBox, nameTranslitTextBox);            
        }
        private void SurnameTextBox_TextChanged(object sender, EventArgs e)
        {
			if (surnameTextBox.Text.ToLower().Min() < 'а' || surnameTextBox.Text.ToLower().Max() > 'я')
				surnameTextBox.ForeColor = Color.Red;
			else
				surnameTextBox.ForeColor = Color.Black;

			MakeTranslit(surnameTextBox, surnameTranslitTextBox); 
        }

        private void DivCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            

        }

        private void RoomCombo_TextChanged(object sender, EventArgs e)
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

        private void BirthdayDatePicker_ValueChanged(object sender, EventArgs e)
        {
            birthdayDatePicker.Format = DateTimePickerFormat.Short;
        }

        private void SubdepartmentCombo_SelectedIndexChanged(object sender, EventArgs e)
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

		private void nameTranslitTextBox_TextChanged(object sender, EventArgs e)
		{
			if (nameTranslitTextBox.Text.ToLower().Min() < 'a' || nameTranslitTextBox.Text.ToLower().Max() > 'z')
				nameTranslitTextBox.ForeColor = Color.Red;
			else
				nameTranslitTextBox.ForeColor = Color.Black;
		}

		private void surnameTranslitTextBox_TextChanged(object sender, EventArgs e)
		{
			if (surnameTranslitTextBox.Text.ToLower().Min() < 'a' || surnameTranslitTextBox.Text.ToLower().Max() > 'z')
				surnameTranslitTextBox.ForeColor = Color.Red;
			else
				surnameTranslitTextBox.ForeColor = Color.Black;
		}
	}
}
