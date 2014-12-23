
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
using MyHealth.Android;

namespace MyHealthAndroid
{
	[Activity (Label = "My Health" ,ScreenOrientation = global::Android.Content.PM.ScreenOrientation.Portrait)]			
	public class HomeActivity : Activity
	{
		private ImageButton searchBtn;
		private ImageButton healthProBtn;
		private ImageButton healthNewsBtn;
		private ImageButton helpBtn;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			SetContentView (Resource.Layout.Home);

			searchBtn = FindViewById<ImageButton> (Resource.Id.healthSearchBtn);
			healthProBtn = FindViewById<ImageButton> (Resource.Id.healthProfessionalBtn);
			healthNewsBtn = FindViewById<ImageButton> (Resource.Id.healthNewsBtn);
			helpBtn = FindViewById<ImageButton> (Resource.Id.helpBtn);


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
				var activity = new Intent(this, typeof(HealthSearchActivity));
				StartActivity( activity);
			};
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
				
				break;
			}
			return base.OnMenuItemSelected (featureId, item);
		}
	}
}

