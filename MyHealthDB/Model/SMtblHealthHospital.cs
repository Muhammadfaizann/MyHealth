using System;

namespace MyHealthDB.Model
{
	public class SMtblHealthHospital
	{
		public SMtblHealthHospital ()
		{

		}
		public int Id { get; set; }
		public string Name { get; set; }
		public int Number { get; set; }
		public string Website { get; set; }
		public Nullable<System.DateTime> LastUpdatedDate { get; set; }
		public Nullable<bool> isArchived { get; set; }
		public Nullable<int> countyId { get; set; }
	}
}

