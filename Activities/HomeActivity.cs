using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MyHealth.Android;
using Android.Preferences;

using MyHealthDB;
using MyHealthDB.Logger;
using Android.Net;

namespace MyHealthAndroid{
	[Activity (Label = "My Health" ,ScreenOrientation = global::Android.Content.PM.ScreenOrientation.Portrait)]			
	public class HomeActivity : Activity
	{
		private ImageButton searchBtn;
		private ImageButton healthProBtn;
		private ImageButton healthNewsBtn;
		private ImageButton helpBtn;
		private Button impNotice;

		protected async override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			SetContentView (Resource.Layout.Home);

			SetCustomActionBar ();
	
			searchBtn = FindViewById<ImageButton> (Resource.Id.healthSearchBtn);
			healthProBtn = FindViewById<ImageButton> (Resource.Id.healthProfessionalBtn);
			healthNewsBtn = FindViewById<ImageButton> (Resource.Id.healthNewsBtn);
			helpBtn = FindViewById<ImageButton> (Resource.Id.helpBtn);
			impNotice = FindViewById<Button> (Resource.Id.impNotice);

			impNotice.Selected = true;

			searchBtn.Click += (object sender, EventArgs e) => 
			{
				var activity = new Intent(this, typeof(HealthSearchActivity));
				StartActivity( activity);
			};

			healthProBtn.Click += (object sender, EventArgs e) => 
			{
				var activity = new Intent(this, typeof(HealthProfessionalActivity));
				StartActivity( activity);
			};

			healthNewsBtn.Click += (object sender, EventArgs e) => 
			{
				var activity = new Intent(this, typeof(HealthNewsActivity));
				StartActivity( activity);
			};

			helpBtn.Click += (object sender, EventArgs e) => 
			{
				var activity = new Intent(this, typeof(HelpActivity));
				StartActivity( activity);
			};

			await LogManager.Log (new LogUsage { 
				Date = DateTime.Now,
				Page = Convert.ToInt32(Pages.Home)
			});

			var preferences = PreferenceManager.GetDefaultSharedPreferences (this.ApplicationContext);
			ISharedPreferencesEditor editor = preferences.Edit();
			if (!preferences.GetBoolean ("applicationUpdated", false)) {
				//Toast.MakeText (this, "Please Sync with latest data.", ToastLength.Long).Show ();
				StartActivity (new Intent (this, typeof(MyProfileActivity)));
			} else {
				var connectivityManager = (ConnectivityManager)GetSystemService (ConnectivityService);
				var activeConnection = connectivityManager.ActiveNetworkInfo;
				if ((activeConnection != null) && activeConnection.IsConnected) {

					string strLastSyncDate = preferences.GetString("LastSyncDate",DateTime.MinValue.ToString("dd-MMM-yyyy HH:mm:ss"));
					DateTime LastSyncDate = Convert.ToDateTime (strLastSyncDate);

					double TotalHours = DateTime.Now.Subtract (LastSyncDate).TotalHours;
					if (TotalHours > 1) {
						await MyHealthDB.ServiceConsumer.SyncDevice ();
						editor.PutString("LastSyncDate", DateTime.Now.ToString("dd-MMM-yyyy"));
						editor.Apply ();
					} else {
						await LogManager.SyncAllLogs ();
					}
				}
			}

		}


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

