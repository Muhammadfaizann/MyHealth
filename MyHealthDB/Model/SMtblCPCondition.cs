using System;

namespace MyHealthDB.Model
{
	public class SMtblCPCondition
	{
		public SMtblCPCondition ()
		{
		}

		public int Id { get; set; }
		public string Condition { get; set; }
		public Nullable<System.DateTime> LastUpdatedDate { get; set; }
		public Nullable<int> ApproveStatus { get; set; }
		public Nullable<int> Responsible { get; set; }
		public string Url { get; set; }
		public string Description { get; set; }
		public string SignAndSymptoms { get; set; }
		public string PreventiveMeasures { get; set; }
		public string MisSpelling { get; set; }
		public int CPUserId { get; set; }
	}
}

