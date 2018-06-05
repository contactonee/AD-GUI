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
    class User
    {
		private XElement xmlNode;
        private DirectoryEntry entry;
		private XDocument xmlFile = XDocument.Load("users.xml");

		private string xmlFileLocation = "users.xml";

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


		private string username = "bazhr1";
		private string password = "vk.com123";
		
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
		public string Password {
			set
			{
				password = value;
			}
		}

		public Dictionary<string, string> Properties { get; set; } = new Dictionary<string, string>();
		private string memberOf = string.Empty;
		private int uac;
        public string Dn { get; }


        
        public User(XElement userXmlNode)
        {
            xmlNode = userXmlNode;
			Dn = xmlNode.Attribute("dn").Value;

			entry = new DirectoryEntry("LDAP://" + Dn);

            UpdateProperties();
        }

        public User(DirectoryEntry userEntry)
        {
            entry = userEntry;
            xmlNode = xmlFile.Root.Descendants("user")
                .Where(t => t.Attribute("dn").Value == Dn)
                .FirstOrDefault();

            Dn = entry.Properties["distinguishedName"].Value.ToString();

            UpdateProperties();
        }

        public User(XElement userXmlNode, DirectoryEntry userEntry)
        {
            entry = userEntry;
            xmlNode = userXmlNode;

            Dn = entry.Properties["distinguishedName"].Value.ToString();

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

				uac = newUserAccountControl;
				entry.Properties["userAccountControl"].Value = uac;
				xmlNode.Element("userAccountControl").Value = uac.ToString();


				xmlFile.Save(xmlFileLocation);
				entry.CommitChanges();
            }
        }
		

		public bool MemberOf(string groupDistinguishedName)
        {
            return memberOf.Contains(groupDistinguishedName);
        }
        
        public void AddGroup(string groupDistinguishedName)
        {
            if (!MemberOf(groupDistinguishedName))
            {
                DirectoryEntry groupEntry = new DirectoryEntry("LDAP://" + groupDistinguishedName);

                // Update property in object
                memberOf += groupDistinguishedName;

                // Update property in XML file
                xmlNode.Element("memberOf").Value += groupDistinguishedName;
				xmlFile.Save(xmlFileLocation);

				// Update Active Directory
				groupEntry.Properties["member"].Add(Dn);
				groupEntry.CommitChanges();

                groupEntry.Close();
            }
        }

        public void RemoveGroup(string groupDistinguishedName)
        {
            if (MemberOf(groupDistinguishedName))
            {
                DirectoryEntry groupEntry = new DirectoryEntry("LDAP://" + groupDistinguishedName);

                string newMemberOf = memberOf.Remove(
                    memberOf.IndexOf(groupDistinguishedName),
                    groupDistinguishedName.Length);

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

        public void SetMembership(string groupDistinguishedName, bool state)
        {
			if (state == true)
				AddGroup(groupDistinguishedName);
            else
                RemoveGroup(groupDistinguishedName);
        }


        public void CommitChanges()
        {
			foreach (KeyValuePair<string, string> prop in Properties)
            {
                xmlNode.Element(prop.Key).Value = prop.Value;
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
    }
}
