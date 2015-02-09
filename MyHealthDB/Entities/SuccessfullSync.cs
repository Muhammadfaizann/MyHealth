using System;
using MyHealth.DB.SQLite;

namespace MyHealthDB
{
	public class SuccessfullSync: DBEntityBase
	{
		public SuccessfullSync()
		{
		}

		//[PrimaryKey]
		//public int ID { get; set; }
		public String Date { get; set; }
		public String NumberOfUpdateEntries { get; set;}
	}
}

