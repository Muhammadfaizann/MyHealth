using Android.Widget;
using System.Collections.Generic;
using Android.App;
using Android.Views;
using MyHealthDB;
using System.Threading.Tasks;

namespace MyHealthAndroid
{
	public class MediaCategoriesAdapter : BaseAdapter<MediaCategory>
	{
		private List<MediaCategory> _list;
		private Activity _activity;

		public MediaCategoriesAdapter(Activity activity)
		{
			_activity = activity;
		}

		public Task loadData () => DatabaseManager
            .GetAllMediaCategoriesAsync()
            .ContinueWith(_ => _list = _.Result);
			
		//count of rows in ListView
		public override int Count => _list.Count;

        public override MediaCategory this[int position] => _list[position];

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
            var view = convertView ?? _activity.LayoutInflater.Inflate(
                Resource.Layout.row_news_table, parent, false);
            var contactName = view.FindViewById<TextView>(Resource.Id.newsChannelName);

            contactName.Text = _list[position].CategoryTitle;

            return view;
        }
	}
}

