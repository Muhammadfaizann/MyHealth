using System;
using System.Collections.Generic;

namespace MyHealthDB
{
	public class NewsChannelsManager
	{
		static NewsChannelsManager ()
		{
		}

		public static NewsChannels GetNewsChannels (int id)
		{
			return DatabaseRepository.GetNewsChannels (id);
		}

		public static IList<NewsChannels> GetAllNewsChannels ()
		{
			return new List<NewsChannels> (DatabaseRepository.GetAllNewsChannels ());
		}

		public static int SaveNewsChannels( NewsChannels item ) 
		{
			return DatabaseRepository.SaveNewsChannels (item);
		}

		public static int DeleteNewsChannels (int id)
		{
			return DatabaseRepository.DeleteNewsChannels (id);
		}
	}
}

