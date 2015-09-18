using System;

using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Net;
using Android.OS;
using Android.Preferences;
using Android.Telephony;
using Android.Views;
using Android.Widget;
using Java.Util;
using System.Threading;

namespace MyHealthAndroid
{
	[Activity(Theme = "@style/MyHealthTheme.Splash", MainLauncher = true, NoHistory = true,
		ScreenOrientation = global::Android.Content.PM.ScreenOrientation.Portrait)]
	public class LaunchActivity : Activity
	{
		private ISharedPreferences preferences;
		private ProgressDialog progressDialog;

		protected async override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Launch);

			//Get the shared Preferences
			preferences = PreferenceManager.GetDefaultSharedPreferences (this.ApplicationContext);

			progressDialog = new ProgressDialog (this);
			progressDialog.Indeterminate = true;
			progressDialog.SetProgressStyle(ProgressDialogStyle.Spinner);
			progressDialog.SetTitle ("Initializing, Please Wait...");
			progressDialog.SetMessage("Verifying Data...");
			progressDialog.SetCancelable(false);
			progressDialog.Show();

			await MyHealthDB.ServiceConsumer.CreateDatabase ();
			progressDialog.SetMessage("Checking Device Registration...");

			if (await MyHealthDB.ServiceConsumer.CheckRegisteredDevice ()) {
				bool isDeviceSynced = preferences.GetBoolean("applicationUpdated",false);
				if (!isDeviceSynced) {
					progressDialog.SetMessage("Synching Data With Server...");
					if (await SyncDevice ())
						ShowAcceptanceDialog ();
				} else {
					ShowAcceptanceDialog ();
				}
			} else {
				var connectivityManager = (ConnectivityManager)GetSystemService (ConnectivityService);
				var activeConnection = connectivityManager.ActiveNetworkInfo;

				if ((activeConnection != null) && activeConnection.IsConnected) {
					progressDialog.SetMessage("Registering Device...");

					if (await MyHealthDB.ServiceConsumer.RegisterDevice("Android", Android.OS.Build.VERSION.SdkInt.ToString())) {
						progressDialog.SetMessage("Synching Data With Server...");
						if(await SyncDevice())
							ShowAcceptanceDialog ();
					}
				} else {
					ShowConnectivityDialog ();
				}
			}

			if (progressDialog.IsShowing) {
				progressDialog.Dismiss ();
			}
		}
			
		protected override void OnPause()
		{
			base.OnPause ();
		}

		protected async Task<Boolean> SyncDevice ()
		{
			try {
				ISharedPreferencesEditor editor = preferences.Edit ();
				editor.PutBoolean ("applicationUpdated", false);
				editor.Apply ();
				//await MyHealthDB.ServiceConsumer.SyncDevice(DateTime.Now);
				await MyHealthDB.ServiceConsumer.SyncDevice ();

				editor.PutBoolean ("applicationUpdated", true);
				editor.PutString ("LastSyncDate", DateTime.Now.ToString ("dd-MMM-yyyy HH:mm:ss"));

				editor.Apply ();
			} catch (Exception ex) {
				Toast.MakeText (this, ex.ToString (), ToastLength.Long).Show ();
				return false;
			}
			return true;
		}

		//------------------------ Show Acceptance Dialog ----------------------//
		private void ShowAcceptanceDialog() {
			progressDialog.Dismiss ();

			bool isAccepted = preferences.GetBoolean("isAccepted",false);
			if (!isAccepted) {
				AlertDialog.Builder alert = new AlertDialog.Builder (this);
				alert.SetTitle ("Accept Terms of Use");
				//alert.SetMessage ("Read our terms and conditions");

				alert.SetCancelable (false);

				alert.SetPositiveButton ("Agree", (senderAlert, args) => {
					var accepted = preferences.Edit();
					accepted.PutBoolean("isAccepted",true);
					accepted.Commit();
					StartActivity (typeof(HomeActivity));
				});

				alert.SetNegativeButton ("Don't Agree", (senderAlert, args) => {
					System.Environment.Exit (0);
				});

				alert.SetNeutralButton ("Read Our T&Cs", (senderAlert, args) => {
					//var uri = "http://myhealthapp.ie/terms.html";
					//var intent = new Intent (Intent.ActionView, uri); 
					//this.StartActivity (intent);
					Intent intent = new Intent (Intent.ActionView, Android.Net.Uri.Parse ("http://www.rcsimyhealth.ie/terms-and-conditions.html"));
					StartActivity (intent);
					//StartActivity(type:UrlQuerySanitizer("www.google.com"));
					System.Environment.Exit (0);
				});

				//run the alert in UI thread to display in the screen
				RunOnUiThread (() => {
					alert.Show ();
				});
			} else {
				StartActivity (typeof(HomeActivity));
			}
		}
		private void ShowConnectivityDialog()
		{
			AlertDialog.Builder alert = new AlertDialog.Builder (this);
			alert.SetTitle ("Connectivity");
			alert.SetMessage ("Please check your internet connection and relaunch applicaiton");

			alert.SetCancelable (false);

			alert.SetPositiveButton ("Ok",(senderAlert, args) => {
				System.Environment.Exit (0);
			});

			//run the alert in UI thread to display in the screen
			RunOnUiThread (() => {
				progressDialog.Dismiss ();
				alert.Show ();
			});
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

