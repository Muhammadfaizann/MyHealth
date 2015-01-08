using System;
using System.Collections.Generic;

namespace MyHealthDB
{
	public class OrganisationManager
	{
		static OrganisationManager ()
		{
		}

		public static Organisation GetItemAt (int id)
		{
			return DatabaseRepository.GetItem (id);
		}

		public static IList<Organisation> GetAllItems ()
		{
			return new List<Organisation> (DatabaseRepository.GetItems ());
		}

		public static int SaveItem( Organisation item ) 
		{
			return DatabaseRepository.SaveItem (item);
		}

		public static int DeleteItem (int id)
		{
			return DatabaseRepository.DeleteItem (id);
		}
	}
}

