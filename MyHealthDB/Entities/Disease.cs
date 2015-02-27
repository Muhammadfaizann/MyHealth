using System;
using MyHealth.DB.SQLite;
using MyHealthDB.Model;

namespace MyHealthDB
{

	public class Disease : DBEntityBase
	{

		public Disease ()
		{

		}

		public string Name { get; set; }
		public string Url { get; set; }
		public string Description { get; set; }
		public string SignAndSymptoms { get; set; }
		public string PreventiveMeasures { get; set; }
		public string MisSpelling { get; set; }
		public int DiseaseCategoryID { get; set;}
		public int CPUserId { get; set; }
	}
}

