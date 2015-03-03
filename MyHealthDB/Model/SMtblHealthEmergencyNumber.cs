using System;

namespace MyHealthDB.Model
{
	public class SMtblHealthEmergencyNumber
	{
		public SMtblHealthEmergencyNumber ()
		{
		}

		public int Id { get; set; }
		public string Name { get; set; }
		public int Number { get; set; }
		public string Website { get; set; }
		public string Description { get; set; }
		public Nullable<System.DateTime> LastUpdatedDate { get; set; }
		public Nullable<bool> isArchived { get; set; }
	}
}

