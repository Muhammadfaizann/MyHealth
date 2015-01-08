using System;
using System.Collections.Generic;

namespace MyHealthDB
{
	public class EmergencyContactManager
	{
		static EmergencyContactManager ()
		{
		}

		public static EmergencyContact GetItemAt (int id)
		{
			return DatabaseRepository.GetItem (id);
		}

		public static IList<EmergencyContact> GetAllItems ()
		{
			return new List<EmergencyContact> (DatabaseRepository.GetItems ());
		}

		public static int SaveItem( EmergencyContact item ) 
		{
			return DatabaseRepository.SaveItem (item);
		}

		public static int DeleteItem (int id)
		{
			return DatabaseRepository.DeleteItem (id);
		}
	}
}

