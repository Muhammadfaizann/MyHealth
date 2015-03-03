using System;

namespace MyHealthDB
{
	public class LogUsage : DBEntityBase
	{
		public LogUsage ()
		{
		}

		public Nullable<System.DateTime> Date { get; set; }
		public Nullable<int> AppId { get; set; }
		public int Page { get; set; }
	
	}
}

