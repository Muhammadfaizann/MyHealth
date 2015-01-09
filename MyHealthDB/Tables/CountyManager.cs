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
			return DatabaseRepository.GetCounty (id);
		}

		public static List<County> GetAllCounties ()
		{
			return new List<County> (DatabaseRepository.GetAllCounties ());
		}

		public static int SaveCounty( County item ) 
		{
			return DatabaseRepository.SaveCounty (item);
		}

		public static int DeleteCounty (int id)
		{
			return DatabaseRepository.DeleteCounty (id);
		}
	}
}

