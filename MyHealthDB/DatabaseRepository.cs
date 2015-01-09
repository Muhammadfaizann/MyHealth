using System;
using System.IO;
using System.Collections.Generic;

namespace MyHealthDB
{
	public class DatabaseRepository
	{
		DatabaseManager db = null;
		protected static string dbLocation;
		protected static DatabaseRepository me;

		static DatabaseRepository ()
		{
			me = new DatabaseRepository ();
		}



		protected DatabaseRepository ()
		{
			dbLocation = DatabaseFilePath;
			db = new DatabaseManager (dbLocation);
		}



		public static string DatabaseFilePath {
			get {
				var sqliteFilename = "MyHealthDB.db3";

				#if SILVERLIGHT
				var path = sqliteFilename;
				#else

				#if __ANDROID__
				string libraryPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);

				#else

				string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
				string libraryPath = Path.Combine (documentsPath, "..", "Library");

				#endif

				var path = Path.Combine(libraryPath, sqliteFilename);

				#endif

				return path;
			}
		}



//		public static HealthSearch GetItem (int id)
//		{
//			return me.db.GetItem<HealthSearch> (id);
//		}
//
//
//
//		public static IEnumerable<HealthSearch> GetItems ()
//		{
//			return me.db.GetItems<HealthSearch> ();
//		}
//
//
//
//		public static int SaveItem (HealthSearch item)
//		{
//			return me.db.SaveItem<HealthSearch> (item);
//		}
//
//
//
//		public static int DeleteItem (int id)
//		{
//			return me.db.DeleteItem<HealthSearch> (id);
//		}


		#region[County]

		public static County GetCounty (int id)
		{
			return me.db.GetItem<County> (id);
		}



		public static IList<County> GetAllCounties ()
		{
			return me.db.GetItems<County> ();
		}



		public static int SaveCounty(County item)
		{
			return me.db.SaveItem<County> (item);
		}



		public static int DeleteCounty (int id)
		{
			return me.db.DeleteItem<County> (id);
		}


		#endregion

		#region[DiseaseCategory]


		public static DiseaseCategory GetDiseaseCategory (int id)
		{

			return me.db.GetItem<DiseaseCategory> (id);
		}



		public static IList<DiseaseCategory> GetAllDiseaseCategories ()
		{

			return me.db.GetItems<DiseaseCategory> ();
		}



		public static int SaveDiseaseCategory(DiseaseCategory item)
		{

			return me.db.SaveItem<DiseaseCategory> (item);
		}



		public static int DeleteDiseaseCategory (int id)
		{

			return me.db.DeleteItem<DiseaseCategory> (id);
		}


		#endregion

		#region[Disease]

		public static Disease GetDisease (int id)
		{

			return me.db.GetItem<Disease> (id);
		}



		public static IList<Disease> GetAllDisease()
		{

			return me.db.GetItems<Disease> ();
		}



		public static int SaveDisease(Disease item)
		{

			return me.db.SaveItem<Disease> (item);
		}



		public static int DeleteDisease (int id)
		{

			return me.db.DeleteItem<Disease> (id);
		}


		#endregion

		#region[EmergencyContacts]

		public static EmergencyContacts GetEmergencyContact (int id)
		{

			return me.db.GetItem<EmergencyContacts> (id);
		}



		public static IList<EmergencyContacts> GetAllEmergencyContacts()
		{

			return me.db.GetItems<EmergencyContacts> ();
		}



		public static int SaveEmergencyContact(EmergencyContacts item)
		{

			return me.db.SaveItem<EmergencyContacts> (item);
		}



		public static int DeleteEmergencyContact (int id)
		{

			return me.db.DeleteItem<EmergencyContacts> (id);
		}


		#endregion

		#region[HelpData]

		public static HelpData GetHelpData (int id)
		{

			return me.db.GetItem<HelpData> (id);
		}



		public static IList<HelpData> GetAllHelpData()
		{

			return me.db.GetItems<HelpData> ();
		}



		public static int SaveHelpData(HelpData item)
		{

			return me.db.SaveItem<HelpData> (item);
		}



		public static int DeleteHelpData (int id)
		{

			return me.db.DeleteItem<HelpData> (id);
		}


		#endregion

		#region[Hospital]

		public static Hospital GetHospital (int id)
		{

			return me.db.GetItem<Hospital> (id);
		}



		public static IList<Hospital> GetAllHospitals()
		{

			return me.db.GetItems<Hospital> ();
		}



		public static int SaveHospital(Hospital item)
		{

			return me.db.SaveItem<Hospital> (item);
		}



		public static int DeleteHospital (int id)
		{

			return me.db.DeleteItem<Hospital> (id);
		}


		#endregion

		#region[NewsChannels]

		public static NewsChannels GetNewsChannels (int id)
		{

			return me.db.GetItem<NewsChannels> (id);
		}



		public static IList<NewsChannels> GetAllNewsChannels()
		{

			return me.db.GetItems<NewsChannels> ();
		}



		public static int SaveNewsChannels(NewsChannels item)
		{

			return me.db.SaveItem<NewsChannels> (item);
		}



		public static int DeleteNewsChannels (int id)
		{

			return me.db.DeleteItem<NewsChannels> (id);
		}


		#endregion

		#region[Organisation]

		public static Organisation GetOrganisation (int id)
		{

			return me.db.GetItem<Organisation> (id);
		}



		public static IList<Organisation> GetAllOrganisations()
		{

			return me.db.GetItems<Organisation> ();
		}



		public static int SaveOrganisation(Organisation item)
		{

			return me.db.SaveItem<Organisation> (item);
		}



		public static int DeleteOrganisation (int id)
		{

			return me.db.DeleteItem<Organisation> (id);
		}


		#endregion

		#region[UsefullNumbers]

		public static UsefullNumbers GetUsefullNumbers (int id)
		{

			return me.db.GetItem<UsefullNumbers> (id);
		}



		public static IList<UsefullNumbers> GetAllUsefullNumbers()
		{

			return me.db.GetItems<UsefullNumbers> ();
		}



		public static int SaveUsefullNumbers(UsefullNumbers item)
		{

			return me.db.SaveItem<UsefullNumbers> (item);
		}



		public static int DeleteUsefullNumbers (int id)
		{

			return me.db.DeleteItem<UsefullNumbers> (id);
		}


		#endregion
		
	}
}

