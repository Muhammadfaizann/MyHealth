using System;
using MyHealth.DB.SQLite;

namespace MyHealthDB
{
	public class HealthSearch : DBEntityBase
	{
		public HealthSearch ()
		{
		}

		[PrimaryKey]
		public int ID { get; set;}
		public String Name { get; set;}
		public String Details { get; set;}
	}
}

