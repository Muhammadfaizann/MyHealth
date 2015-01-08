using System;
using System.Collections.Generic;

namespace MyHealthDB
{
	public class DiseaseManager
	{
		static DiseaseManager ()
		{
		}

		public static Disease GetItemAt (int id)
		{
			return DatabaseRepository.GetItem (id);
		}

		public static IList<Disease> GetAllItems ()
		{
			return new List<Disease> (DatabaseRepository.GetItems ());
		}

		public static int SaveItem( Disease item ) 
		{
			return DatabaseRepository.SaveItem (item);
		}

		public static int DeleteItem (int id)
		{
			return DatabaseRepository.DeleteItem (id);
		}
	}
}

