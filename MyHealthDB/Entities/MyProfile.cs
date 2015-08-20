using System;
using MyHealth.DB.SQLite;
using MyHealthDB.Model;

namespace MyHealthDB
{
	public class MyProfile : DBEntityBase
	{
		public MyProfile ()
		{
		}
			//public int Id { get; set; }
			public int ApplicationId { get; set; }
			public int Height_Metre { get; set; }
			public int Height_Centimetre { get; set; }
			public int Weight_Kg  { get; set; }
			public int Weight_Grams { get; set; }
			public String County { get; set; }
			public String AgeRange { get; set; }
			public String Gender { get; set; }
			public String BloodGroup { get; set; }
			public String DeviceId { get; set; }

	}
}

