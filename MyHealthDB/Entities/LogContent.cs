using System;

namespace MyHealthDB
{
	public class LogContent : DBEntityBase
	{
		public LogContent ()
		{
		}

		public Nullable<System.DateTime> Date { get; set; }
		public Nullable<int> AppId { get; set; }
		public Nullable<int> ConditionId { get; set; }
		public Nullable<int> CategoryId { get; set; }
	}
}

