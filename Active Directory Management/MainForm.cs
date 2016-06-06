using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.DirectoryServices;

namespace Active_Directory_Management
{
    
    public partial class MainForm : Form
    {
        private DirectoryEntry ldapConnection = new DirectoryEntry("LDAP://OU=TestOU,OU=Users,OU=Aktau,DC=nng,DC=kz");
        public MainForm()
        {
            InitializeComponent();
            ldapConnection.AuthenticationType = AuthenticationTypes.Secure;
            unlimitedRadio.Select();
            cityCombo.SelectedIndex = 0;
        }


        private bool checkChar(string input)
        {
            foreach(char ch in input)
                if((ch < 'А' || ch > 'я') && ch != 'ё' && ch != 'Ё')
                    return false;
            return true;
        }
        private void makeTranslit(TextBox sender, TextBox translit)
        {
            string input = sender.Text;
            if (!checkChar(input))
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
            dict['Ю'] = "Yu"; // Yuriev
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
        private void closeApplication(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void createUser(object sender, EventArgs e)
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
            newUser.Properties["title"].Value = positionTextBox.Text;
            newUser.Properties["telephoneNumber"].Value = internalCombo.Text;
            newUser.Properties["physicalDeliveryOfficeName"].Value = roomCombo.Text;
            newUser.CommitChanges();

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
            // divCombo List Update
            // if divisions exist, enable divLabel and divCombo
            divLabel.Enabled = true;
            divCombo.Enabled = true;

            //otherwise disable
        }

        private void nameTextBox_TextChanged(object sender, EventArgs e)
        {
            makeTranslit(nameTextBox, nameTranslitTextBox);
        }

        private void surnameTextBox_TextChanged(object sender, EventArgs e)
        {
            makeTranslit(surnameTextBox, surnameTranslitTextBox);
        }
    }
}
