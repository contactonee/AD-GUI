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
		
		private XElement city;
		private User user;
		public DialogResult success = DialogResult.None;

		static string[] groups = System.IO.File.ReadAllLines("groups.csv");


        public DetailView(XElement city)
        {
			this.city = city;

			InitializeComponent();
            OnLoad();

			saveBtn.Enabled = false;
		}
        public DetailView(XElement city, User user)
		{
			this.city = city;

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
				birthdayPicker.Value = DateTime.Parse(user.GetProperty("extensionAttribute2"));
			}
			catch
			{
				Debug.WriteLine("No birthday");
				birthdayPicker.Clear();
			}

			try
			{
				genderSelector.Value = user.GetProperty("extensionAttribute3");
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
			
			foreach(Guid group in groupSelector.Groups.Values)
			{
				DirectoryEntry entry = new DirectoryEntry(string.Format("LDAP://<GUID={0}>", group.ToString()));
				groupSelector.SetValue(group, user.MemberOf(entry.Properties["distinguishedName"].Value.ToString()));
			}

			saveBtn.Enabled = false;
		}

		private void Field_Changed(object sender, EventArgs e)
		{
			saveBtn.Enabled = true;
		}

		private void OnLoad()
        {
			nameBox.TextChanged += Field_Changed;
			surnameBox.TextChanged += Field_Changed;
			middlenameBox.TextChanged += Field_Changed;
			nameEnBox.TextChanged += Field_Changed;
			surnameEnBox.TextChanged += Field_Changed;
			birthdayPicker.Enter += Field_Changed;
			genderSelector.Enter += Field_Changed;
			departmentCombo.TextChanged += Field_Changed;
			divCombo.TextChanged += Field_Changed;
			posCombo.TextChanged += Field_Changed;
			posEnBox.TextChanged += Field_Changed;
			roomCombo.TextChanged += Field_Changed;
			telCombo.TextChanged += Field_Changed;
			managerCheck.Enter += Field_Changed;
			groupSelector.Enter += Field_Changed;
			groupBox3.Enter += Field_Changed;

			
			groupSelector.File = groups;
			groupSelector.RenderList(city.Attribute("nameRU").Value, Guid.Empty);

			unlimitedRadio.Select();
            expirationDatePicker.Value = DateTime.Today.AddMonths(1);
            mobileTextBox.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
            if (city.Attribute("name").Value == "Minsk")
                mobileTextBox.Mask = "+375 (00) 0000000";

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


				User manager = null;

				HashSet<string> diffDivs = new HashSet<string>();
				HashSet<string> diffPoss = new HashSet<string>();
				HashSet<string> diffRooms = new HashSet<string>();
				HashSet<string> diffTels = new HashSet<string>();


				foreach (XElement elem in city.Elements().ToArray())
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

				posEnBox.Clear();

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
				if(dict.ContainsKey(ch))
					result += dict[ch];

			return result;
        }



		private bool ValidateForm()
		{
			bool error = false;

			error |= (nameBox.Text == "");
			error |= (surnameBox.Text == "");
			error |= (nameEnBox.Text == "");
			error |= (surnameEnBox.Text == "");
			error |= (middlenameBox.Text == "");
			error |= (birthdayPicker.Value == DateTime.MinValue);

			if (departmentCombo.Enabled)
				error |= (departmentCombo.Text == "");

			error |= (roomCombo.Text == "");
			error |= (telCombo.Text == "");

			return !error;
		}

        private void CreateButton(object sender, EventArgs e)
        {
			if(ValidateForm())
				CreateUser();
			else
			{
				MessageBox.Show("Заполните необходимые поля!",
					"",
					MessageBoxButtons.OK,
					MessageBoxIcon.Warning);
			}
        }

        private void CreateUser()
        {
			this.Cursor = Cursors.AppStarting;
			string displayName = surnameEnBox.Text + " " + nameEnBox.Text;
			bool newUser = false;

			if (user == null)
			{
                XElement par;

                if (departmentCombo.Text == "")
                    par = city;

                else
				    par = city.Elements("dept")
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

			if (departmentCombo.Text == "")
			{
				user.Properties["department"] = departmentCombo.Text;
				user.Properties["division"] = city.Descendants("dept")
						.Where(t => t.Attribute("nameRU").Value == departmentCombo.Text)
						.Select(t => t.Attribute("name").Value)
						.First();
			}

			user.Properties["description"] = divCombo.Text;
			user.Properties["title"] = posCombo.Text;
			user.Properties["employeeType"] = posEnBox.Text;
			user.Properties["telephoneNumber"] = telCombo.Text;
			user.Properties["ipPhone"] = telCombo.Text;
			user.Properties["physicalDeliveryOfficeName"] = roomCombo.Text;
			user.Properties["extensionAttribute2"] = birthdayPicker.Value.ToString("dd.MM.yyyy");
			user.Properties["extensionAttribute3"] = genderSelector.Value.Substring(0, 1);

			user.Properties["company"] = "АО \"НИПИнефтегаз\"";
			user.Properties["l"] = city.Attribute("nameRU").Value;
			user.Properties["st"] = city.Attribute("name").Value;
			user.Properties["c"] = city.Attribute("country").Value;
			user.Properties["postalCode"] = city.Attribute("postalCode").Value;

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
				foreach (KeyValuePair<Guid, bool> pair in groupSelector.SelectedGroups())
				{
					Debug.WriteLine(pair.Key);
					if (pair.Value)
					{
						DirectoryEntry entry = new DirectoryEntry(string.Format("LDAP://<GUID={0}>", pair.Key));
						user.AddGroup(entry.Properties["distinguishedName"].Value.ToString());

					}
				}
			}
			catch
			{
				Debug.WriteLine("Не может добавить в группы, возможно не хватает прав");
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
			this.Cursor = Cursors.Default;
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

			XElement deptElem = city.Elements("dept")
				.Where(t => t.Attribute("nameRU").Value == departmentCombo.Text)
				.First();
			

			groupSelector.RenderList(city.Attribute("nameRU").Value, new Guid(deptElem.Attribute("guid").Value));

			User manager = null;

			HashSet<string> diffDivs = new HashSet<string>();
			HashSet<string> diffPoss = new HashSet<string>();
			HashSet<string> diffRooms = new HashSet<string>();
			HashSet<string> diffTels = new HashSet<string>();


			foreach (XElement elem in deptElem.Elements("user").ToArray())
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

			posEnBox.Clear();

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

			TextBox textBox = (TextBox)sender;

			if (textBox.Name == nameEnBox.Name
				|| textBox.Name == surnameEnBox.Name)
			{
				if (CheckLatin(textBox.Text))
					textBox.ForeColor = Color.Black;
				else
					textBox.ForeColor = Color.Red;
			}


			else
			{
				if (CheckCyrillic(textBox.Text))
				{
					textBox.ForeColor = Color.Black;
					if (textBox.Name == nameBox.Name)
						nameEnBox.Text = Translit(textBox.Text);
					if (textBox.Name == surnameBox.Name)
						surnameEnBox.Text = Translit(textBox.Text);
				}
				else
					textBox.ForeColor = Color.Red;
			}

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


		private void cancelBtn_Click(object sender, EventArgs e)
		{
			if (saveBtn.Enabled)
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
			else
				this.Close();
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

		private void genderSelector_Click(object sender, EventArgs e)
		{
			// Mark that some fields were edited
			Changed();

		}

		private void divCombo_SelectedIndexChanged(object sender, EventArgs e)
		{
			// Mark that some fields were edited
			Changed();

			User manager = null;
			
			HashSet<string> diffPoss = new HashSet<string>();
			HashSet<string> diffRooms = new HashSet<string>();
			HashSet<string> diffTels = new HashSet<string>();


			foreach (XElement elem in city.Descendants("user")
				.Where(t => t.Element("description").Value == divCombo.Text)
				.ToArray())
			{
				if (elem.Element("title").Value.Trim() != string.Empty)
					diffPoss.Add(elem.Element("title").Value);

				if (elem.Element("physicalDeliveryOfficeName").Value.Trim() != string.Empty)
					diffRooms.Add(elem.Element("physicalDeliveryOfficeName").Value);

				if (elem.Element("telephoneNumber").Value.Trim() != string.Empty)
					diffTels.Add(elem.Element("telephoneNumber").Value);

				if (elem.Element("manager").Value.Trim() != string.Empty && manager == null)
					manager = User.Load(elem.Element("manager").Value);

			}
			posCombo.BeginUpdate();
			posCombo.Items.Clear();
			posCombo.ResetText();
			posCombo.Items.AddRange(diffPoss.ToArray());
			posCombo.EndUpdate();

			posEnBox.Clear();

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

		private void CloudCheck_CheckedChanged(object sender, EventArgs e)
		{
			// Mark that some fields were edited
			Changed();

		}

		private void InternetCombo_SelectedIndexChanged(object sender, EventArgs e)
		{
			// Mark that some fields were edited
			Changed();

		}

		private void ExpirationDatePicker_ValueChanged(object sender, EventArgs e)
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
				posEnBox.Text = city.Descendants("user")
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

		private void GroupSelector_Click(object sender, EventArgs e)
		{
			changed = true;
		}

		private bool CheckCyrillic(string text)
		{
			text = text.ToLower();
			if (text == string.Empty)
				return true;
			if (text.Min() < 'а' || (text.Max() > 'я' && text.Max() != 'ё'))
				return false;
			else
				return true;
		}
		private bool CheckLatin(string text)
		{
			text = text.ToLower();
			if (text == string.Empty)
				return true;
			if (text.Min() < 'a' || text.Max() > 'z')
				return false;
			else
				return true;
		}

		private void LatinTextBoxes_Validating(object sender, CancelEventArgs e)
		{
			if (!CheckLatin(((TextBox)sender).Text))
				e.Cancel = true;
		}
		private void CyrillicTextBoxes_Validating(object sender, CancelEventArgs e)
		{
			if (!CheckCyrillic(((TextBox)sender).Text))
				e.Cancel = true;
		}

		private void birthdayPicker_Validating(object sender, CancelEventArgs e)
		{
			if (((BirthdayPicker)sender).Value == DateTime.MinValue)
			{
				((BirthdayPicker)sender).ForeColor = Color.Red;
				e.Cancel = true;
			}
		}

		private void Field_Leave(object sender, EventArgs e)
		{
			if (sender is TextBox)
				((TextBox)sender).Text = ((TextBox)sender).Text.Trim();
			if (sender is ComboBox)
				((ComboBox)sender).Text = ((ComboBox)sender).Text.Trim();
			if(sender is MaskedTextBox)
				((MaskedTextBox)sender).Text = ((MaskedTextBox)sender).Text.Trim();
		}

		private void mobileTextBox_Validating(object sender, CancelEventArgs e)
		{
			// +7(___) ___ - ____
			((MaskedTextBox)sender).TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
			int cnt = ((MaskedTextBox)sender).Text.Length;
			((MaskedTextBox)sender).TextMaskFormat = MaskFormat.IncludePromptAndLiterals;

			Debug.WriteLine(((MaskedTextBox)sender).Text);
			if (((MaskedTextBox)sender).Text.Contains('_') && cnt > 0)
			{
				e.Cancel = true;
				((MaskedTextBox)sender).ForeColor = Color.Red;
			}
		}

		private void mobileTextBox_TextChanged(object sender, EventArgs e)
		{
			((MaskedTextBox)sender).ForeColor = Color.Black;
		}

		private void birthdayPicker_Enter(object sender, EventArgs e)
		{
			((BirthdayPicker)sender).ForeColor = Color.Black;
		}
    }
}
