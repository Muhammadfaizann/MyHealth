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
			return null; //DatabaseRepository.GetHospital (id);
		}

		public static List<Hospital> GetAllHospitals ()
		{
			return null; //new List<Hospital> (DatabaseRepository.GetAllHospitals ());
		}

		public static int SaveHospital( Hospital item ) 
		{
			return 0; //DatabaseRepository.SaveHospital (item);
		}

		public static int DeleteHospital (int id)
		{
			return 0; //DatabaseRepository.DeleteHospital (id);
		}
	}
}

