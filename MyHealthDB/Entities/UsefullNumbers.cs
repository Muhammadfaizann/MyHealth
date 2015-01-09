using System;
using MyHealth.DB.SQLite;

namespace MyHealthDB
{
	public class UsefullNumbers: DBEntityBase
	{
		public UsefullNumbers()
		{
		}

		//[PrimaryKey]
		//public int ID { get; set; }
		public String Name { get; set; }
		public String Number{ get; set;}
	}
}

