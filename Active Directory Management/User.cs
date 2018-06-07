using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Diagnostics;

namespace Active_Directory_Management
{
    public class User
    {
		private XElement xmlNode;
        private DirectoryEntry entry;
		private XDocument xmlFile = XDocument.Load(Active_Directory_Management.Properties.Resources.XmlFile);

		private string xmlFileLocation = Active_Directory_Management.Properties.Resources.XmlFile;
		
		private string username = "bazhr1";
		private string password = "vk.com123";

		public Dictionary<string, string> Properties { get; set; } = new Dictionary<string, string>();
		private string memberOf = string.Empty;
		private int uac;
        public string Dn { get; }
		public string Name { get; }

		
		

		public User(string name, DirectoryEntry path, XElement parent)
		{
			// Get first name and family name from name string
			string surname = name.Split(' ')[0].ToLower();
			string firstName = name.Split(' ')[1].ToLower();

			// Create entry in Active Directory
			entry = path.Children.Add("CN=" + name, "user");
			

			// Build Searcher 
			DirectorySearcher searcher = new DirectorySearcher()
			{
				SearchRoot = new DirectoryEntry("LDAP://DC=nng,DC=kz"),
				SearchScope = SearchScope.Subtree
			};

			// Supposed samAccountName (login)
			string samAccountName = surname.Substring(0, Math.Min(4, surname.Length))
				+ firstName[0];
			int cnt = 0;
			
			// Iterate cnt while available login is found 
			do
			{
				cnt++;
				searcher.Filter = String.Format("(samAccountName={0})",
					samAccountName + cnt.ToString());
			}
			while (searcher.FindOne() != null);

			// Set final samAccountName
			samAccountName += cnt.ToString();
			entry.Properties["samAccountName"].Value = samAccountName;
			entry.Properties["userPrincipalName"].Value = samAccountName + "@nng.kz";

			// Commit changes to Active Directory (create user)
			entry.CommitChanges();

			// Set standart password and require to change it on next logon
			entry.Invoke("SetPassword", new object[] { "1234567Aa" });
			entry.Properties["pwdLastSet"].Value = 0;
			entry.CommitChanges();

			// Manually enable account
			entry.Properties["userAccountControl"].Value = 0x200;
			entry.CommitChanges();
			

			// Write local parameters
			Dn = entry.Properties["distinguishedName"].Value.ToString();
			uac = 0x200;
			memberOf = string.Empty;

			// Create new xml node 
			xmlNode = new XElement("user",
				new XAttribute("name", name),
				new XAttribute("dn", Dn),
				new XAttribute("uac", uac),
				new XAttribute("memberOf", memberOf));

			// Append node to parent in document
			parent.Add(xmlNode);

			// Save changes
			xmlFile.Save(Active_Directory_Management.Properties.Resources.XmlFile);

			
		}
		
		public User (XElement userXmlNode)
		{
			xmlNode = userXmlNode;
			Dn = xmlNode.Attribute("dn").Value;

			entry = new DirectoryEntry("LDAP://" + Dn);

			UpdateProperties();
		}

		public User(DirectoryEntry userEntry)
        {
            entry = userEntry;
			Dn = entry.Properties["distinguishedName"].Value.ToString();
			xmlNode = xmlFile.Root.Descendants("user")
                .Where(t => t.Attribute("dn").Value == Dn)
                .FirstOrDefault();

            

            UpdateProperties();
        }

        public User(string username)
        {
            string lastname, firstname;
            try
            {
                lastname = username.Split(' ')[0];
                firstname = username.Split(' ')[1];
            }
            catch
            {
                lastname = string.Empty;
                firstname = username;
            }

			xmlNode = xmlFile.Root.Descendants("user")
				.Where(t => t.Element("sn").Value == lastname
					&& t.Element("givenName").Value == firstname)
				.First();

			Dn = xmlNode.Attribute("dn").Value;

			entry = new DirectoryEntry("LDAP://" + Dn);

			UpdateProperties();
		}

		public string Username
		{
			get
			{
				return username;
			}
			set
			{
				username = value;
			}
		}
		public string Password
		{
			set
			{
				password = value;
			}
		}
		private string XmlFileLocation
		{
			get
			{
				return xmlFileLocation;
			}
			set
			{
				xmlFileLocation = value;
				xmlFile = XDocument.Load(XmlFileLocation);
			}
		}


		private void UpdateProperties()
        {
            foreach (XElement elem in xmlNode.Elements())
            {
				if (elem.Name == "memberOf")
					memberOf = elem.Value;
				else if (elem.Name == "userAccountControl")
					uac = int.Parse(elem.Value);
				else
					Properties.Add(elem.Name.ToString(), elem.Value.ToString());
            }
        }
		/// <summary>
		/// Запрашивает или устанавливает значение блокировки аккаунта
		/// </summary>
        public bool Enabled
        {
            get
            {
                return !Convert.ToBoolean(uac & 0x2);
            }
            set
            {
				int newUserAccountControl;

				if (value == true)
					// Enable, remove flag
					newUserAccountControl = uac & ~0x2;
                else
					// Disable, set flag
                    newUserAccountControl = uac | 0x2;

				// Set new parameters
				uac = newUserAccountControl;
				entry.Properties["userAccountControl"].Value = uac;
				xmlNode.Element("userAccountControl").Value = uac.ToString();

				// Save changes
				xmlFile.Save(xmlFileLocation);
				entry.CommitChanges();
            }
        }

		/// <summary>
		/// Возвращает информацию состоит ли пользователь в группе
		/// </summary>
		/// <param name="groupDN">Distinguished Name группы </param>
		/// <returns>Членство в группе</returns>
		public bool MemberOf(string groupDN)
        {
            return memberOf.Contains(groupDN);
        }


        /// <summary>
		/// Добавление пользователя в группу
		/// </summary>
		/// <param name="groupDN">Distinguished Name группы куда добаляется пользователь</param>
        public void AddGroup(string groupDN)
        {
            if (!MemberOf(groupDN))
            {
                DirectoryEntry groupEntry = new DirectoryEntry("LDAP://" + groupDN);

                // Update property in object
                memberOf += groupDN;

                // Update property in XML file
                xmlNode.Element("memberOf").Value += groupDN;
				xmlFile.Save(xmlFileLocation);

				// Update Active Directory
				groupEntry.Properties["member"].Add(Dn);
				groupEntry.CommitChanges();

                groupEntry.Close();
            }
        }

		/// <summary>
		/// Удаление пользователя из группы
		/// </summary>
		/// <param name="groupDN">Distinguished Name группы откуда удаляется пользователь</param>
		public void RemoveGroup(string groupDN)
        {
            if (MemberOf(groupDN))
            {
                DirectoryEntry groupEntry = new DirectoryEntry("LDAP://" + groupDN);

                string newMemberOf = memberOf.Remove(
                    memberOf.IndexOf(groupDN),
                    groupDN.Length);

                // Update property in object
                memberOf = newMemberOf;

                // Update property in XML file
                xmlNode.Element("memberOf").Value = newMemberOf;
				xmlFile.Save(xmlFileLocation);

                // Update Active Directory
				groupEntry.Properties["member"].Remove(Dn);
				groupEntry.CommitChanges();

				groupEntry.Close();
            }
        }

		/// <summary>
		/// Устанавливает членство пользователя в группе
		/// </summary>
		/// <param name="groupDistinguishedName"></param>
		/// <param name="state"></param>
        public void SetMembership(string groupDistinguishedName, bool state)
        {
			if (state == true)
				AddGroup(groupDistinguishedName);
            else
                RemoveGroup(groupDistinguishedName);
        }

		/// <summary>
		/// Сохраняет все локальные изменения в Active Directory и XML файл
		/// </summary>
        public void CommitChanges()
        {
			foreach (KeyValuePair<string, string> prop in Properties)
            {
				try
				{
					xmlNode.Element(prop.Key).Value = prop.Value;
				}
				catch
				{
					xmlNode.Add(new XElement(prop.Key, prop.Value));
				}
                entry.Properties[prop.Key].Value = prop.Value;
            }
			xmlFile.Save(XmlFileLocation);
			entry.CommitChanges();
		}


		public void ShowProperties()
		{
			foreach(KeyValuePair<string, string> pair in Properties)
			{
				System.Diagnostics.Debug.WriteLine(pair);
			}
		}

		public void DropPassword()
		{
			entry.Invoke("SetPassword", new object[] { "12345678Ab" });
			entry.Properties["pwdLastSet"].Value = 0;
			entry.CommitChanges();
		}
    }
}
