using System;
using MyHealth.DB.SQLite;

namespace MyHealthDB
{

	public class Disease : DBEntityBase
	{

		public Disease ()
		{

		}



		[PrimaryKey]
		public int ID { get; set;}
		public int DiseaseCategoryID{get;set;}
		public String Name { get; set;}
		public String Details { get; set;}
	}
}

