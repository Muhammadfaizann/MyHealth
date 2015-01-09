using System;
using MyHealth.DB.SQLite;

namespace MyHealthDB
{
	public class County : DBEntityBase
	{
		public County()
		{
		}

		//[PrimaryKey]
		//public int ID { get; set; }
		public int CountyID{get;set;}
		public String Name { get; set; }
		public double Longitude { get; set; }
		public double Lattitude { get; set;}
	}
}

