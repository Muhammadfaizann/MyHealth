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

		private CommonData _model;

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

