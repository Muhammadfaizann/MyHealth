﻿
using System;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using MyHealthAndroid;
using MyHealthDB;
using MyHealthDB.Logger;

namespace MyHealth.Android
{
	[Activity (Label = "MyHealth", ScreenOrientation = global::Android.Content.PM.ScreenOrientation.Portrait)]			
	public class HealthProfessionalActivity : Activity
	{
		private Button backButton;
		private ListView _professionalsList;
		//private CommonData _model;

		protected async override void OnCreate (Bundle bundle)
		{
			//_model = new CommonData ();

			base.OnCreate (bundle);
			SetContentView (Resource.Layout.activity_health_professionals);

			SetCustomActionBar ();

			_professionalsList = FindViewById<ListView> (Resource.Id.healthProfessionalsList);
			var _listAdapter = new HPCustomAdapter(this);
			_professionalsList.Adapter = _listAdapter;

			_professionalsList.ItemClick += (object sender, AdapterView.ItemClickEventArgs e) => {
				//var item = _model.getHealthProfessionals().ElementAt(e.Position);
				var _detialsActivity = new Intent (this, typeof(HPDetailsActivity));
				_detialsActivity.PutExtra("callerCellPosition", e.Position);
				StartActivity(_detialsActivity);
			};

			// back button
			backButton = FindViewById<Button> (Resource.Id.backButton);
			backButton.Text = "Health Services";
			backButton.Click += (object sender, EventArgs e) => 
			{
				base.OnBackPressed();
			};

			await LogManager.Log (new LogUsage { 
				Date = DateTime.Now, 
				Page = Convert.ToInt32(Pages.HealthProfessionals)
			});
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
			}
			return base.OnMenuItemSelected (featureId, item);
		}
	}
}

