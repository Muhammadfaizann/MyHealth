using System;
using Android.Widget;
using System.Collections.Generic;
using Android.App;
using Android.Views;
using Android.Content;
using Android.Graphics;
using System.Net;
using Android.Text;
using Android.OS;

namespace MyHealthAndroid
{
	public class NewsFeedAdapter : BaseAdapter
	{
		private List<FeedItem> _list;
		private Activity _activity;
		private RssFeedName _feed;

		//constructor
		public NewsFeedAdapter (Activity activity, RssFeedName feed)
		{
			_activity = activity;
			_feed = feed;

			string feedUrl;
			switch (feed) 
			{
				case RssFeedName.BBC:
					feedUrl = "http://feeds.bbci.co.uk/news/health/rss.xml?edition=uk#";
					break;
				case RssFeedName.IrishHealth:
					feedUrl = "http://www.irishhealth.com/rss/ihfeed.php";
					break;
				case RssFeedName.IrishTimesHealth:
					feedUrl = "https://www.irishtimes.com/cmlink/irish-times-health-1.1364620";
					break;
				case RssFeedName.PULSE:
					feedUrl = "http://pulsevoices.org/index.php?format=feed&type=rss&title=Pulse-Voices%20from%20the%20Heart%20of%20Medicine%20-%20Welcome%20to%20Pulse-Voices%20from%20the%20Heart%20of%20Medicine";
					break;
				default:
					feedUrl = "http://feeds.bbci.co.uk/news/health/rss.xml?edition=uk#";
					break;
			}

			_list = RSSFeedReader.Read (feedUrl);
		}
			
		//count of rows in ListView
		public override int Count {
			get { return _list.Count; }
		}

		public List<FeedItem> ItemList
		{
			get { return _list; }
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
				Resource.Layout.row_news_details_table, parent, false);
		
			var NewsDate = view.FindViewById<TextView> (Resource.Id.newsDate);
			var Headline = view.FindViewById<TextView> (Resource.Id.newsHeadline);
			var NewsImage = view.FindViewById<ImageView> (Resource.Id.newsImageView);
			var ActualNews = view.FindViewById<TextView> (Resource.Id.actualNews);



			NewsDate.Text =  (_list [position].PubDate == null || _list [position].PubDate == DateTime.MinValue) ? "" : _list [position].PubDate.ToString();
			Headline.Text = _list [position].Title;
			var imageBitmap = GetImageBitmapFromUrl (_list[position].ImageUrl);
			NewsImage.SetImageBitmap (imageBitmap);
			if (_feed == RssFeedName.PULSE)
            {
                var htmlString = _list[position].Description;
                ISpanned html;
                if (Build.VERSION.SdkInt >= BuildVersionCodes.N)
                {
                    html = Html.FromHtml(htmlString, Android.Text.FromHtmlOptions.ModeLegacy);
                }
                else
                {
                    html = Html.FromHtml(htmlString);
                }

                ActualNews.SetText (html, TextView.BufferType.Spannable);
			} else {
				ActualNews.Text = _list [position].Description;
			}

			return view;
		}

		private Bitmap GetImageBitmapFromUrl(string url) 
		{
			Bitmap imageBitmap = null;
			if (url != null) {
				using (var webClient = new WebClient ()) {
					var imageBytes = webClient.DownloadData (url);
					if (imageBytes != null && imageBytes.Length > 0) {
						imageBitmap = BitmapFactory.DecodeByteArray (imageBytes, 0, imageBytes.Length);
					}
				}
			}
			return imageBitmap;
		}
	}
}

