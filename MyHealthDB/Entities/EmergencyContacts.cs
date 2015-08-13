using System;
using MyHealth.DB.SQLite;
using MyHealthDB.Model;

namespace MyHealthDB
{
	public class EmergencyContacts : DBEntityBase
	{
		public EmergencyContacts()
		{
		}


		//[PrimaryKey]
		//public int ID { get; set; }
		public String Name { get; set; }
		public String Description { get; set; }
		public String PhoneNumber { get; set;}
		public bool? isArchived { get; set;}
	}
}

