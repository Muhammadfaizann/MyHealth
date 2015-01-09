using System;
using System.Collections.Generic;

namespace MyHealthDB
{
	public class EmergencyContactManager
	{
		static EmergencyContactManager ()
		{
		}

		public static EmergencyContacts GetEmergencyContact (int id)
		{
			return DatabaseRepository.GetEmergencyContact (id);
		}

		public static List<EmergencyContacts> GetAllEmergencyContacts ()
		{
			return new List<EmergencyContacts> (DatabaseRepository.GetAllEmergencyContacts ());
		}

		public static int SaveEmergencyContact( EmergencyContacts item ) 
		{
			return DatabaseRepository.SaveEmergencyContact (item);
		}

		public static int DeleteEmergencyContact (int id)
		{
			return DatabaseRepository.DeleteEmergencyContact (id);
		}
	}
}

