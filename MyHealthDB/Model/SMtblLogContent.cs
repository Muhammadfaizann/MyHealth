using System;

namespace MyHealthDB
{
	public class SMtblLogContent
	{
		public SMtblLogContent ()
		{
		}

		public int Id { get; set; }
		public Nullable<int> ApplicationUsersAppId { get; set; }
		public Nullable<System.DateTime> Date { get; set; }
		public Nullable<int> AppId { get; set; }
		public Nullable<int> ConditionId { get; set; }
		public Nullable<int> CategoryId { get; set; }
	}
}

