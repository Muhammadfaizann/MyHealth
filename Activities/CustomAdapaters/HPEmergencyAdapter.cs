using System;
using Android.Widget;
using System.Collections.Generic;
using Android.App;
using Android.Views;
using Android.Content;

namespace MyHealthAndroid
{
	public class HPEmergencyAdapter : BaseAdapter
	{
		private String[,] _list;
		private Activity _activity;
		private CommonData _model;

		//constructor
		public HPEmergencyAdapter (Activity activity)
		{
			_activity = activity;
			_model = new CommonData ();
			_list = _model.GetEmergencyContacts ();
		}
			
		//count of rows in ListView
		public override int Count {
			get { return _list.GetLength(0); }
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
				Resource.Layout.row_hp_emergency, parent, false);
			var emergencyName = view.FindViewById<TextView> (Resource.Id.emergencyName);
			var emergencyInfo = view.FindViewById<TextView> (Resource.Id.emergencyInfo);
			var emergencyNumber = view.FindViewById<TextView> (Resource.Id.emergencyNumber);
			emergencyName.Text = _list[position , 0];
			emergencyInfo.Text = _list[position, 1];
			emergencyNumber.Text = _list[position , 2];

			emergencyNumber.Clickable = true;
			emergencyNumber.Click += (object sender, EventArgs e) => {
				var uri = Android.Net.Uri.Parse ("tel:" + emergencyNumber.Text);
				var intent = new Intent (Intent.ActionView, uri); 
				_activity.StartActivity (intent); 
			};

//			if (_contactList [position].PhotoId == null) {
//				contactImage = view.FindViewById<ImageView> (Resource.Id.ContactImage);
//				contactImage.SetImageResource (Resource.Drawable.contactImage);
//			}  
			return view;
		}
	}
}

