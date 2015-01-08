using System;
using MyHealth.DB.SQLite;

namespace MyHealthDB
{
	public class HelpData : DBEntityBase
	{
		public HelpData()
		{
		}

		[PrimaryKey]
		public int ID { get; set; }
		public String Name { get; set; }
	}
}

