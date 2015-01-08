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
			return DatabaseRepository.GetHelpData (id);
		}

		public static IList<HelpData> GetAllHelpData ()
		{
			return new List<HelpData> (DatabaseRepository.GetAllHelpData ());
		}

		public static int SaveHelpData( HelpData item ) 
		{
			return DatabaseRepository.SaveHelpData (item);
		}

		public static int DeleteHelpData (int id)
		{
			return DatabaseRepository.DeleteHelpData (id);
		}
	}
}

