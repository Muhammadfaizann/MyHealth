using System;
using Android.Widget;
using System.Collections.Generic;
using Android.App;
using Android.Views;
using Android.Content;
using MyHealthDB;
using System.Threading.Tasks;

namespace MyHealthAndroid
{
	public class NewsChannelsAdapter : BaseAdapter
	{
		public List<NewsChannels> ChannelList;
		private Activity _activity;
		private CommonData _model;

		//constructor
		public NewsChannelsAdapter (Activity activity)
		{
			_activity = activity;
			_model = new CommonData ();
			//ChannelList = _model.GetAllChannels ();
		}

		public async Task loadData () {
			ChannelList = await _model.GetAllChannels ();
		}

		//count of rows in ListView
		public override int Count {
			get { return ChannelList.Count; }
		}

		public override Java.Lang.Object GetItem (int position) {
			// could wrap a Contact in a Java.Lang.Object
			// to return it here if needed
			return null;
		}

		public override long GetItemId (int position) {
			return -1;
		}

		public override View GetView (int position, View convertView, ViewGroup parent)
		{
			var view = convertView ?? _activity.LayoutInflater.Inflate (
				Resource.Layout.row_news_table, parent, false);
			var contactName = view.FindViewById<TextView> (Resource.Id.newsChannelName);
			var contactImage = view.FindViewById<ImageView> (Resource.Id.newsChannelImage);

			contactName.Text = ChannelList [position].Name;
			contactImage.SetImageResource (ChannelList [position].resourceID);

			return view;
		}
	}
}

