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
			return null; //DatabaseRepository.GetNewsChannels (id);
		}

		public static List<NewsChannels> GetAllNewsChannels ()
		{
			return null; //new List<NewsChannels> (DatabaseRepository.GetAllNewsChannels ());
		}

		public static int SaveNewsChannels( NewsChannels item ) 
		{
			return 0; //DatabaseRepository.SaveNewsChannels (item);
		}

		public static int DeleteNewsChannels (int id)
		{
			return 0; //DatabaseRepository.DeleteNewsChannels (id);
		}
	}
}

