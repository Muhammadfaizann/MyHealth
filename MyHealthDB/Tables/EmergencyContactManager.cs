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
			return null;//DatabaseRepository.GetEmergencyContact (id);
		}

		public static List<EmergencyContacts> GetAllEmergencyContacts ()
		{
			return null; //new List<EmergencyContacts> (DatabaseRepository.GetAllEmergencyContacts ());
		}

		public static int SaveEmergencyContact( EmergencyContacts item ) 
		{
			return 0;// DatabaseRepository.SaveEmergencyContact (item);
		}

		public static int DeleteEmergencyContact (int id)
		{
			return 0;// DatabaseRepository.DeleteEmergencyContact (id);
		}
	}
}

