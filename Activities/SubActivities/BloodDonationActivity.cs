
using System;
using System.Collections.Generic;

using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Net;
using Android.OS;

using Android.Views;
using Android.Widget;
using MyHealthAndroid.Model;
using MyHealthDB;
using MyHealthDB.Logger;

namespace MyHealthAndroid
{
	[Activity (Label = "MyHealth", ScreenOrientation = global::Android.Content.PM.ScreenOrientation.Portrait)]			
	public class BloodDonationActivity : Activity
	{
		protected async override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			SetContentView (Resource.Layout.sub_activity_blood_donation);

			SetCustomActionBar ();

			await LogManager.Log<LogUsage> (new LogUsage { 
				Date = DateTime.Now, 
				Page = Convert.ToInt32(Pages.BloodDonation)
			});

			var Link = FindViewById (Resource.Id.bloodDonationLink);
			Link.Click += async (object sender, EventArgs e) => {
				var uri = Android.Net.Uri.Parse ("https://www.giveblood.ie"); //RCSI-305
                var intent = new Intent (Intent.ActionView, uri); 
				StartActivity (intent);
				await LogManager.Log<LogExternalLink> (new LogExternalLink {
					Date = DateTime.Now, 
					Link = "https://www.giveblood.ie" //RCSI-305
                });
			};

			// back button
			var _backButton = FindViewById<Button> (Resource.Id.backButton);
			_backButton.Text = "Blood Donation";
			_backButton.Click += (object sender, EventArgs e) => 
			{
				base.OnBackPressed();
			};

			var _homeButton = FindViewById<TextView> (Resource.Id.txtAppTitle);
			_homeButton.MovementMethod = Android.Text.Method.LinkMovementMethod.Instance;
			_homeButton.Touch += delegate {
				var homeActivity = new Intent (this, typeof(HomeActivity));
                homeActivity.SetFlags(ActivityFlags.ClearTop | ActivityFlags.ClearTask | ActivityFlags.NewTask);
				StartActivity (homeActivity);
            };

			await UpdateBloodSupplyList ();
		}

		private async Task UpdateBloodSupplyList() 
		{
			IList<BloodSupply> bloodSupplyList;
			bloodSupplyList = GetBloodSupply ();
			this.SetLables (bloodSupplyList);

			//var connectivityManager = (ConnectivityManager) GetSystemService (ConnectivityService);
			//var activeConnection = connectivityManager.ActiveNetworkInfo;
			//if ((activeConnection != null) && activeConnection.IsConnected)
			if(NetworkStatus.IsActive() && NetworkStatus.IsConnected())
			{
				try {
					bloodSupplyList = await ServiceConsumer.GetBloodDonationInfo ("https://www.giveblood.ie/clinicsxml.aspx?blood=1"); //RCSI-305
					// adding new item for fetch date from service to be displayed in label
					bloodSupplyList.Add(new BloodSupply { BloodGroup = "FETCHDATE", SupplyDays = DateTime.Now.Date.ToString("dd MMM yyyy") });
					SaveBloodSupply (bloodSupplyList);
				} catch {
					bloodSupplyList = GetBloodSupply ();
				}
				this.SetLables (bloodSupplyList);
			}
		}

		private void SetLables (IList<BloodSupply> bloodSupplyList)
		{
			var oPlus = FindViewById<TextView> (Resource.Id.oplusLabel);
			var oMinus = FindViewById<TextView> (Resource.Id.ominusLabel);
			var aPlus = FindViewById<TextView> (Resource.Id.aplusLabel);
			var aMinus = FindViewById<TextView> (Resource.Id.aminusLabel);
			var bPlus = FindViewById<TextView> (Resource.Id.bplusLabel);
			var bMinus = FindViewById<TextView> (Resource.Id.bminusLabel);
			var abPlus = FindViewById<TextView> (Resource.Id.abplusLabel);
			var abMinus = FindViewById<TextView> (Resource.Id.abminusLabel);
			var _bloodDonationLabel = FindViewById<TextView> (Resource.Id.bloodDonationLabel);

			foreach (var bloodSupply in bloodSupplyList) {
				switch (bloodSupply.BloodGroup.ToUpper ()) {
				case "O+":
					oPlus.Text = bloodSupply.SupplyDays;
					break;
				case "O-":
					oMinus.Text = bloodSupply.SupplyDays;
					break;
				case "A+":
					aPlus.Text = bloodSupply.SupplyDays;
					break;
				case "A-":
					aMinus.Text = bloodSupply.SupplyDays;
					break;
				case "B+":
					bPlus.Text = bloodSupply.SupplyDays;
					break;
				case "B-":
					bMinus.Text = bloodSupply.SupplyDays;
					break;
				case "AB+":
					abPlus.Text = bloodSupply.SupplyDays;
					break;
				case "AB-":
					abMinus.Text = bloodSupply.SupplyDays;
					break;

				case "FETCHDATE":
					_bloodDonationLabel.Text = "Refreshed on " + bloodSupply.SupplyDays;
					break;
				}
			}
		}

		//------------------------ custom activity ----------------------//
		#region [Blood Supply Details]

		public static void SaveBloodSupply(IList<BloodSupply> bloodSupplyList) {
			ISharedPreferences prefs = Application.Context.GetSharedPreferences ("MyHealthAppPrefs", FileCreationMode.Private);//PreferenceManager.GetDefaultSharedPreferences (_context);
			ISharedPreferencesEditor editor = prefs.Edit ();

			var dict = new List<string> ();

			foreach (var item in bloodSupplyList) {
				dict.Add (item.BloodGroup + ":" + item.SupplyDays);
			}

			editor.PutStringSet ("BloodSupplyList", dict);

			editor.Apply ();
		}

		public static IList<BloodSupply> GetBloodSupply() {
			ISharedPreferences prefs = Application.Context.GetSharedPreferences("MyHealthAppPrefs", FileCreationMode.Private);
			var dict = prefs.GetStringSet ("BloodSupplyList", new List<string>());

			List<BloodSupply> toReturn = new List<BloodSupply> ();

			if (dict != null) {
				foreach (var item in dict) {
					string[] items = item.Split (':');
					toReturn.Add (new BloodSupply() {
						BloodGroup = items[0].ToString(),
						SupplyDays = items[1].ToString()
					});
				}
			}
			return toReturn;
		}

		#endregion

		//------------------------ custom activity ----------------------//
		public void SetCustomActionBar () 
		{
			ActionBar.SetDisplayShowHomeEnabled (false);
			ActionBar.SetDisplayShowTitleEnabled (false);
			ActionBar.SetCustomView (Resource.Layout.actionbar_custom);
			ActionBar.SetDisplayShowCustomEnabled (true);
		}

		//------------------------ menu item ----------------------//
		public override bool OnCreateOptionsMenu (IMenu menu)
		{
			MenuInflater.Inflate (Resource.Menu.main_activity_actions, menu);
			return base.OnCreateOptionsMenu (menu);
		}

		public override bool OnMenuItemSelected (int featureId, IMenuItem item)
		{
			switch (item.ItemId) {

			case Resource.Id.action_profile:
				var newActivity = new Intent(this, typeof(MyProfileActivity));
				StartActivity(newActivity);
				break;
			}
			return base.OnMenuItemSelected (featureId, item);
		}
	}
}

