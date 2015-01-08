using System;
using System.Collections.Generic;

namespace MyHealthDB
{
	public class HosptialManager
	{
		static HosptialManager ()
		{
		}

		public static Hospital GetHospital (int id)
		{
			return DatabaseRepository.GetHospital (id);
		}

		public static IList<Hospital> GetAllHospitals ()
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

