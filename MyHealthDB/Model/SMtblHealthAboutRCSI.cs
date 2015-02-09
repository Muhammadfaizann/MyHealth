using System;

namespace MyHealthDB.Model
{
	public class SMtblHealthAboutRCSI
	{
		public SMtblHealthAboutRCSI ()
		{
		}

		public int Id { get; set; }
		public Nullable<System.DateTime> LastUpdatedDate { get; set; }
		public Nullable<bool> isArchived { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public byte[] mainImage { get; set; }
	}
}

