using SQLite;

namespace MyHealthDB
{
    public class DBEntityBase : IDBEntity
	{
		public DBEntityBase ()
		{
		}

		[PrimaryKey]
		public int? ID {get; set;}
	}
}

