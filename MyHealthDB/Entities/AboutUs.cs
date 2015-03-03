using System;

namespace MyHealthDB
{
	public class AboutUs : DBEntityBase
	{
		public AboutUs ()
		{
		}

		public Nullable<DateTime> LastUpdatedDate { get; set; }
		public Nullable<bool> isArchived { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public byte[] mainImage { get; set; }
	}
}

