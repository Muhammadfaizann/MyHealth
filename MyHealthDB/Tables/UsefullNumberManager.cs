using System;
using System.Collections.Generic;

namespace MyHealthDB
{
	public class UsefullNumberManager
	{
		static UsefullNumberManager()
		{
		}

		public static UsefullNumbers GetItemAt (int id)
		{
			return DatabaseRepository.GetItem (id);
		}

		public static IList<UsefullNumbers> GetAllItems ()
		{
			return new List<UsefullNumbers> (DatabaseRepository.GetItems ());
		}

		public static int SaveItem( UsefullNumbers item ) 
		{
			return DatabaseRepository.SaveItem (item);
		}

		public static int DeleteItem (int id)
		{
			return DatabaseRepository.DeleteItem (id);
		}
	}
}

