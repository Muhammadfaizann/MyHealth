using System;
using System.Collections.Generic;

namespace MyHealthDB
{
	public class UsefullNumberManager
	{
		static UsefullNumberManager()
		{
		}

		public static UsefullNumbers GetUsefullNumbers (int id)
		{
			return null; //DatabaseRepository.GetUsefullNumbers (id);
		}

		public static List<UsefullNumbers> GetAllUsefullNumbers ()
		{
			return null; //new List<UsefullNumbers> (DatabaseRepository.GetAllUsefullNumbers ());
		}

		public static int SaveUsefullNumbers( UsefullNumbers item ) 
		{
			return 0; //DatabaseRepository.SaveUsefullNumbers (item);
		}

		public static int DeleteUsefullNumbers (int id)
		{
			return 0; // DatabaseRepository.DeleteUsefullNumbers (id);
		}
	}
}

