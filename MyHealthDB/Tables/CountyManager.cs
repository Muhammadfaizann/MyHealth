using System;
using System.Collections.Generic;

namespace MyHealthDB
{
	public class CountyManager
	{
		static CountyManager ()
		{
		}

		public static County GetCounty (int id)
		{
			return null; //DatabaseRepository.GetCounty (id);
		}

		public static List<County> GetAllCounties ()
		{
			return null; //new List<County> (DatabaseRepository.GetAllCounties ());
		}

		public static int SaveCounty( County item ) 
		{
			return 0; // DatabaseRepository.SaveCounty (item);
		}

		public static int DeleteCounty (int id)
		{
			return 0; //DatabaseRepository.DeleteCounty (id);
		}
	}
}

