using System;

namespace MyHealthDB.Model
{
	public class SMtblHealthOrganizationsInfo
	{
		public SMtblHealthOrganizationsInfo ()
		{
		}

		public int Id { get; set; }
		public string Name { get; set; }
		public string Number { get; set; }
		public string Website { get; set; }
		public Nullable<System.DateTime> LastUpdatedDate { get; set; }
		public Nullable<bool> isArchived { get; set; }
	}
}

