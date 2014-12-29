
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
	public class BloodDonationActivity : Activity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			SetContentView (Resource.Layout.sub_activity_blood_donation);

			var Link = FindViewById (Resource.Id.bloodDonationLink);
			Link.Click += (object sender, EventArgs e) => {
				var uri = Android.Net.Uri.Parse ("http://www.giveblood.ie");
				var intent = new Intent (Intent.ActionView, uri); 
				StartActivity (intent);
			};

			// back button
			var _backButton = FindViewById<Button> (Resource.Id.backButton);
			_backButton.Text = "Blood Donation";
			_backButton.Click += (object sender, EventArgs e) => 
			{
				base.OnBackPressed();
			};
		}
	}
}

