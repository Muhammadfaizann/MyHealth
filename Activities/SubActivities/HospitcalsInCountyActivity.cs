
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

namespace MyHealthAndroid
{
	[Activity (Label = "My Health")]			
	public class HospitalsInCountyActivity : Activity
	{

		private CommonData _model;
		private ListView _commonListView;
		private Button _backButton;


		protected override void OnCreate (Bundle bundle)
		{

			base.OnCreate (bundle);
			_model = new CommonData ();

			SetContentView (Resource.Layout.activity_hp_details_table);
			_commonListView = FindViewById<ListView> (Resource.Id.emergencyList);
			_commonListView.Adapter = new HospitalsAdapter (this);

			SetCustomActionBar ();

			//implement the back button 
			_backButton = FindViewById<Button> (Resource.Id.backButton);
			_backButton.Text = "Hospitals";
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
			

		//-------------------------------------- Private functions --------------------------------------//

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

