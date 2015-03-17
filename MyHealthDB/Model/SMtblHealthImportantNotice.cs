using System;

namespace MyHealthDB.Model
{
	public class SMtblHealthImportantNotice
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public Nullable<System.DateTime> StartDate { get; set; }
		public Nullable<System.DateTime> EndDate { get; set; }
		public Nullable<System.DateTime> LastUpdatedDate { get; set; }
		public Nullable<bool> isArchived { get; set; }

		public string NoticeColor { get; set; }

		public SMtblHealthImportantNotice ()
		{
		}
	}
}

