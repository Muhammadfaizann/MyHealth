using System;
using System.Collections.Generic;

namespace MyHealthDB
{
	public class CountyManager
	{
		static CountyManager ()
		{
		}

		public static County GetItemAt (int id)
		{
			return DatabaseRepository.GetItem (id);
		}

		public static IList<County> GetAllItems ()
		{
			return new List<County> (DatabaseRepository.GetItems ());
		}

		public static int SaveItem( County item ) 
		{
			return DatabaseRepository.SaveItem (item);
		}

		public static int DeleteItem (int id)
		{
			return DatabaseRepository.DeleteItem (id);
		}
	}
}

