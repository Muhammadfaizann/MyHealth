
using System;
using System.Linq;

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
	public class HealthNewsActivity : Activity
	{
		private Button backButton;
		private ListView _channelsList;

		protected async override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			base.OnCreate (bundle);
			SetContentView (Resource.Layout.activity_hp_details_table);

			SetCustomActionBar ();

			_channelsList = FindViewById<ListView> (Resource.Id.emergencyList);
			var _listAdapter = new NewsChannelsAdapter(this);
			await _listAdapter.loadData ();
			_channelsList.Adapter = _listAdapter;

			_channelsList.ItemClick += (object sender, AdapterView.ItemClickEventArgs e) => {
				var detailActivity = new Intent (this, typeof (NewsDetailActivity));
				detailActivity.PutExtra("ChannelName",_listAdapter.ChannelList.ElementAt(e.Position).Name);
				StartActivity(detailActivity);
			};

			// back button
			backButton = FindViewById<Button> (Resource.Id.backButton);
			backButton.Text = "Health News";
			backButton.Click += (object sender, EventArgs e) => 
			{
				base.OnBackPressed();
			};

			await LogManager.Log<LogUsage> (new LogUsage { 
				Date = DateTime.Now, 
				Page = Convert.ToInt32(Pages.HealthNews)
			});

			var _homeButton = FindViewById<TextView> (Resource.Id.txtAppTitle);
			_homeButton.MovementMethod = Android.Text.Method.LinkMovementMethod.Instance;
			_homeButton.Touch += delegate {
				var homeActivity = new Intent (this, typeof(HomeActivity));
                homeActivity.SetFlags(ActivityFlags.ClearTop | ActivityFlags.ClearTask | ActivityFlags.NewTask);
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

