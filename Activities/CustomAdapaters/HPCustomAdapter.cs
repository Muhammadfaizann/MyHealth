using System;
using Android.Widget;
using System.Collections.Generic;
using Android.App;
using Android.Views;

namespace MyHealthAndroid
{
	public class HPCustomAdapter : BaseAdapter
	{
		private List<HPData> _list;
		private Activity _activity;
		private CommonData _model;

		//constructor
		public HPCustomAdapter (Activity activity)
		{
			_activity = activity;
			_model = new CommonData ();
			_list = _model.GetHealthProfessionals ();
		}
			
		//count of rows in ListView
		public override int Count {
			get { return _list.Count; }
		}

		public override Java.Lang.Object GetItem (int position) {
			// could wrap a Contact in a Java.Lang.Object
			// to return it here if needed
			return _list[position];
		}

		public override long GetItemId (int position) {
			return _list [position].Id;
		}

		public override View GetView (int position, View convertView, ViewGroup parent)
		{
			var view = convertView ?? _activity.LayoutInflater.Inflate (
				Resource.Layout.row_health_professionals_table, parent, false);
			var contactName = view.FindViewById<TextView> (Resource.Id.hpDisplayName);
			var contactImage = view.FindViewById<ImageView> (Resource.Id.hpImageView);
			contactName.Text = _list [position].DisplayName;
			contactImage.SetImageResource (_list [position].DisplayIcon);
//			if (_contactList [position].PhotoId == null) {
//				contactImage = view.FindViewById<ImageView> (Resource.Id.ContactImage);
//				contactImage.SetImageResource (Resource.Drawable.contactImage);
//			}  
			return view;
		}
	}
}

