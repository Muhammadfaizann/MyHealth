using System;

namespace MyHealthDB
{
	public class SMtblLogExternalLink
	{
		public SMtblLogExternalLink ()
		{
		}

		public int Id { get; set; }
		public Nullable<int> ApplicationUsersAppId { get; set; }
		public Nullable<System.DateTime> Date { get; set; }
		public Nullable<int> AppId { get; set; }
		public string Link { get; set; }
	}
}

