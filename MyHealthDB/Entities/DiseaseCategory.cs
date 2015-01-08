using System;
using MyHealth.DB.SQLite;

namespace MyHealthDB
{
	public class DiseaseCategory : DBEntityBase
	{
		public DiseaseCategory ()
		{
		}

		[PrimaryKey]
		public int ID { get; set;}
		public String CategoryName { get; set;}
		public String CategoryDetails { get; set;}
	}
}

