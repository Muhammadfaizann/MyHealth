using System;

namespace MyHealthDB.Model
{
	public class SMtblCounty
	{
		public SMtblCounty ()
		{
		}
		public int Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }

		public int ProvinceId { get; set; }
	}
}

