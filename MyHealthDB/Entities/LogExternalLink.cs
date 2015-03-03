using System;

namespace MyHealthDB
{
	public class LogExternalLink : DBEntityBase
	{
		public LogExternalLink ()
		{
		}

		public Nullable<System.DateTime> Date { get; set; }
		public Nullable<int> AppId { get; set; }
		public string Link { get; set; }
	}
}

