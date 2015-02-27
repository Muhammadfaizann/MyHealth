using System;
using UIKit;
using Foundation;
using System.Threading.Tasks;

using System.Collections.Generic;
using System.Linq;
using MyHealthDB;


namespace RCSI
{
	public class HelperMethods
	{
		public static List<Disease> RecentDiseases {
			get;
			set;
		}

		public HelperMethods ()
		{
		}

		public static void AddRecentDisease(Disease selectedDisease) {
			if (RecentDiseases == null) {
				RecentDiseases = new List<Disease> ();
			}

			if (RecentDiseases.Where (d => d.ID.Value == selectedDisease.ID.Value).Count() == 0) {
				RecentDiseases.Add (selectedDisease);

				SaveRecentDiseases (RecentDiseases.Select (i => i.ID.Value).ToArray());
			}
		}

		public static void SaveRecentDiseases (int[] diseaseIds) {
			var userDefs = NSUserDefaults.StandardUserDefaults;
			NSMutableArray _arry = new NSMutableArray (); //(diseaseIds.Length);

			foreach (int id in diseaseIds) {
				//_arry.Add (NSNumber. id);
			}

			//userDefs ["RecentDiseaseIds"] = _arry;

			userDefs.Synchronize ();
		}

		public static nint[] GetRecentDiseases () {
			var userDefs = NSUserDefaults.StandardUserDefaults;

			var _arry = userDefs.ArrayForKey("RecentDiseaseIds");

			List<nint> toReturn = new List<nint> ();

			if (_arry != null) {
				foreach (var item in _arry) {
					toReturn.Add (Convert.ToInt32(item.ToString()));
				}
			}
			RecentDiseases = new List<Disease> ();
			return toReturn.ToArray ();
		}

		public async static Task<Boolean> CheckIfInternetAvailable() {
			return true;
		}
	}
}

