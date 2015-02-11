using System;

namespace MyHealthDB.Model
{
	public class SMApplicationUsersApp
	{
		public SMApplicationUsersApp ()
		{
		}

		public int ID { get; set; }
		public int ApplicationId { get; set; }
		public string UserName { get; set; }
		public string APPLICATION_NAME { get; set; }
		public short STATUS { get; set; }
		public Nullable<int> TIMES_USED_THIS_MONTH { get; set; }
		public Nullable<System.DateTime> DATE_LAST_LOGGED_INTO { get; set; }
		public Nullable<int> PIN { get; set; }
		public Nullable<decimal> APP_DISCONNECTED_BY { get; set; }
		public Nullable<System.DateTime> APP_DISCONNECTED_DATE { get; set; }
		public Nullable<decimal> APP_TRASHED_BY { get; set; }
		public Nullable<System.DateTime> APP_TRASHED_DATE { get; set; }
		public Nullable<System.DateTime> PIN_RESET_DATE { get; set; }
		public Nullable<decimal> APP_RECONNECTED_BY { get; set; }
		public Nullable<System.DateTime> APP_RECONNECTED_DATE { get; set; }
		public string DEVICE_ID { get; set; }
		public Nullable<int> PIN_RESET_BY { get; set; }
		public Nullable<System.DateTime> INITIAL_LOGGED_IN_DATE { get; set; }
		public string PASSWORD { get; set; }
		public Nullable<System.DateTime> PASSWORD_RESET_DATE { get; set; }
		public Nullable<int> PASSWORD_RESET_BY { get; set; }
		public string Type { get; set; }
		public string UserGuid { get; set; }
		public string OSVersion { get; set; }
	}
}

