using System;
using System.Collections.Generic;

namespace MyHealthDB
{
	public class DiseaseCategoryManager
	{
		static DiseaseCategoryManager ()
		{
		}

		public static DiseaseCategory GetDiseaseCategory (int id)
		{
			return null; //DatabaseRepository.GetDiseaseCategory (id);
		}

		public static List<DiseaseCategory> GetAllDiseaseCategories ()
		{
			return null; //new List<DiseaseCategory> (DatabaseRepository.GetAllDiseaseCategories ());
		}

		public static int SaveDiseaseCategory( DiseaseCategory item ) 
		{
			return 0; //DatabaseRepository.SaveDiseaseCategory (item);
		}

		public static int DeleteDiseaseCategory (int id)
		{
			return 0; //DatabaseRepository.DeleteDiseaseCategory (id);
		}
	}
}

