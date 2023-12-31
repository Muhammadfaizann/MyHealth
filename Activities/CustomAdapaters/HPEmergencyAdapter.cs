﻿using System;
using Android.Widget;
using System.Collections.Generic;
using Android.App;
using Android.Views;
using Android.Content;
using MyHealthDB;
using System.Threading.Tasks;

namespace MyHealthAndroid
{
	public class HPEmergencyAdapter : BaseAdapter
	{
		private List<EmergencyContacts> _list;
		private Activity _activity;
		private CommonData _model;

		//constructor
		public HPEmergencyAdapter (Activity activity)
		{
			_activity = activity;
			_model = new CommonData ();
		}

		public Task loadData () {
			return _model
                .GetEmergencyContacts ()
                .ContinueWith(_ => _list = _.Result);
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
			return _list[position].ID.Value;
		}

		public override View GetView (int position, View convertView, ViewGroup parent)
		{
			var view = convertView ?? _activity.LayoutInflater.Inflate (
				Resource.Layout.row_hp_emergency, parent, false);
			var emergencyName = view.FindViewById<TextView> (Resource.Id.emergencyName);
			var emergencyInfo = view.FindViewById<TextView> (Resource.Id.emergencyInfo);
			var emergencyNumber = view.FindViewById<TextView> (Resource.Id.emergencyNumber);
			emergencyName.Text = _list [position].Name;  //_list[position , 0];
			emergencyInfo.Text = _list [position].Description; //_list[position, 1];
			emergencyNumber.Text = _list [position].PhoneNumber.ToString(); //_list[position , 2];

			emergencyNumber.Clickable = true;
			emergencyNumber.Click -= onNumberClicked;
			emergencyNumber.Click += onNumberClicked;

//			if (_contactList [position].PhotoId == null) {
//				contactImage = view.FindViewById<ImageView> (Resource.Id.ContactImage);
//				contactImage.SetImageResource (Resource.Drawable.contactImage);
//			}  
			return view;
		}

		private void onNumberClicked (object sender, EventArgs e) {
			var uri = Android.Net.Uri.Parse ("tel:" + ((TextView)sender).Text);
			var intent = new Intent (Intent.ActionView, uri); 
			_activity.StartActivity (intent); 
		}
	}
}

