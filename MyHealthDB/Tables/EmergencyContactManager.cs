using System;
using System.Collections.Generic;

namespace MyHealthDB
{
	public class EmergencyContactManager
	{
		static EmergencyContactManager ()
		{
		}

		public static EmergencyContact GetEmergencyContact (int id)
		{
			return DatabaseRepository.GetEmergencyContact (id);
		}

		public static IList<EmergencyContact> GetAllEmergencyContacts ()
		{
			return new List<EmergencyContact> (DatabaseRepository.GetAllEmergencyContacts ());
		}

		public static int SaveEmergencyContact( EmergencyContact item ) 
		{
			return DatabaseRepository.SaveEmergencyContact (item);
		}

		public static int DeleteEmergencyContact (int id)
		{
			return DatabaseRepository.DeleteEmergencyContact (id);
		}
	}
}

