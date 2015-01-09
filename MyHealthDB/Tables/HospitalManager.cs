using System;
using System.Collections.Generic;

namespace MyHealthDB
{
	public class HospitalManager
	{
		static HospitalManager ()
		{
		}

		public static Hospital GetHospital (int id)
		{
			return DatabaseRepository.GetHospital (id);
		}

		public static List<Hospital> GetAllHospitals ()
		{
			return new List<Hospital> (DatabaseRepository.GetAllHospitals ());
		}

		public static int SaveHospital( Hospital item ) 
		{
			return DatabaseRepository.SaveHospital (item);
		}

		public static int DeleteHospital (int id)
		{
			return DatabaseRepository.DeleteHospital (id);
		}
	}
}

