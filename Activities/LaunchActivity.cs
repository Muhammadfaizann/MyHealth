using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace MyHealthAndroid
{
	[Activity(Theme = "@style/Theme.Splash", MainLauncher = true, NoHistory = true,
		ScreenOrientation = global::Android.Content.PM.ScreenOrientation.Portrait)]
	public class LaunchActivity : Activity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Launch);

			AlertDialog.Builder alert = new AlertDialog.Builder (this);
			alert.SetTitle("Accept Terms of Use");
			alert.SetMessage("Read our terms and conditions");

			alert.SetCancelable (false);
			
			alert.SetPositiveButton ("Agree", (senderAlert, args) => {
				StartActivity(typeof(HomeActivity));
			} );

			alert.SetNegativeButton ("Don't Agree", (senderAlert, args) => {
				System.Environment.Exit(0);
			} );
			//run the alert in UI thread to display in the screen
			RunOnUiThread (() => {
				alert.Show();
			} );
		}
	}
}

