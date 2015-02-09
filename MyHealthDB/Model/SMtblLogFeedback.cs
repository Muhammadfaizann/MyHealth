using System;

namespace MyHealthDB
{
	public class SMtblLogFeedback
	{
		public SMtblLogFeedback ()
		{
		}

		public int Id { get; set; }

		public Nullable<System.DateTime> Date { get; set; }
		public string FeedbackText { get; set; }
		public Nullable<bool> IsArchived { get; set; }
	}
}

