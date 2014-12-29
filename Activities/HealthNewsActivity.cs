
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
using MyHealthAndroid;

namespace MyHealth.Android
{
	[Activity (Label = "My Health", ScreenOrientation = global::Android.Content.PM.ScreenOrientation.Portrait)]
	public class HealthNewsActivity : Activity
	{
		private Button backButton;
		private ListView _channelsList;
		private CommonData _model;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			_model = new CommonData ();

			base.OnCreate (bundle);
			SetContentView (Resource.Layout.activity_hp_details_table);

			_channelsList = FindViewById<ListView> (Resource.Id.emergencyList);
			var _listAdapter = new NewsChannelsAdapter(this);
			_channelsList.Adapter = _listAdapter;

			_channelsList.ItemClick += (object sender, AdapterView.ItemClickEventArgs e) => {
				var detailActivity = new Intent (this, typeof (NewsDetailActivity));
				detailActivity.PutExtra("ChannelName", "BBC News");
				StartActivity(detailActivity);
			};

			// back button
			backButton = FindViewById<Button> (Resource.Id.backButton);
			backButton.Text = "News";
			backButton.Click += (object sender, EventArgs e) => 
			{
				base.OnBackPressed();
			};
		}
	}
}

