using System;

namespace MyHealthDB.Model
{
	public class SMConditionCategories
	{
		public SMConditionCategories ()
		{
		}

		public int CategoryId { get; set; }
		public int[] ConditionId { get; set; }
	}
}

