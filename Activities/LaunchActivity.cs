﻿using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Xamarin;
using Android.Telephony;
using Java.Util;
using MyHealthDB.Logger;
using MyHealthDB;
using Android.Net;
using Android.Preferences;
using System.Threading.Tasks;

namespace MyHealthAndroid
{
	[Activity(Theme = "@style/Theme.Splash", MainLauncher = true, NoHistory = true,
		ScreenOrientation = global::Android.Content.PM.ScreenOrientation.Portrait)]
	public class LaunchActivity : Activity
	{
		private ISharedPreferences preferences;

		protected async override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Launch);

			//Get the shared Preferences
			preferences = PreferenceManager.GetDefaultSharedPreferences (this.ApplicationContext); 



			await MyHealthDB.ServiceConsumer.CreateDatabase ();
			if (await MyHealthDB.ServiceConsumer.CheckRegisteredDevice ()) {
				bool isDeviceSynced = preferences.GetBoolean("applicationUpdated",false);
				if (!isDeviceSynced) {
					if (await SyncDevice ())
						ShowAcceptanceDialog ();
				} else {
									
					ShowAcceptanceDialog ();
				}
			} else {
				var connectivityManager = (ConnectivityManager)GetSystemService (ConnectivityService);
				var activeConnection = connectivityManager.ActiveNetworkInfo;

				if ((activeConnection != null) && activeConnection.IsConnected) {
					Toast.MakeText (this, "Registering device", ToastLength.Long).Show();
					if (await MyHealthDB.ServiceConsumer.RegisterDevice("Android")) {
						if(await SyncDevice())
							ShowAcceptanceDialog ();
					}
				} else {
					ShowConnectivityDialog ();
				}
			}
			}
			
		protected override void OnPause()
		{
			base.OnPause ();
		}

		protected async Task<Boolean> SyncDevice (bool ShowMessage=true)
		{
			try {
				ISharedPreferencesEditor editor = preferences.Edit();
				if(ShowMessage){
					Toast.MakeText(this, "Initializing Application for the first time, Please wait.", ToastLength.Long).Show();
				}
				editor.PutBoolean("applicationUpdated", false);
				editor.Apply();
				await MyHealthDB.ServiceConsumer.SyncDevice();
				if(ShowMessage)
				{
					Toast.MakeText(this, "Application Initilaized Successfully.", ToastLength.Long).Show();
				}
				editor.PutBoolean("applicationUpdated", true);
				editor.PutString("LastSyncDate", DateTime.Now.ToString("dd-MMM-yyyy HH:mm:ss"));

				editor.Apply();
			} catch (Exception ex) {
				Toast.MakeText(this, ex.ToString(), ToastLength.Long).Show();
				return false;
			}
			return true;
		}

		//------------------------ Show Acceptance Dialog ----------------------//
		private void ShowAcceptanceDialog() {
			AlertDialog.Builder alert = new AlertDialog.Builder (this);
			alert.SetTitle ("Accept Terms of Use");
			alert.SetMessage ("Read our terms and conditions");

			alert.SetCancelable (false);

			alert.SetPositiveButton ("Agree",(senderAlert, args) => {
				StartActivity (typeof(HomeActivity));
			});

			alert.SetNegativeButton ("Don't Agree", (senderAlert, args) => {
				System.Environment.Exit (0);
			});
			//run the alert in UI thread to display in the screen
			RunOnUiThread (() => {
				alert.Show ();
			});
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

