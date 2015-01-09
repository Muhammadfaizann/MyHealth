using System;
using Android.Widget;
using System.Collections.Generic;
using Android.App;
using Android.Views;
using Android.Content;
using MyHealthDB;

namespace MyHealthAndroid
{
	public class HospitalsAdapter : BaseAdapter
	{
		private List<Hospital> _list;
		private Activity _activity;
		private CommonData _model;

		//constructor
		public HospitalsAdapter (Activity activity)
		{
			_activity = activity;
			_model = new CommonData ();
			_list = _model.GetHospitalsInCounty (0);
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
				Resource.Layout.row_hp_orgnisation, parent, false);
			var orgnisationName = view.FindViewById<TextView> (Resource.Id.orgnisationName);
			var orgnisationNumber = view.FindViewById<TextView> (Resource.Id.orgnisationNumber);
			var orgnisationWebsite = view.FindViewById<TextView> (Resource.Id.orgnisationWebsite);
			orgnisationName.Text = _list[position].Name;
			orgnisationNumber.Text = _list[position].PhoneNumber;
			orgnisationWebsite.Text = _list[position].URL;

			orgnisationNumber.Clickable = true;
			orgnisationNumber.Click += (object sender, EventArgs e) => {
				var uri = Android.Net.Uri.Parse ("tel:" + orgnisationNumber.Text);
				var intent = new Intent (Intent.ActionView, uri); 
				_activity.StartActivity (intent); 
			};

			orgnisationWebsite.Clickable = true;
			orgnisationWebsite.Click += (object sender, EventArgs e) => {
				var uri = Android.Net.Uri.Parse ("http://"+orgnisationWebsite.Text);
				var intent = new Intent (Intent.ActionView, uri); 
				_activity.StartActivity (intent); 
			};

			return view;
		}
	}
}

