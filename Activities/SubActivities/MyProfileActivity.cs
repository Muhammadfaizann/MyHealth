
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace MyHealthAndroid
{
	[Activity (Label = "My Health", ScreenOrientation = global::Android.Content.PM.ScreenOrientation.Portrait)]			
	public class MyProfileActivity : Activity
	{
		private CommonData _data;
		private Spinner heightFeetSpinner;
		private Spinner heightInchSpinner;

		protected override void OnCreate (Bundle bundle)
		{
			_data = new CommonData ();
			var Feets = _data.GetFeetData ();
			var Inches = _data.GetInchData ();

			base.OnCreate (bundle);
			SetContentView (Resource.Layout.sub_activity_profile);
				
			heightFeetSpinner = FindViewById<Spinner> (Resource.Id.heightFeetSpinner);
			heightFeetSpinner.Adapter = new ArrayAdapter<String> (this, Android.Resource.Layout.SimpleSpinnerDropDownItem, Feets);
			heightFeetSpinner.ItemSelected += (object sender, AdapterView.ItemSelectedEventArgs e) => {
				var F = Feets[e.Position];
			};

			heightInchSpinner = FindViewById<Spinner> (Resource.Id.heightInchSpinner);
			heightInchSpinner.Adapter = new ArrayAdapter<String> (this, Android.Resource.Layout.SimpleSpinnerDropDownItem, Inches);
			heightInchSpinner.ItemSelected += (object sender, AdapterView.ItemSelectedEventArgs e) => {
				var I = Inches[e.Position];
			};

			// back button
			var _backButton = FindViewById<Button> (Resource.Id.backButton);
			_backButton.Text = "My Profile";
			_backButton.Click += (object sender, EventArgs e) => 
			{
				base.OnBackPressed();
			};
		}
	}
}

