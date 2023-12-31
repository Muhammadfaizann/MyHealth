﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Views;
using Android.Widget;
using MyHealthDB;
using MyHealthDB.Logger;

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
			//_list = _model.GetHospitalsInCounty (0);
		}

		public async Task loadData (int provinceId) {
			_list = await DatabaseManager.SelectHospitalsByProvince (provinceId);
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
			orgnisationNumber.Click -= onNumberClicked;
			orgnisationNumber.Click += onNumberClicked;

			orgnisationWebsite.Clickable = true;
			orgnisationWebsite.Click -= onWebsiteClicked;
			orgnisationWebsite.Click += onWebsiteClicked;

			return view;
		}

		private void onNumberClicked (object sender, EventArgs e) {
			var uri = Android.Net.Uri.Parse ("tel:" + ((TextView)sender).Text);
			var intent = new Intent (Intent.ActionView, uri); 
			_activity.StartActivity (intent); 
		}

		private void onWebsiteClicked (object sender, EventArgs e)
		{
			var alert = new AlertDialog.Builder (_activity);
			alert.SetTitle ("");
			alert.SetMessage ("This link will take you to an external website, Do you want to Proceed?");

			alert.SetCancelable (false);

			alert.SetPositiveButton ("OK",(senderAlert, args) => {
				//LogManager.Log<LogExternalLink> (new LogExternalLink (){ Date = DateTime.Now, Link = url });

			var uri = Android.Net.Uri.Parse (((TextView)sender).Text);
				var intent = new Intent (Intent.ActionView, uri); 
				_activity.StartActivity (intent); 
				LogManager.Log<LogExternalLink>( new LogExternalLink {
					Date = DateTime.Now, 
				Link = ((TextView)sender).Text
				});
			});

			alert.SetNegativeButton ("Cancel", (senderAlert, args) => {
				//perform your own task for this conditional button click
			});
			//run the alert in UI thread to display in the screen
			_activity.RunOnUiThread (() => {
				alert.Show ();
			});
			//var uri = Android.Net.Uri.Parse (orgnisationWebsite.Text);
			//var intent = new Intent (Intent.ActionView, uri); 
			//_activity.StartActivity (intent); 
			//await LogManager.Log<LogExternalLink> (new LogExternalLink {
			//	Date = DateTime.Now, 
			//	Link = orgnisationWebsite.Text
			//});
		}
	}
}
