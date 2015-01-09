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
			return DatabaseRepository.GetUsefullNumbers (id);
		}

		public static List<UsefullNumbers> GetAllUsefullNumbers ()
		{
			return new List<UsefullNumbers> (DatabaseRepository.GetAllUsefullNumbers ());
		}

		public static int SaveUsefullNumbers( UsefullNumbers item ) 
		{
			return DatabaseRepository.SaveUsefullNumbers (item);
		}

		public static int DeleteUsefullNumbers (int id)
		{
			return DatabaseRepository.DeleteUsefullNumbers (id);
		}
	}
}

