using System;
using MyHealth.DB.SQLite;
using MyHealthDB.Model;

namespace MyHealthDB
{
	public class Province : DBEntityBase
	{
		public Province()
		{
		}

		public String Name { get; set; }
	}
}

