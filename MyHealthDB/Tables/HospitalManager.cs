using System;
using System.Collections.Generic;

namespace MyHealthDB
{
	public class HosptialManager
	{
		static HosptialManager ()
		{
		}

		public static Hosptial GetItemAt (int id)
		{
			return DatabaseRepository.GetItem (id);
		}

		public static IList<Hosptial> GetAllItems ()
		{
			return new List<Hosptial> (DatabaseRepository.GetItems ());
		}

		public static int SaveItem( Hosptial item ) 
		{
			return DatabaseRepository.SaveItem (item);
		}

		public static int DeleteItem (int id)
		{
			return DatabaseRepository.DeleteItem (id);
		}
	}
}

