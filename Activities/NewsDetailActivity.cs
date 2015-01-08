﻿
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

			SetCustomActionBar ();

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
