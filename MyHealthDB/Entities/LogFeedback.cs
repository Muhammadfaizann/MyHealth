using System;

namespace MyHealthDB
{
	public class LogFeedback : DBEntityBase
	{
		public LogFeedback ()
		{
		}

		public Nullable<System.DateTime> Date { get; set; }
		public string FeedbackText { get; set; }
		public Nullable<bool> IsArchived { get; set; }
	}
}

