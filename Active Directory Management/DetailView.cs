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
        private XDocument doc = XDocument.Load(Properties.Resources.XmlFile);
		private User user;


        public DetailView()
        {
            InitializeComponent();
            OnLoad();
        }
        public DetailView(User user)
		{
			InitializeComponent();

			this.user = user;
			this.Text = user.Name;

			create.Text = "Сохранить Изменения";

			nameTextBox.Text = user.Properties["givenName"];
			surnameTextBox.Text = user.Properties["sn"];
			middleNameTextBox.Text = user.Properties["middleName"];

			try
			{
				surnameTranslitTextBox.Text = user.Name.Split(' ')[0];
				nameTranslitTextBox.Text = user.Name.Split(' ')[1];
			}
			catch
			{
				nameTranslitTextBox.Text = user.Name;
				surnameTranslitTextBox.Text = string.Empty;
			}
			mobileTextBox.Text = user.Properties["mobile"];

			try
			{
				birthdayDatePicker.Value = DateTime.Parse(user.Properties["extenstionAttribute2"]);
			}
			catch
			{
				birthdayDatePicker.Format = DateTimePickerFormat.Custom;
				birthdayDatePicker.CustomFormat = " ";
			}

			departmentCombo.SelectedIndex = departmentCombo.Items.IndexOf(user.Properties["department"]);
			divCombo.Text = user.Properties["description"];
			posCombo.Text = user.Properties["title"];
			roomCombo.Text = user.Properties["physicalDeliveryOfficeName"];
			telCombo.Text = user.Properties["telephoneNumber"];

			cdCheck.Checked = user.MemberOf(Properties.Groups.DvdDrives);
			usbDiskCheck.Checked = user.MemberOf(Properties.Groups.DvdDrives);
			usbDeviceCheck.Checked = user.MemberOf(Properties.Groups.UsbDevices);

			if (user.MemberOf(Properties.Groups.InternetFull))
				internetCombo.SelectedIndex = 2;
			else if (user.MemberOf(Properties.Groups.InternetLimited))
				internetCombo.SelectedIndex = 1;
			else
				internetCombo.SelectedIndex = 0;


			OnLoad();
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
		
		private string Translit(string text)
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
            foreach (char ch in text)
                result += dict[ch];

			return result;
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
			string displayName = surnameTranslitTextBox.Text + " " + nameTranslitTextBox.Text;
			bool newUser = false;

			if (user == null)
			{
				XElement par = doc.Root.Elements("dept")
					.Where(t => t.Attribute("russName").Value == departmentCombo.Text)
					.FirstOrDefault();

				DirectoryEntry ou = new DirectoryEntry("LDAP://" + par.Attribute("dn").Value);

				user = new User(displayName, ou, par);
				newUser = true;
			}

			user.Properties["givenName"] = nameTextBox.Text;
			user.Properties["sn"] = surnameTextBox.Text;
			user.Properties["displayName"] = displayName;
			user.Properties["middleName"] = middleNameTextBox.Text;
			user.Properties["mobile"] = mobileTextBox.Text;
			user.Properties["l"] = cityCombo.Text;
			user.Properties["department"] = departmentCombo.Text;
			user.Properties["description"] = divCombo.Text;
			user.Properties["title"] = posCombo.Text;
			user.Properties["telephoneNumber"] = telCombo.Text;
			user.Properties["ipPhone"] = telCombo.Text;
			user.Properties["physicaldeliveryofficename"] = roomCombo.Text;
			user.Properties["extensionAttribute2"] = birthdayDatePicker.Value.ToString("dd.MM.yyyy");
			if (limitedRadio.Checked)
				user.Properties["accountExpires"] = expirationDatePicker.Value.AddDays(1)
					.ToFileTime()
					.ToString();

			user.CommitChanges();

			try
			{
				user.SetMembership(Properties.Groups.DvdDrives, cdCheck.Checked);
				user.SetMembership(Properties.Groups.UsbDrives, usbDiskCheck.Checked);
				user.SetMembership(Properties.Groups.UsbDevices, usbDeviceCheck.Checked);
				user.SetMembership(Properties.Groups.InternetLimited, internetCombo.SelectedIndex == 1);
				user.SetMembership(Properties.Groups.InternetFull, internetCombo.SelectedIndex == 2);
			}
			catch
			{
				Debug.WriteLine("Невозможно назначить группы, возможно не хватает прав");
			}

			if(newUser)
			{
				MessageBox.Show("Пользователь успешно создан");
				this.Close();
			}
			else
			{
				MessageBox.Show("Изменения успешно сохранены");
			}
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
			if (nameTextBox.Text.Length > 0
					&& (nameTextBox.Text.ToLower().Min() < 'а'
					|| nameTextBox.Text.ToLower().Max() > 'я'))
				nameTextBox.ForeColor = Color.Red;
			else
			{
				nameTextBox.ForeColor = Color.Black;
				nameTranslitTextBox.Text = Translit(nameTextBox.Text);
			}
        }
        private void SurnameTextBox_TextChanged(object sender, EventArgs e)
        {
			if (surnameTextBox.Text.Length > 0
					&& (surnameTextBox.Text.ToLower().Min() < 'а'
					|| surnameTextBox.Text.ToLower().Max() > 'я'))
				surnameTextBox.ForeColor = Color.Red;
			else
			{
				surnameTextBox.ForeColor = Color.Black;
				surnameTranslitTextBox.Text = Translit(surnameTextBox.Text);
			}
		}

		private void NameTranslitTextBox_TextChanged(object sender, EventArgs e)
		{
			if (nameTranslitTextBox.Text.Length > 0
					&& (nameTranslitTextBox.Text.ToLower().Min() < 'a'
					|| nameTranslitTextBox.Text.ToLower().Max() > 'z'))
				nameTranslitTextBox.ForeColor = Color.Red;
			else
				nameTranslitTextBox.ForeColor = Color.Black;
		}

		private void SurnameTranslitTextBox_TextChanged(object sender, EventArgs e)
		{
			if (surnameTranslitTextBox.Text.Length > 0
					&& (surnameTranslitTextBox.Text.ToLower().Min() < 'a'
					|| surnameTranslitTextBox.Text.ToLower().Max() > 'z'))
				surnameTranslitTextBox.ForeColor = Color.Red;
			else
				surnameTranslitTextBox.ForeColor = Color.Black;
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

		
	}
}
