using System;

namespace MyHealthDB
{
	public class SMLogUsage
	{
		public SMLogUsage ()
		{
		}

		public int Id { get; set; }
		public Nullable<int> ApplicationUsersAppId { get; set; }
		public Nullable<System.DateTime> Date { get; set; }
		public Nullable<int> AppId { get; set; }
		public int Page { get; set; }
	}
}

