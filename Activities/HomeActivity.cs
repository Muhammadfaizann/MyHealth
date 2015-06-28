using System;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Net;
using Android.OS;
using Android.Preferences;

using Android.Views;
using Android.Widget;
using MyHealth.Android;
using MyHealthDB;
using MyHealthDB.Logger;
using System.Threading.Tasks;

namespace MyHealthAndroid{
	[Activity (Label = "MyHealth", ScreenOrientation = global::Android.Content.PM.ScreenOrientation.Portrait)]			
	public class HomeActivity : Activity
	{
		private ImageButton searchBtn;
		private ImageButton healthProBtn;
		private ImageButton healthNewsBtn;
		private ImageButton helpBtn;
		private Button impNotice;

		protected async override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);
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

					var importantNotice = await DatabaseManager.SelectImportantNotice(DateTime.Now.Date);
					if (importantNotice != null) {
						impNotice.Text = importantNotice.Name;
						impNotice.SetBackgroundColor (Color.ParseColor (importantNotice.NoticeColor));
					}
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

			var txtAppTitle = ActionBar.CustomView.FindViewById (Resource.Id.txtAppTitle);
			if (txtAppTitle.LayoutParameters is ViewGroup.MarginLayoutParams) {
				ViewGroup.MarginLayoutParams p = (ViewGroup.MarginLayoutParams) txtAppTitle.LayoutParameters;
				p.LeftMargin = 60;
				txtAppTitle.RequestLayout();
			}
		}

		//------------------------ menu item ----------------------//
		public override bool OnCreateOptionsMenu (IMenu menu)
		{
			MenuInflater.Inflate (Resource.Menu.main_activity_actions_with_sync, menu);
			return base.OnCreateOptionsMenu (menu);
		}

		public override bool OnMenuItemSelected (int featureId, IMenuItem item)
		{
			switch (item.ItemId) {
				
			case Resource.Id.action_profile:
				var newActivity = new Intent(this, typeof(MyProfileActivity));
				StartActivity (newActivity);
				break;
			case Resource.Id.action_sync:
				
				try {
					// Get the shared Preferences
					var preferences = PreferenceManager.GetDefaultSharedPreferences (this.ApplicationContext); 
					ISharedPreferencesEditor editor = preferences.Edit();
					Toast.MakeText(this, "Updating database, Please wait.", ToastLength.Long).Show();
					editor.PutBoolean("applicationUpdated", false);
					editor.Apply();
					ServiceConsumer.SyncDevice()
						.ContinueWith((r) => {
							Toast.MakeText(this, "Successfully updated the system.", ToastLength.Long).Show();
							editor.PutBoolean("applicationUpdated", true);
							editor.Apply();
						}, TaskScheduler.FromCurrentSynchronizationContext());
				} catch (Exception ex) {
					Toast.MakeText(this, ex.ToString(), ToastLength.Long).Show();
				}

				break;
			}
			return base.OnMenuItemSelected (featureId, item);
		}
	}
}

