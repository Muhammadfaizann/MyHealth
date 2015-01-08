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
			return DatabaseRepository.GetDisease (id);
		}

		public static IList<Disease> GetAllCounties ()
		{
			return new List<Disease> (DatabaseRepository.GetAllDisease());
		}

		public static int SaveDisease( Disease item ) 
		{
			return DatabaseRepository.SaveDisease (item);
		}

		public static int DeleteDisease (int id)
		{
			return DatabaseRepository.DeleteDisease (id);
		}
	}
}

