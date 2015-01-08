using System;
using System.Collections.Generic;

namespace MyHealthDB
{
	public class NewsChannelsManager
	{
		static NewsChannelsManager ()
		{
		}

		public static NewsChannels GetItemAt (int id)
		{
			return DatabaseRepository.GetItem (id);
		}

		public static IList<NewsChannels> GetAllItems ()
		{
			return new List<NewsChannels> (DatabaseRepository.GetItems ());
		}

		public static int SaveItem( NewsChannels item ) 
		{
			return DatabaseRepository.SaveItem (item);
		}

		public static int DeleteItem (int id)
		{
			return DatabaseRepository.DeleteItem (id);
		}
	}
}

