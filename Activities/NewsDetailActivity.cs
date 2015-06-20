
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
	public enum RssFeedName { BBC, PULSE, IrishHealth, IrishTimesHealth  }

	[Activity (Label = "MyHealth", ScreenOrientation = global::Android.Content.PM.ScreenOrientation.Portrait)]
	public class NewsDetailActivity : Activity
	{
		private Button backButton;
		private ListView _channelsList;
		private NewsFeedAdapter _listAdapter = null;
		//private CommonData _model;

		protected override void OnCreate (Bundle bundle)
		{
		
			base.OnCreate (bundle);
			SetContentView (Resource.Layout.activity_hp_details_table);

			SetCustomActionBar ();

			_channelsList = FindViewById<ListView> (Resource.Id.emergencyList);
			string channelName = Intent.GetStringExtra ("ChannelName");

			if (channelName == "BBC Medical News") {
				_listAdapter = new NewsFeedAdapter (this, RssFeedName.BBC);
			}
			else if (channelName == "Pulse Latest") {
				_listAdapter = new NewsFeedAdapter (this, RssFeedName.PULSE);
			}
			else if (channelName == "Irish Health") {
				_listAdapter = new NewsFeedAdapter (this, RssFeedName.IrishHealth);
			}
			else if (channelName == "Irish Times Health") {
				_listAdapter = new NewsFeedAdapter (this, RssFeedName.IrishTimesHealth);
			}
			_channelsList.Adapter = _listAdapter;
			_channelsList.ItemClick += OnListItemClick;

			// back button
			backButton = FindViewById<Button> (Resource.Id.backButton);
			backButton.Text = channelName;
			backButton.Click += (object sender, EventArgs e) => 
			{
				base.OnBackPressed();
			};
		}

		void OnListItemClick(object sender, AdapterView.ItemClickEventArgs e)
		{
			var listView = sender as ListView;
			var item = this._listAdapter.ItemList[e.Position];
			AlertDialog.Builder alert = new AlertDialog.Builder (this);
			alert.SetTitle ("Launch Browser");
			alert.SetMessage ("You will now be directed to external website, Do you want to proceed.");

			alert.SetPositiveButton ("YES", (object senderAlert, DialogClickEventArgs Args) => {
				var uri = Android.Net.Uri.Parse (item.Link);
				var intent = new Intent (Intent.ActionView, uri); 
				this.StartActivity (intent); 
			});

			alert.SetNegativeButton ("NO", (object senderAlert, DialogClickEventArgs Args) => {

			});

			alert.Show ();
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

