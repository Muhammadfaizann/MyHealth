
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

namespace MyHealthAndroid
{
	[Activity (Label = "My Health", ScreenOrientation = global::Android.Content.PM.ScreenOrientation.Portrait)]			
	public class HelpActivity : Activity
	{
		private ListView _helpList;
		private Button _backButton;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			SetContentView (Resource.Layout.activity_help_data);

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
					case 3:
					newActivity = new Intent(this, typeof(MyProfileActivity));
						StartActivity(newActivity);
						break;

				}
			};

			// back button
			_backButton = FindViewById<Button> (Resource.Id.backButton);
			_backButton.Text = "I want to help";
			_backButton.Click += (object sender, EventArgs e) => 
			{
				base.OnBackPressed();
			};
		}
	}
}

