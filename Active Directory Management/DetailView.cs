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
			OnLoad();

			this.user = user;
			this.Text = user.Name;

			saveBtn.Text = "Сохранить Изменения";

			nameBox.Text = user.GetProperty("givenName");
			surnameBox.Text = user.GetProperty("sn");
			familyNameBox.Text = user.GetProperty("middleName");

			try
			{
				surnameEnBox.Text = user.Name.Split(' ')[0];
				nameEnBox.Text = user.Name.Split(' ')[1];
			}
			catch
			{
				nameEnBox.Text = user.Name;
				surnameEnBox.Text = string.Empty;
			}
			mobileTextBox.Text = user.GetProperty("mobile");

			try
			{
				birthdayDatePicker.Value = DateTime.Parse(user.GetProperty("extenstionAttribute2"));
			}
			catch
			{
				birthdayDatePicker.Format = DateTimePickerFormat.Custom;
				birthdayDatePicker.CustomFormat = " ";
			}

			departmentCombo.SelectedIndex = departmentCombo.Items.IndexOf(user.GetProperty("department"));
			divCombo.Text = user.GetProperty("description");
			posCombo.Text = user.GetProperty("title");
			roomCombo.Text = user.GetProperty("physicalDeliveryOfficeName");
			telCombo.Text = user.GetProperty("telephoneNumber");

			cdCheck.Checked = user.MemberOf(Properties.Groups.DvdDrives);
			usbDiskCheck.Checked = user.MemberOf(Properties.Groups.DvdDrives);
			usbDeviceCheck.Checked = user.MemberOf(Properties.Groups.UsbDevices);

			if (user.MemberOf(Properties.Groups.InternetFull))
				internetCombo.SelectedIndex = 2;
			else if (user.MemberOf(Properties.Groups.InternetLimited))
				internetCombo.SelectedIndex = 1;
			else
				internetCombo.SelectedIndex = 0;


			
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
                .Select(t => t.Attribute("nameRU").Value)
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

        private void CreateButton(object sender, EventArgs e)
        {
            CreateUser();
        }

        private void CreateUser()
        {
			string displayName = surnameEnBox.Text + " " + nameEnBox.Text;
			bool newUser = false;

			if (user == null)
			{
				XElement par = doc.Root.Elements("dept")
					.Where(t => t.Attribute("nameRU").Value == departmentCombo.Text)
					.FirstOrDefault();

				DirectoryEntry ou = new DirectoryEntry("LDAP://" + par.Attribute("dn").Value);

				user = new User(displayName, ou, par);
				newUser = true;
			}

			user.Properties["givenName"] = nameBox.Text;
			user.Properties["sn"] = surnameBox.Text;
			user.Properties["displayName"] = displayName;
			user.Properties["middleName"] = familyNameBox.Text;
			user.Properties["mobile"] = mobileTextBox.Text;
			user.Properties["l"] = cityCombo.Text;
			user.Properties["department"] = departmentCombo.Text;
			user.Properties["description"] = divCombo.Text;
			user.Properties["title"] = posCombo.Text;
			user.Properties["telephoneNumber"] = telCombo.Text;
			user.Properties["ipPhone"] = telCombo.Text;
			user.Properties["physicalDeliveryOfficeName"] = roomCombo.Text;
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


			var users = doc.Root.Elements("dept")
				.Where(t => t.Attribute("nameRU").Value == departmentCombo.Text)
				.Select(t => t.Elements("user"))
				.First();

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

				if (elem.Element("physicalDeliveryOfficeName").Value.Trim() != string.Empty
						&& !elem.Element("physicalDeliveryOfficeName").Value.StartsWith("("))
					diffRooms.Add(elem.Element("physicalDeliveryOfficeName").Value);

				if (elem.Element("telephoneNumber").Value.Trim() != string.Empty
						&& !elem.Element("telephoneNumber").Value.StartsWith("("))
					diffTels.Add(elem.Element("telephoneNumber").Value);
			}

			divCombo.BeginUpdate();
			posCombo.BeginUpdate();
			roomCombo.BeginUpdate();
			telCombo.BeginUpdate();

			divCombo.Items.Clear();
			posCombo.Items.Clear();
			roomCombo.Items.Clear();
			telCombo.Items.Clear();

			divCombo.Items.AddRange(diffDivs.ToArray());
			posCombo.Items.AddRange(diffPoss.ToArray());
			roomCombo.Items.AddRange(diffRooms.ToArray());
			telCombo.Items.AddRange(diffTels.ToArray());

			divCombo.EndUpdate();
			posCombo.EndUpdate();
			roomCombo.EndUpdate();
			telCombo.EndUpdate();
		}

        private void UpdateCombos(XElement[] users)
        {
			// Common code after department change

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

                if (elem.Element("physicalDeliveryOfficeName").Value.Trim() != string.Empty
                        && !elem.Element("physicalDeliveryOfficeName").Value.StartsWith("("))
                    diffRooms.Add(elem.Element("physicalDeliveryOfficeName").Value);

                if (elem.Element("telephoneNumber").Value.Trim() != string.Empty
                        && !elem.Element("telephoneNumber").Value.StartsWith("("))
                    diffTels.Add(elem.Element("telephoneNumber").Value);
            }

			divCombo.BeginUpdate();
			posCombo.BeginUpdate();
			roomCombo.BeginUpdate();
			telCombo.BeginUpdate();

			divCombo.Items.Clear();
            posCombo.Items.Clear();
            roomCombo.Items.Clear();
            telCombo.Items.Clear();

            divCombo.Items.AddRange(diffDivs.ToArray());
            posCombo.Items.AddRange(diffPoss.ToArray());
            roomCombo.Items.AddRange(diffRooms.ToArray());
            telCombo.Items.AddRange(diffTels.ToArray());

			// Debug.WriteLine(diffDiv.)

			divCombo.EndUpdate();
			posCombo.EndUpdate();
			roomCombo.EndUpdate();
			telCombo.EndUpdate();

		}


        private void NameTextBox_TextChanged(object sender, EventArgs e)
        {
			if (nameBox.Text.Length > 0
					&& (nameBox.Text.ToLower().Min() < 'а'
					|| nameBox.Text.ToLower().Max() > 'я'))
				nameBox.ForeColor = Color.Red;
			else
			{
				nameBox.ForeColor = Color.Black;
				nameEnBox.Text = Translit(nameBox.Text);
			}
        }
        private void SurnameTextBox_TextChanged(object sender, EventArgs e)
        {
			if (surnameBox.Text.Length > 0
					&& (surnameBox.Text.ToLower().Min() < 'а'
					|| surnameBox.Text.ToLower().Max() > 'я'))
				surnameBox.ForeColor = Color.Red;
			else
			{
				surnameBox.ForeColor = Color.Black;
				surnameEnBox.Text = Translit(surnameBox.Text);
			}
		}

		private void NameTranslitTextBox_TextChanged(object sender, EventArgs e)
		{
			if (nameEnBox.Text.Length > 0
					&& (nameEnBox.Text.ToLower().Min() < 'a'
					|| nameEnBox.Text.ToLower().Max() > 'z'))
				nameEnBox.ForeColor = Color.Red;
			else
				nameEnBox.ForeColor = Color.Black;
		}

		private void SurnameTranslitTextBox_TextChanged(object sender, EventArgs e)
		{
			if (surnameEnBox.Text.Length > 0
					&& (surnameEnBox.Text.ToLower().Min() < 'a'
					|| surnameEnBox.Text.ToLower().Max() > 'z'))
				surnameEnBox.ForeColor = Color.Red;
			else
				surnameEnBox.ForeColor = Color.Black;
		}

		private void DivCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            

        }

        private void RoomCombo_TextChanged(object sender, EventArgs e)
        {
            var res = doc.Root.Descendants("user")
                .Where(t => t.Element("physicalDeliveryOfficeName").Value == roomCombo.Text)
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
		
	}
}
