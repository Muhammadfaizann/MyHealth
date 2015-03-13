using System;

namespace MyHealthDB
{
	public class ImportantNotice : DBEntityBase
	{
		public string Name { get; set; }
		public Nullable<System.DateTime> StartDate { get; set; }
		public Nullable<System.DateTime> EndDate { get; set; }
		public Nullable<System.DateTime> LastUpdatedDate { get; set; }
		public Nullable<bool> isArchived { get; set; }

		public string NoticeColor { get; set; }

		public ImportantNotice ()
		{
		}
	}
}

