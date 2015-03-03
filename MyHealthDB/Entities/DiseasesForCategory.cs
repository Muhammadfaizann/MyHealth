using System;
using MyHealthDB.Model;

namespace MyHealthDB
{
	public class DiseasesForCategory : DBEntityBase
	{
		public DiseasesForCategory ()
		{
		}

//		public DiseasesForCategory (SMConditionCategories cc)
//		{
//			ConditionId = cc.ConditionId;
//			CategoryId = string.Join (",", cc.CategoryId);
//		}

		public int CategoryId { get; set; }
		public string ConditionId { get; set; }
	}
}

