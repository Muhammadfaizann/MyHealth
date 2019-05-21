
using System;

using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using MyHealthDB;
using MyHealthDB.Logger;

namespace MyHealthAndroid
{
	[Activity (Label = "MyHealth" ,ScreenOrientation = global::Android.Content.PM.ScreenOrientation.Portrait)]
	public class VideoLinksActivity : Activity
	{
		private ListView _commonListView;
		private Button _backButton;

		protected async override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

            var categoryId = Intent.GetIntExtra("CategoryId", -1);
            var categoryTitle = Intent.GetStringExtra("CategoryTitle");

            await SetLayoutWithTable(categoryId);

            //implement the back button 
            _backButton = FindViewById<Button>(Resource.Id.backButton);
            _backButton.Text = categoryTitle;
            _backButton.Click += (object sender, EventArgs e) =>
            {
                base.OnBackPressed();
            };

            SetCustomActionBar ();

			var _homeButton = FindViewById<TextView> (Resource.Id.txtAppTitle);
			_homeButton.MovementMethod = Android.Text.Method.LinkMovementMethod.Instance;
			_homeButton.Touch += delegate {
				var homeActivity = new Intent (this, typeof(HomeActivity));
                homeActivity.SetFlags(ActivityFlags.ClearTop | ActivityFlags.ClearTask | ActivityFlags.NewTask);
				StartActivity (homeActivity);
            };
		}

		//------------------------ custom activity ----------------------//
		public void SetCustomActionBar() 
		{
			ActionBar.SetDisplayShowHomeEnabled (false);
			ActionBar.SetDisplayShowTitleEnabled (false);
			ActionBar.SetCustomView (Resource.Layout.actionbar_custom);
			ActionBar.SetDisplayShowCustomEnabled (true);
		}
        
		private async Task SetLayoutWithTable(int categoryId)
		{
			SetContentView (Resource.Layout.activity_hp_details_table);
			_commonListView = FindViewById<ListView> (Resource.Id.emergencyList);

            var videoLinkAdapter = new HPVideoLinksAdapter(this);
            await videoLinkAdapter.loadData(categoryId);
            _commonListView.Adapter = videoLinkAdapter;
            await LogManager.Log(new LogUsage
            {
                Date = DateTime.Now,
                Page = Convert.ToInt32(Pages.MyHealthVideos)
            });
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
