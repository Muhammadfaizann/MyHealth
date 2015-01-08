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
			return DatabaseRepository.GetDiseaseCategory (id);
		}

		public static IList<DiseaseCategory> GetAllDiseaseCategories ()
		{
			return new List<DiseaseCategory> (DatabaseRepository.GetAllDiseaseCategories ());
		}

		public static int SaveDiseaseCategory( DiseaseCategory item ) 
		{
			return DatabaseRepository.SaveDiseaseCategory (item);
		}

		public static int DeleteDiseaseCategory (int id)
		{
			return DatabaseRepository.DeleteDiseaseCategory (id);
		}
	}
}

