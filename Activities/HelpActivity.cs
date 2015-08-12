
using System;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;

using MyHealthDB;
using MyHealthDB.Logger;

namespace MyHealthAndroid
{
	[Activity (Label = "MyHealth", ScreenOrientation = global::Android.Content.PM.ScreenOrientation.Portrait)]			
	public class HelpActivity : Activity
	{
		private ListView _helpList;
		private Button _backButton;

		protected async override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			SetContentView (Resource.Layout.activity_help_data);
			SetCustomActionBar ();

			_helpList = FindViewById<ListView> (Resource.Id.helpDataList);
			var _listAdapter = new HelpDataAdapter(this);
			_helpList.Adapter = _listAdapter;

			_helpList.ItemClick += (object sender, AdapterView.ItemClickEventArgs e) => {
				Intent newActivity;
				switch (e.Position) {
					case 0:
						newActivity = new Intent(this, typeof(BloodDonationActivity));
						StartActivity(newActivity);
						break;
					case 1:
					newActivity = new Intent(this, typeof(OrganDonationActivity));
						StartActivity(newActivity);
						break;
					case 2:
					newActivity = new Intent(this, typeof(FeedbackActivity));
						StartActivity(newActivity);
						break;
					//case 3:
					//newActivity = new Intent(this, typeof(MyProfileActivity));
						//StartActivity(newActivity);
						//break;
				}
			};

			await LogManager.Log (new LogUsage {
				Date = DateTime.Now, 
				Page = Convert.ToInt32(Pages.IWantToHelp)
			});

			// back button
			_backButton = FindViewById<Button> (Resource.Id.backButton);
			_backButton.Text = "I want to help";
			_backButton.Click += (object sender, EventArgs e) => 
			{
				base.OnBackPressed();
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
				StartActivity(newActivity);	break;
			}
			return base.OnMenuItemSelected (featureId, item);
		}
	}
}

