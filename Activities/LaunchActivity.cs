using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Xamarin;
using Android.Telephony;
using Java.Util;

namespace MyHealthAndroid
{
	[Activity(Theme = "@style/Theme.Splash", MainLauncher = true, NoHistory = true,
		ScreenOrientation = global::Android.Content.PM.ScreenOrientation.Portrait)]
	public class LaunchActivity : Activity
	{
		protected async override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			Xamarin.Insights.Initialize ("0f69e93547fe9808f9e454c362ec1f65a490762c", this.BaseContext);

			try  {
				throw (new Exception());
			} catch (Exception exception) {
				Xamarin.Insights.Report (exception);
			}
			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Launch);

			AlertDialog.Builder alert = new AlertDialog.Builder (this);
			alert.SetTitle("Accept Terms of Use");
			alert.SetMessage("Read our terms and conditions");

			alert.SetCancelable (false);
			
			alert.SetPositiveButton ("Agree", async (senderAlert, args) => {
				if (!ifDatabaseExists(BaseContext)) {
					await MyHealthDB.ServiceConsumer.CreateDatabase();
					await MyHealthDB.Logger.LogManager.SyncAllLogs ();
					SaveDatabaseExits(BaseContext);
					StartActivity (new Intent(this, typeof(MyProfileActivity)));
				} else {
					StartActivity(typeof(HomeActivity));
				}
			});

			alert.SetNegativeButton ("Don't Agree", (senderAlert, args) => {
				System.Environment.Exit(0);
			});
			//run the alert in UI thread to display in the screen
			RunOnUiThread (() => {
				alert.Show();
			});
		}
			
		protected override void OnPause()
		{
			base.OnPause ();
		}

		//------------------------ Check If Databases Exists ----------------------//
		private Boolean ifDatabaseExists (Context _context) {
			ISharedPreferences prefs = Application.Context.GetSharedPreferences("MyHealthAppPrefs", FileCreationMode.Private);//PreferenceManager.GetDefaultSharedPreferences (_context);
			return prefs.GetBoolean ("dbExists", false);

		}

		private void SaveDatabaseExits (Context _context) {

			ISharedPreferences prefs = Application.Context.GetSharedPreferences ("MyHealthAppPrefs", FileCreationMode.Private);//PreferenceManager.GetDefaultSharedPreferences (_context);
			ISharedPreferencesEditor editor = prefs.Edit ();

			editor.PutBoolean ("dbExists", true);
			editor.Apply ();
		}
		//------------------------ Get Device ID ----------------------//

		private string RetreiveDeviceID ()
		{
			string telephonyDeviceID = "";
			//string telephonySIMSerialNumber = "";

			TelephonyManager telephonyManager = (TelephonyManager) GetSystemService (Context.TelephonyService);
			if (telephonyManager != null)
			{
				if(!string.IsNullOrEmpty(telephonyManager.DeviceId))
					telephonyDeviceID = telephonyManager.DeviceId;
//				if(!string.IsNullOrEmpty(telephonyManager.SimSerialNumber))
//					telephonySIMSerialNumber = telephonyManager.SimSerialNumber;
			}
			var androidID = Android.Provider.Settings.Secure.GetString(ApplicationContext.ContentResolver, Android.Provider.Settings.Secure.AndroidId);
			var deviceUuid = new UUID (androidID.GetHashCode (), ((long)telephonyDeviceID.GetHashCode () << 32));//| telephonySIMSerialNumber.GetHashCode());
			return deviceUuid.ToString();
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

