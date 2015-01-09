using System;
using MyHealth.DB.SQLite;
using System.Collections.Generic;
using System.Linq;

namespace MyHealthDB
{
	public class DatabaseManager : SQLiteConnection
	{
		static object locker = new object();

		public DatabaseManager (string path) : base (path)
		{
			CreateTable<County> ();
			CreateTable<Disease> ();
			CreateTable<DiseaseCategory> ();
			CreateTable<EmergencyContacts> ();
			CreateTable<HelpData> ();
			CreateTable<Hospital> ();
			CreateTable<NewsChannels> ();
			CreateTable<Organisation> ();
			CreateTable<UsefullNumbers> ();
		}

		public IList<T> GetItems<T> () where T : IDBEntity, new () 
		{
			lock (locker) {
				return (from i in Table<T> ()
				        select i).ToList ();
			}
		}

		public T GetItem<T> (int id) where T : IDBEntity, new() 
		{
			return Table<T> ().FirstOrDefault (x => x.ID == id);
		}

		public int SaveItem<T> (T item) where T : IDBEntity, new()
		{
			lock (locker) {
				if (item.ID > 0 && Table<T>().Any(x => x.ID == item.ID)) {

					Update (item);
					return item.ID;
				} else {
					return Insert (item);
				}
			}
		}

		public int DeleteItem<T> (int id) where T : IDBEntity, new() 
		{
			lock (locker) {
				return Delete<T> (new T () { ID = id });
			}
		}
	}
}

