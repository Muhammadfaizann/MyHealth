
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
	public class NewsDetailActivity : Activity
	{
		private Button backButton;
		private ListView _channelsList;
		//private CommonData _model;

		protected override void OnCreate (Bundle bundle)
		{
		
			base.OnCreate (bundle);
			SetContentView (Resource.Layout.activity_hp_details_table);

			_channelsList = FindViewById<ListView> (Resource.Id.emergencyList);
			var _listAdapter = new NewsFeedAdapter(this);
			_channelsList.Adapter = _listAdapter;

			// back button
			backButton = FindViewById<Button> (Resource.Id.backButton);
			backButton.Text = Intent.GetStringExtra("ChannelName");
			backButton.Click += (object sender, EventArgs e) => 
			{
				base.OnBackPressed();
			};
		}
	}
}

