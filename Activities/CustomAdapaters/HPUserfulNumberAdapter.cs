using System;
using Android.Widget;
using System.Collections.Generic;
using Android.App;
using Android.Views;
using Android.Content;

namespace MyHealthAndroid
{
	public class HPUserfulNumberAdapter : BaseAdapter
	{
		private List<MyUsefulNumbers> _list;
		private HPDetailsActivity _activity;

		//constructor
		public HPUserfulNumberAdapter (HPDetailsActivity activity)
		{
			_activity = activity;
			_list = DataService.LoadNumbers ();
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
				Resource.Layout.row_hp_useful_numbers, parent, false);
			var contactName = view.FindViewById<TextView> (Resource.Id.hpContactName);
			var contactEdit = view.FindViewById<ImageView> (Resource.Id.hpEditContactImage);
			var contactNumber = view.FindViewById<TextView> (Resource.Id.hpContactNumber);

			contactName.Text = _list[position].Title;
			contactNumber.Text = _list [position].Number;

			contactEdit.Clickable = true;
			contactEdit.Click += (object sender, EventArgs e) => {
				_activity.ShowInputDialog(position);
			};

			return view;
		}
	}
}

