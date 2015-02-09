using System;
using System.Collections.Generic;

namespace MyHealthDB
{
	public class DiseaseManager
	{
		static DiseaseManager ()
		{
		}

		public static Disease GetDisease (int id)
		{
			return null; //DatabaseRepository.GetDisease (id);
		}

		public static IList<Disease> GetAllDiseases ()
		{
			return null; //new List<Disease> (DatabaseRepository.GetAllDisease());
		}

		public static int SaveDisease( Disease item ) 
		{
			return 0; //DatabaseRepository.SaveDisease (item);
		}

		public static int DeleteDisease (int id)
		{
			return 0; //DatabaseRepository.DeleteDisease (id);
		}
	}
}

