using System;
using MyHealth.DB.SQLite;

namespace MyHealthDB
{
	public class Organisation : DBEntityBase
	{
		public Organisation()
		{
		}

		[PrimaryKey]
		public int ID { get; set; }
		public String Name { get; set; }
		public String PhoneNumber { get; set; }
		public String URL { get; set;}
	}
}

