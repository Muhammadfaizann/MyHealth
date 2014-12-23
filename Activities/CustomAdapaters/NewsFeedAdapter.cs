using System;
using Android.Widget;
using System.Collections.Generic;
using Android.App;
using Android.Views;
using Android.Content;
using Android.Graphics;
using System.Net;

namespace MyHealthAndroid
{
	public class NewsFeedAdapter : BaseAdapter
	{
		private List<FeedItem> _list;
		private Activity _activity;

		//constructor
		public NewsFeedAdapter (Activity activity)
		{
			_activity = activity;
			_list = RSSFeedReader.ReadBBCRSSFeed ("http://feeds.bbci.co.uk/news/health/rss.xml?edition=uk#");
		}
			
		//count of rows in ListView
		public override int Count {
			get { return _list.Count; }
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

			NewsDate.Text = _list [position].PubDate.ToString();
			Headline.Text = _list [position].Title;
			var imageBitmap = GetImageBitmapFromUrl (_list[position].ImageUrl);
			NewsImage.SetImageBitmap (imageBitmap);
			ActualNews.Text = _list [position].Description;

			NewsImage.Clickable = true;
			NewsImage.Click += (object sender, EventArgs e) => 
			{
				AlertDialog.Builder alert = new AlertDialog.Builder(_activity);
				alert.SetTitle("Launch Browser");
				alert.SetMessage("You will now be directed to external website, Do you want to proceed.");

				alert.SetPositiveButton ("YES", (object senderAlert, DialogClickEventArgs Args) => {
					var uri = Android.Net.Uri.Parse ( _list[position].Link);
					var intent = new Intent (Intent.ActionView, uri); 
					_activity.StartActivity (intent); 
				});

				alert.SetNegativeButton ("NO", (object senderAlert, DialogClickEventArgs Args) => {

				});

				alert.Show();
			};

			return view;
		}

		private Bitmap GetImageBitmapFromUrl(string url) 
		{
			Bitmap imageBitmap = null;
			using (var webClient = new WebClient ()) {
				var imageBytes = webClient.DownloadData (url);
				if (imageBytes != null && imageBytes.Length > 0) {
					imageBitmap = BitmapFactory.DecodeByteArray (imageBytes, 0, imageBytes.Length);
				}
			}
			return imageBitmap;
		}
	}
}

