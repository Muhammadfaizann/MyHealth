
using System;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;

namespace MyHealthAndroid
{
	[Activity (Label = "MyHealth")]			
	public class HospitalsInCountyActivity : Activity
	{
		//private CommonData _model;
		private ListView _commonListView;
		private Button _backButton;

		protected async override void OnCreate (Bundle bundle)
		{

			base.OnCreate (bundle);
			//_model = new CommonData ();

			SetContentView (Resource.Layout.activity_hp_details_table);
			_commonListView = FindViewById<ListView> (Resource.Id.emergencyList);

			var selectedProvinceId = Intent.GetIntExtra ("provinceId", -1);

			var hospitalAdapter = new HospitalsAdapter (this);
			await hospitalAdapter.loadData (selectedProvinceId);
			_commonListView.Adapter = hospitalAdapter;
			SetCustomActionBar ();

			//LogManager.Log<LogUsage> (new LogUsage (){ Date = DateTime.Now, Page = Convert.ToInt32(Pages.IWantToHelp).ToString() });

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

