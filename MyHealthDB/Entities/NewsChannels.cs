using System;

namespace MyHealthDB
{
    public class NewsChannels : DBEntityBase
	{
		public NewsChannels()
		{
		}

		//[PrimaryKey]
		//public int ID { get; set; }
		public int resourceID { get; set;}
		public String Name { get; set; }
		public String RSSFeedURL { get;set; }
		public String WebsiteURL {get;set;}
	}
}

