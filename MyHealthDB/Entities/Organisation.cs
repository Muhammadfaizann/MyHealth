using System;

namespace MyHealthDB
{
    public class Organisation : DBEntityBase
	{
//		public Organisation(SMtblHealthOrganizationsInfo organizatoin)
//		{
//			ID = organizatoin.Id;
//			Name = organizatoin.Name;
//			PhoneNumber = organizatoin.Number;
//			URL = organizatoin.Website;
//		}

		public Organisation ()
		{

		}

		//[PrimaryKey]
		//public int ID { get; set; }
		public String Name { get; set; }
		public String PhoneNumber { get; set; }
		public String URL { get; set;}
		public bool? isArchived { get; set;}
	}
}

