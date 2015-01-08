using System;
using System.Collections.Generic;

namespace MyHealthDB
{
	public class DiseaseCategoryManager
	{
		static DiseaseCategoryManager ()
		{
		}

		public static DiseaseCategory GetItemAt (int id)
		{
			return DatabaseRepository.GetItem (id);
		}

		public static IList<DiseaseCategory> GetAllItems ()
		{
			return new List<DiseaseCategory> (DatabaseRepository.GetItems ());
		}

		public static int SaveItem( DiseaseCategory item ) 
		{
			return DatabaseRepository.SaveItem (item);
		}

		public static int DeleteItem (int id)
		{
			return DatabaseRepository.DeleteItem (id);
		}
	}
}

