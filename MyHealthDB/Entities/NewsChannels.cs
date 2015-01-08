using System;
using MyHealth.DB.SQLite;

namespace MyHealthDB
{
	public class NewsChannels : DBEntityBase
	{
		public NewsChannels()
		{
		}

		[PrimaryKey]
		public int ID { get; set; }
		public String Name { get; set; }
		public String RSSFeed URL { get;set; }
		public String WebsiteURL {get;set;}
	}
}

