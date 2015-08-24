
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
using Android.Webkit;

using MyHealthDB;
using MyHealthDB.Logger;

namespace MyHealthAndroid
{
	[Activity (Label = "MyHealth", ScreenOrientation = global::Android.Content.PM.ScreenOrientation.Portrait)]			
	public class OrganDonationActivity : Activity
	{
		protected async override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			SetContentView (Resource.Layout.sub_activity_organ_donation);

			SetCustomActionBar ();

			await LogManager.Log<LogUsage> (new LogUsage { 
				Date = DateTime.Now, 
				Page = Convert.ToInt32(Pages.OrganDonors)
			});

			var webView = FindViewById<WebView> (Resource.Id.organDonationWebView);
			webView.LoadUrl("file:///android_asset/Content/OrganDonor.html");

			// back button
			var _backButton = FindViewById<Button> (Resource.Id.backButton);
			_backButton.Text = "Organ Donation";
			_backButton.Click += (object sender, EventArgs e) => 
			{
				base.OnBackPressed();
			};
			var _homeButton = FindViewById<TextView> (Resource.Id.txtAppTitle);
			_homeButton.MovementMethod = Android.Text.Method.LinkMovementMethod.Instance;
			_homeButton.Touch += delegate {
				var homeActivity = new Intent (this, typeof(HomeActivity));
				StartActivity (homeActivity);
			};

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
			//case Resource.Id.txtAppTitle:
			//	var homeActivity = new Intent(this, typeof(HomeActivity));
			//	StartActivity(homeActivity);
			//	break;
			}
			return base.OnMenuItemSelected (featureId, item);
		}
	}
}

