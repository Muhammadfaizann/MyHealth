
using System;
using System.Collections.Generic;
using System.Linq;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using MyHealthAndroid;
using MyHealthDB;
using MyHealthDB.Logger;
using Android.Preferences;

namespace MyHealthAndroid
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
                var id = _listAdapter.GetItemId(e.Position);
				var _detialsActivity = new Intent (this, typeof(HPDetailsActivity));
				_detialsActivity.PutExtra("id", Convert.ToInt32(id));
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
			}
			return base.OnMenuItemSelected (featureId, item);
		}
	}
}

