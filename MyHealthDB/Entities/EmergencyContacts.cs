using System;
using MyHealth.DB.SQLite;

namespace MyHealthDB
{
	public class EmergencyContacts : DBEntityBase
	{
		public EmergencyContacts()
		{
		}

		[PrimaryKey]
		public int ID { get; set; }
		public String Name { get; set; }
		public String Description { get; set; }
		public String PhoneNumber { get; set;}
	}
}

