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

		public static HealthSearch GetItem (int id) 
		{
			return me.db.GetItem<HealthSearch> (id);
		}

		public static IEnumerable<HealthSearch> GetItems ()
		{
			return me.db.GetItems<HealthSearch> ();
		}

		public static int SaveItem (HealthSearch item)
		{
			return me.db.SaveItem<HealthSearch> (item);
		}

		public static int DeleteItem (int id )
		{
			return me.db.DeleteItem<HealthSearch> (id);
		}
	}
}

