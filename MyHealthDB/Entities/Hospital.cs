using System;

namespace MyHealthDB
{
	public class Hospital : DBEntityBase
	{
		public Hospital()
		{
		}

		//[PrimaryKey]
		//public int ID { get; set; }
		public int? CountyID{get;set;}
		public String Name { get; set; }
		public String PhoneNumber { get; set; }
		public String URL { get; set;}
		public bool? isArchived { get; set; }
	}
}

