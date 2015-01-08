using System;
using System.Collections.Generic;

namespace MyHealthDB
{
	public class HelpDataManager
	{
		static HelpDataManager ()
		{
		}

		public static HelpData GetItemAt (int id)
		{
			return DatabaseRepository.GetItem (id);
		}

		public static IList<HelpData> GetAllItems ()
		{
			return new List<HelpData> (DatabaseRepository.GetItems ());
		}

		public static int SaveItem( HelpData item ) 
		{
			return DatabaseRepository.SaveItem (item);
		}

		public static int DeleteItem (int id)
		{
			return DatabaseRepository.DeleteItem (id);
		}
	}
}

