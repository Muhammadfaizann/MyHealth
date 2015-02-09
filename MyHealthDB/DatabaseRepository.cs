using System;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;

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
			//db = new DatabaseManager (dbLocation);
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


//		#region[County]
//
//		public async static Task<County> GetCounty (int id)
//		{
//			return await me.db.GetItem<County> (id);
//		}
//
//
//
//		public async static Task<List<County>> GetAllCounties ()
//		{
//			return await me.db.GetItems<County> ();
//		}
//
//
//
//		public async static Task<int> SaveCounty(County item)
//		{
//			return await me.db.SaveItem<County> (item);
//		}
//
//
//
//		public async static Task<int> DeleteCounty (int id)
//		{
//			return await me.db.DeleteItem<County> (id);
//		}
//
//
//		#endregion
//
//		#region[DiseaseCategory]
//
//
//		public async static Task<DiseaseCategory> GetDiseaseCategory (int id)
//		{
//
//			return await me.db.GetItem<DiseaseCategory> (id);
//		}
//
//
//
//		public async static Task<List<DiseaseCategory>> GetAllDiseaseCategories ()
//		{
//
//			return await me.db.GetItems<DiseaseCategory> ();
//		}
//
//
//
//		public async static Task<int> SaveDiseaseCategory(DiseaseCategory item)
//		{
//
//			return await me.db.SaveItem<DiseaseCategory> (item);
//		}
//
//
//
//		public async static Task<int> DeleteDiseaseCategory (int id)
//		{
//
//			return await me.db.DeleteItem<DiseaseCategory> (id);
//		}
//
//
//		#endregion
//
//		#region[Disease]
//
//		public async static Task<Disease> GetDisease (int id)
//		{
//			return await me.db.GetItem<Disease> (id);
//		}
//
//
//
//		public async static Task<List<Disease>> GetAllDisease()
//		{
//
//			return await me.db.GetItems<Disease> ();
//		}
//
//
//
//		public async static Task<int> SaveDisease(Disease item)
//		{
//			return await me.db.SaveItem<Disease> (item);
//		}
//
//
//
//		public async static Task<int> DeleteDisease (int id)
//		{
//
//			return await me.db.DeleteItem<Disease> (id);
//		}
//
//
//		#endregion
//
//		#region[DiseasesForCategory]
//
//		public async static Task<DiseasesForCategory> GetDiseaseForCategory (int id)
//		{
//
//			return await me.db.GetItem<DiseasesForCategory> (id);
//		}
//
//
//
//		public async static Task<List<DiseasesForCategory>> GetAllDiseasesForCategories()
//		{
//
//			return await me.db.GetItems<DiseasesForCategory> ();
//		}
//
//
//
//		public async static Task<int> SaveDiseasesForCategory(DiseasesForCategory item)
//		{
//
//			return await me.db.SaveItem<DiseasesForCategory> (item);
//		}
//
//
//
//		public async static Task<int> DeleteDiseasesForCategory (int id)
//		{
//
//			return await me.db.DeleteItem<DiseasesForCategory> (id);
//		}
//
//
//		#endregion
//
//		#region[EmergencyContacts]
//
//		public async static Task<EmergencyContacts> GetEmergencyContact (int id)
//		{
//
//			return await me.db.GetItem<EmergencyContacts> (id);
//		}
//
//
//
//		public static IList<EmergencyContacts> GetAllEmergencyContacts()
//		{
//
//			return me.db.GetItems<EmergencyContacts> ();
//		}
//
//
//
//		public static int SaveEmergencyContact(EmergencyContacts item)
//		{
//
//			return me.db.SaveItem<EmergencyContacts> (item);
//		}
//
//
//
//		public static int DeleteEmergencyContact (int id)
//		{
//
//			return me.db.DeleteItem<EmergencyContacts> (id);
//		}
//
//
//		#endregion
//
//		#region[HelpData]
//
//		public static HelpData GetHelpData (int id)
//		{
//
//			return me.db.GetItem<HelpData> (id);
//		}
//
//
//
//		public static IList<HelpData> GetAllHelpData()
//		{
//
//			return me.db.GetItems<HelpData> ();
//		}
//
//
//
//		public static int SaveHelpData(HelpData item)
//		{
//
//			return me.db.SaveItem<HelpData> (item);
//		}
//
//
//
//		public static int DeleteHelpData (int id)
//		{
//
//			return me.db.DeleteItem<HelpData> (id);
//		}
//
//
//		#endregion
//
//		#region[Hospital]
//
//		public static Hospital GetHospital (int id)
//		{
//
//			return me.db.GetItem<Hospital> (id);
//		}
//
//
//
//		public static IList<Hospital> GetAllHospitals()
//		{
//
//			return me.db.GetItems<Hospital> ();
//		}
//
//
//
//		public static int SaveHospital(Hospital item)
//		{
//
//			return me.db.SaveItem<Hospital> (item);
//		}
//
//
//
//		public static int DeleteHospital (int id)
//		{
//
//			return me.db.DeleteItem<Hospital> (id);
//		}
//
//
//		#endregion
//
//		#region[NewsChannels]
//
//		public static NewsChannels GetNewsChannels (int id)
//		{
//
//			return me.db.GetItem<NewsChannels> (id);
//		}
//
//
//
//		public static IList<NewsChannels> GetAllNewsChannels()
//		{
//
//			return me.db.GetItems<NewsChannels> ();
//		}
//
//
//
//		public static int SaveNewsChannels(NewsChannels item)
//		{
//
//			return me.db.SaveItem<NewsChannels> (item);
//		}
//
//
//
//		public static int DeleteNewsChannels (int id)
//		{
//
//			return me.db.DeleteItem<NewsChannels> (id);
//		}
//
//
//		#endregion
//
//		#region[Organisation]
//
//		public static Organisation GetOrganisation (int id)
//		{
//
//			return me.db.GetItem<Organisation> (id);
//		}
//
//
//
//		public static IList<Organisation> GetAllOrganisations()
//		{
//
//			return me.db.GetItems<Organisation> ();
//		}
//
//
//
//		public static int SaveOrganisation(Organisation item)
//		{
//
//			return me.db.SaveItem<Organisation> (item);
//		}
//
//
//
//		public static int DeleteOrganisation (int id)
//		{
//
//			return me.db.DeleteItem<Organisation> (id);
//		}
//
//
//		#endregion
//
//		#region[UsefullNumbers]
//
//		public static UsefullNumbers GetUsefullNumbers (int id)
//		{
//
//			return me.db.GetItem<UsefullNumbers> (id);
//		}
//
//
//
//		public static IList<UsefullNumbers> GetAllUsefullNumbers()
//		{
//
//			return me.db.GetItems<UsefullNumbers> ();
//		}
//
//
//
//		public static int SaveUsefullNumbers(UsefullNumbers item)
//		{
//
//			return me.db.SaveItem<UsefullNumbers> (item);
//		}
//
//
//
//		public static int DeleteUsefullNumbers (int id)
//		{
//
//			return me.db.DeleteItem<UsefullNumbers> (id);
//		}
//
//
//		#endregion
		
	}
}

