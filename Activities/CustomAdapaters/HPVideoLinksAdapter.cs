using System;
using Android.Widget;
using System.Collections.Generic;
using Android.App;
using Android.Views;
using Android.Content;
using MyHealthDB;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace MyHealthAndroid
{
	public class HPVideoLinksAdapter : BaseAdapter
	{
		private List<VideoLink> _list;
		private Activity _activity;

		public HPVideoLinksAdapter(Activity activity)
		{
			_activity = activity;
		}

		public Task loadData () => DatabaseManager
            .GetAllVideoLinksAsync()
            .ContinueWith(_ => _list = _.Result);
			
		//count of rows in ListView
		public override int Count => _list.Count;

		public override Java.Lang.Object GetItem (int position) {
			// could wrap a Contact in a Java.Lang.Object
			// to return it here if needed
			return null;
		}

		public override long GetItemId (int position) {
			return _list[position].ID.Value;
		}

		public override View GetView (int position, View convertView, ViewGroup parent)
		{
			var view = convertView ?? _activity.LayoutInflater.Inflate (
				Resource.Layout.row_hp_emergency, parent, false);
            var item = _list[position];
			var txtTitle = view.FindViewById<TextView> (Resource.Id.emergencyName);
			var txtDescription = view.FindViewById<TextView> (Resource.Id.emergencyInfo);
			var txtUrl = view.FindViewById<TextView> (Resource.Id.emergencyNumber);
			txtTitle.Text = item.Title;
			txtDescription.Text = item.Description;
            txtUrl.Text = string.IsNullOrWhiteSpace(item.UrlDisplayName) ? item.Url : item.UrlDisplayName;

            txtUrl.Tag = position;
			txtUrl.Clickable = true;
			txtUrl.Click -= onNumberClicked;
			txtUrl.Click += onNumberClicked;
            
			return view;
		}

		private void onNumberClicked (object sender, EventArgs e) {
            var position = Convert.ToInt32(((TextView)sender).Tag);
            var url = _list[position].Url;

            if (url.StartsWith("https://", StringComparison.InvariantCultureIgnoreCase))
            {
                url = Regex.Replace(url, "https://", "https://", RegexOptions.IgnoreCase);
            }
            else if (url.StartsWith("http://", StringComparison.InvariantCultureIgnoreCase))
            {
                url = Regex.Replace(url, "http://", "http://", RegexOptions.IgnoreCase);
            }
            else if (url.StartsWith("ftp://", StringComparison.InvariantCultureIgnoreCase))
            {
                url = Regex.Replace(url, "ftp://", "ftp://", RegexOptions.IgnoreCase);
            }

            var uri = Android.Net.Uri.Parse(url);

			var intent = new Intent (Intent.ActionView, uri); 
			_activity.StartActivity (intent); 
		}
	}
}

