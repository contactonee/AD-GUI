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
		private bool changed = false;

		private XDocument doc = XDocument.Load(Properties.Resources.XmlFile);
		private XElement city;
		private User user;
		public DialogResult success = DialogResult.None;


        public DetailView(string city)
        {
			this.city = doc.Root.Elements("city")
				.Where(t => t.Attribute("name").Value == city
					|| t.Attribute("nameRU").Value == city)
				.First();

			InitializeComponent();
            OnLoad();

        }
        public DetailView(string city, User user)
		{
			this.city = doc.Root.Elements("city")
				.Where(t => t.Attribute("name").Value == city
					|| t.Attribute("nameRU").Value == city)
				.First();

			InitializeComponent();
			OnLoad();

			this.user = user;
			this.Text = user.Name;

			saveBtn.Text = "Сохранить изменения";

			nameBox.Text = user.GetProperty("givenName");
			surnameBox.Text = user.GetProperty("sn");
			middlenameBox.Text = user.GetProperty("middleName");

			try
			{
				surnameEnBox.Text = user.Name.Split(' ')[0];
				nameEnBox.Text = user.Name.Split(' ')[1];
			}
			catch
			{
				Debug.WriteLine("No lastname");
				nameEnBox.Text = user.Name;
				surnameEnBox.Text = string.Empty;
			}
			mobileTextBox.Text = user.GetProperty("mobile");

			try
			{
				birthdayDatePicker.Value = DateTime.Parse(user.GetProperty("extensionAttribute2"));
			}
			catch
			{
				Debug.WriteLine("No birthday");
				birthdayDatePicker.Format = DateTimePickerFormat.Custom;
				birthdayDatePicker.CustomFormat = " ";
			}

			try
			{
				genderSelector.Gender = user.GetProperty("extensionAttribute3");
			}
			catch (Exception e)
			{
				Debug.WriteLine(e.Message);
				Debug.WriteLine("Не задан пол в атрибутах");
			}
			
			departmentCombo.SelectedIndex = departmentCombo.Items.IndexOf(user.GetProperty("department"));
			divCombo.Text = user.GetProperty("description");
			posCombo.Text = user.GetProperty("title");
			roomCombo.Text = user.GetProperty("physicalDeliveryOfficeName");
			telCombo.Text = user.GetProperty("telephoneNumber");

			if (user.GetProperty("manager") == string.Empty)
				managerCheck.Checked = false;
			if ((string)managerCheck.Tag == user.Dn)
				managerCheck.Enabled = false;

			cdCheck.Checked = user.MemberOf(Properties.Groups.DvdDrives);
			usbDiskCheck.Checked = user.MemberOf(Properties.Groups.DvdDrives);
			usbDeviceCheck.Checked = user.MemberOf(Properties.Groups.UsbDevices);

			if (user.MemberOf(Properties.Groups.InternetFull))
				internetCombo.SelectedIndex = 2;
			else if (user.MemberOf(Properties.Groups.InternetLimited))
				internetCombo.SelectedIndex = 1;
			else
				internetCombo.SelectedIndex = 0;

			changed = false;
			saveBtn.Enabled = false;
			
		}

        private void OnLoad()
        {
            unlimitedRadio.Select();
            internetCombo.SelectedIndex = 0;
            expirationDatePicker.Value = DateTime.Today.AddMonths(1);
            mobileTextBox.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;

			string[] depts = city.Elements("dept")
				.Select(t => t.Attribute("nameRU").Value)
				.ToArray();
			if (depts.Count() > 0)
			{
				departmentCombo.Enabled = true;
				departmentLabel.Enabled = true;
				departmentCombo.Items.AddRange(depts);
			}
			else
			{
				departmentCombo.Enabled = false;
				departmentLabel.Enabled = false;
			}
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
				XElement par = city.Elements("dept")
					.Where(t => t.Attribute("nameRU").Value == departmentCombo.Text)
					.FirstOrDefault();

				DirectoryEntry ou = new DirectoryEntry("LDAP://" + par.Attribute("dn").Value);

				user = new User(displayName, ou, par);
				newUser = true;
			}

			user.Properties["givenName"] = nameBox.Text;
			user.Properties["sn"] = surnameBox.Text;
			user.Properties["displayName"] = displayName;
			user.Properties["middleName"] = middlenameBox.Text;
			user.Properties["mobile"] = mobileTextBox.Text;
			user.Properties["l"] = city.Attribute("nameRU").Value;
			user.Properties["st"] = city.Attribute("name").Value;
			user.Properties["c"] = city.Attribute("name").Value == "Minsk" ? "BY" : "KZ";
			user.Properties["department"] = departmentCombo.Text;
			user.Properties["division"] = city.Descendants("dept")
				.Where(t => t.Attribute("nameRU").Value == departmentCombo.Text)
				.Select(t => t.Attribute("name").Value)
				.First();
			user.Properties["description"] = divCombo.Text;
			user.Properties["title"] = posCombo.Text;
			user.Properties["employeeType"] = posEnBox.Text;
			user.Properties["telephoneNumber"] = telCombo.Text;
			user.Properties["ipPhone"] = telCombo.Text;
			user.Properties["physicalDeliveryOfficeName"] = roomCombo.Text;
			user.Properties["extensionAttribute2"] = birthdayDatePicker.Value.ToString("dd.MM.yyyy");
			user.Properties["extensionAttribute3"] = genderSelector.Gender.Substring(0, 1);
			user.Properties["company"] = "АО \"НИПИнефтегаз\"";
			if (limitedRadio.Checked)
				user.Properties["accountExpires"] = expirationDatePicker.Value.AddDays(1)
					.ToFileTime()
					.ToString();
			if (managerCheck.Checked)
				user.Properties["manager"] = (string)managerCheck.Tag;
			else
				user.Properties["manager"] = null;

			user.CommitChanges();

			try
			{
				user.SetMembership(Properties.Groups.DvdDrives, cdCheck.Checked);
				user.SetMembership(Properties.Groups.UsbDrives, usbDiskCheck.Checked);
				user.SetMembership(Properties.Groups.UsbDevices, usbDeviceCheck.Checked);
				user.SetMembership(Properties.Groups.InternetLimited, internetCombo.SelectedIndex == 1);
				user.SetMembership(Properties.Groups.InternetFull, internetCombo.SelectedIndex == 2);
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.Message);
				Debug.WriteLine("Невозможно назначить группы, возможно не хватает прав");
			}

			if(newUser)
			{
				success = MessageBox.Show(
					"Пользователь успешно создан",
					"Пользователь создан",
					MessageBoxButtons.OK,
					MessageBoxIcon.Information);
				
				
			}
			else
			{
				MessageBox.Show(
					"Изменения успешно сохранены",
					"Изменения сохранены",
					MessageBoxButtons.OK,
					MessageBoxIcon.Information);

			}
			this.Close();
		}

        private void LimitedRadio_CheckedChanged(object sender, EventArgs e)
        {
			// Mark that some fields were edited
			Changed();

			expirationDatePicker.Enabled = true;
        }

        private void UnlimitedRadio_CheckedChanged(object sender, EventArgs e)
        {
			// Mark that some fields were edited
			Changed();

			expirationDatePicker.Enabled = false;
        }

        

        private void DepartmentCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
			// Mark that some fields were edited
			Changed();
			
			UpdateCombos(city.Elements("dept")
				.Where(t => t.Attribute("nameRU").Value == departmentCombo.Text)
				.Select(t => t.Elements("user"))
				.First()
				.ToArray());
		}

		private void UpdateCombos(XElement[] users)
		{
			User manager = null;

			HashSet<string> diffDivs = new HashSet<string>();
			HashSet<string> diffPoss = new HashSet<string>();
			HashSet<string> diffRooms = new HashSet<string>();
			HashSet<string> diffTels = new HashSet<string>();


			foreach (XElement elem in users)
			{
				if (elem.Element("description").Value.Trim() != string.Empty
						&& !elem.Element("description").Value.StartsWith("("))
					diffDivs.Add(elem.Element("description").Value);

				if (elem.Element("title").Value.Trim() != string.Empty)
					diffPoss.Add(elem.Element("title").Value);

				if (elem.Element("physicalDeliveryOfficeName").Value.Trim() != string.Empty)
					diffRooms.Add(elem.Element("physicalDeliveryOfficeName").Value);

				if (elem.Element("telephoneNumber").Value.Trim() != string.Empty)
					diffTels.Add(elem.Element("telephoneNumber").Value);

				if (elem.Element("manager").Value.Trim() != string.Empty && manager == null)
					manager = User.Load(elem.Element("manager").Value);

			}

			divCombo.BeginUpdate();
			divCombo.Items.Clear();
			divCombo.ResetText();
			divCombo.Items.AddRange(diffDivs.ToArray());
			divCombo.EndUpdate();

			posCombo.BeginUpdate();
			posCombo.Items.Clear();
			posCombo.ResetText();
			posCombo.Items.AddRange(diffPoss.ToArray());
			posCombo.EndUpdate();

			roomCombo.BeginUpdate();
			roomCombo.Items.Clear();
			roomCombo.ResetText();
			roomCombo.Items.AddRange(diffRooms.ToArray());
			roomCombo.EndUpdate();

			telCombo.BeginUpdate();
			telCombo.Items.Clear();
			telCombo.ResetText();
			telCombo.Items.AddRange(diffTels.ToArray());
			telCombo.EndUpdate();

			if (manager == null)
			{
				managerPanel.Enabled = false;
				managerCheck.Visible = false;
				managerCheck.Checked = false;
			}
			else
			{
				managerPanel.Enabled = true;
				managerCheck.Visible = true;
				managerCheck.Checked = true;

				managerCheck.Text = manager.GetProperty("sn") + " " + manager.GetProperty("givenName");
				managerCheck.Tag = manager.Dn;
			}
		}


        private void NameTextBox_TextChanged(object sender, EventArgs e)
        {
			// Mark that some fields were edited
			Changed();

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
			// Mark that some fields were edited
			Changed();

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
			// Mark that some fields were edited
			Changed();

			if (nameEnBox.Text.Length > 0
					&& (nameEnBox.Text.ToLower().Min() < 'a'
					|| nameEnBox.Text.ToLower().Max() > 'z'))
				nameEnBox.ForeColor = Color.Red;
			else
				nameEnBox.ForeColor = Color.Black;
		}

		private void SurnameTranslitTextBox_TextChanged(object sender, EventArgs e)
		{
			// Mark that some fields were edited
			Changed();

			if (surnameEnBox.Text.Length > 0
					&& (surnameEnBox.Text.ToLower().Min() < 'a'
					|| surnameEnBox.Text.ToLower().Max() > 'z'))
				surnameEnBox.ForeColor = Color.Red;
			else
				surnameEnBox.ForeColor = Color.Black;
		}

        private void RoomCombo_TextChanged(object sender, EventArgs e)
        {
			telCombo.Items.Clear();
			try
			{
				var res = city.Descendants("user")
					.Where(t => t.Element("physicalDeliveryOfficeName").Value == roomCombo.Text)
					.Select(t => t.Element("telephoneNumber").Value);

				HashSet<string> diffTels = new HashSet<string>();

				foreach (string elem in res)
					diffTels.Add(elem);

				telCombo.Items.AddRange(diffTels.ToArray());
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.Message);
				Debug.WriteLine("Наверное не выбран город");
			}
        }

        private void BirthdayDatePicker_ValueChanged(object sender, EventArgs e)
        {
			// Mark that some fields were edited
			Changed();

			birthdayDatePicker.Format = DateTimePickerFormat.Short;
        }

		private void cancelBtn_Click(object sender, EventArgs e)
		{
			if (!changed)
				this.Close();
			else
			{
				DialogResult dialogResult = MessageBox.Show(
					"Все несохраненные изменения будут потеряны",
					"Внимание",
					MessageBoxButtons.YesNo,
					MessageBoxIcon.Warning,
					MessageBoxDefaultButton.Button2);
				if (dialogResult == DialogResult.Yes)
					this.Close();
			}
		}
		

		private void DetailView_KeyUp(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
				saveBtn.PerformClick();
			if (e.KeyCode == Keys.Escape)
				cancelBtn.PerformClick();
		}

		private void Changed()
		{
			if (!changed)
			{
				changed = true;
				saveBtn.Enabled = true;
			}
		}

		private void middlenameBox_TextChanged(object sender, EventArgs e)
		{
			// Mark that some fields were edited
			Changed();

		}

		private void mobileTextBox_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
		{
			// Mark that some fields were edited
			Changed();

		}

		private void genderSelector_Click(object sender, EventArgs e)
		{
			// Mark that some fields were edited
			Changed();

		}

		private void divCombo_SelectedIndexChanged(object sender, EventArgs e)
		{
			// Mark that some fields were edited
			Changed();
		}

		private void posCombo_SelectedIndexChanged(object sender, EventArgs e)
		{
			// Mark that some fields were edited
			Changed();

		}

		private void roomCombo_SelectedIndexChanged(object sender, EventArgs e)
		{
			// Mark that some fields were edited
			Changed();

		}

		private void telCombo_SelectedIndexChanged(object sender, EventArgs e)
		{
			// Mark that some fields were edited
			Changed();

		}

		private void managerCheck_CheckedChanged(object sender, EventArgs e)
		{
			// Mark that some fields were edited
			Changed();

		}

		private void cdCheck_CheckedChanged(object sender, EventArgs e)
		{
			// Mark that some fields were edited
			Changed();

		}

		private void usbDiskCheck_CheckedChanged(object sender, EventArgs e)
		{
			// Mark that some fields were edited
			Changed();

		}

		private void usbDeviceCheck_CheckedChanged(object sender, EventArgs e)
		{
			// Mark that some fields were edited
			Changed();

		}

		private void cloudCheck_CheckedChanged(object sender, EventArgs e)
		{
			// Mark that some fields were edited
			Changed();

		}

		private void internetCombo_SelectedIndexChanged(object sender, EventArgs e)
		{
			// Mark that some fields were edited
			Changed();

		}

		private void expirationDatePicker_ValueChanged(object sender, EventArgs e)
		{
			// Mark that some fields were edited
			Changed();

		}

		private void PosCombo_TextChanged(object sender, EventArgs e)
		{
			// Mark that some fields were edited
			Changed();

			try
			{
				posEnBox.Text = doc.Root.Descendants("user")
					.Where(t => t.Element("title").Value == posCombo.Text
						&& t.Element("employeeType").Value != "")
					.Select(t => t.Element("employeeType").Value)
					.First();
			}
			catch
			{
				posEnBox.Clear();
				Debug.WriteLine("Не удалось найти перевод должности", "Warning");
			}
		}
	}
}
