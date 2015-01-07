using System;
using System.Collections.Generic;

namespace MyHealthDB
{
	public class HealthSearchManager
	{
		static HealthSearchManager ()
		{
		}

		public static HealthSearch GetItemAt (int id)
		{
			return DatabaseRepository.GetItem (id);
		}

		public static IList<HealthSearch> GetAllItems ()
		{
			return new List<HealthSearch> (DatabaseRepository.GetItems ());
		}

		public static int SaveItem( HealthSearch item ) 
		{
			return DatabaseRepository.SaveItem (item);
		}

		public static int DeleteItem (int id)
		{
			return DatabaseRepository.DeleteItem (id);
		}
	}
}

