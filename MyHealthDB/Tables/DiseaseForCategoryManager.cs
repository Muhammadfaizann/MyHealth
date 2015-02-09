using System;
using System.Collections.Generic;

namespace MyHealthDB
{
	public class DiseaseForCategoryManager
	{
		static DiseaseForCategoryManager ()
		{
		}

		public static DiseasesForCategory GetDiseasesForCategory (int id)
		{
			return null; //DatabaseRepository.GetDiseaseForCategory (id);
		}

		public static List<DiseasesForCategory> GetAllDiseaseCategories ()
		{
			return null; //new List<DiseasesForCategory> (DatabaseRepository.GetAllDiseasesForCategories ());
		}

		public static int SaveDiseaseCategory( DiseasesForCategory item ) 
		{
			return 0; //DatabaseRepository.SaveDiseasesForCategory (item);
		}

		public static int DeleteDiseaseCategory (int id)
		{
			return 0; //DatabaseRepository.DeleteDiseasesForCategory (id);
		}
	}
}

