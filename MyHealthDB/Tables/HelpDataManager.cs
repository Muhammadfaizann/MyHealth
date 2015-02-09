using System;
using System.Collections.Generic;

namespace MyHealthDB
{
	public class HelpDataManager
	{
		static HelpDataManager ()
		{
		}
		public static HelpData GetHelpData (int id)
		{
			return null;// DatabaseRepository.GetHelpData (id);
		}

		public static IList<HelpData> GetAllHelpData ()
		{
			return null; //new List<HelpData> (DatabaseRepository.GetAllHelpData ());
		}

		public static int SaveHelpData( HelpData item ) 
		{
			return 0; //DatabaseRepository.SaveHelpData (item);
		}

		public static int DeleteHelpData (int id)
		{
			return 0; //DatabaseRepository.DeleteHelpData (id);
		}
	}
}

