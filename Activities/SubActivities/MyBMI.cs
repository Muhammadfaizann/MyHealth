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

using MyHealthDB;
using MyHealthDB.Logger;
using Android.Preferences;

namespace MyHealthAndroid
{
	[Activity (Label = "MyHealth", ScreenOrientation = global::Android.Content.PM.ScreenOrientation.Portrait)]			
	public class MyBMI : Activity
	{
		protected override void OnCreate (Bundle bundle)
		{
		
			base.OnCreate (bundle);
			SetContentView (Resource.Layout.sub_activity_mybmi);

			var bmi = Intent.GetStringExtra ("MyBMI");
			AlertDialog.Builder alert = new AlertDialog.Builder(this);
			alert.SetTitle("BMI Calculation");
			alert.SetMessage("your BMI is " + bmi);

			alert.SetPositiveButton ("Ok", (object senderAlert, DialogClickEventArgs Args) => {

			});

			alert.Show();
			// back button
			var _backButton = FindViewById<Button> (Resource.Id.backButton);
			_backButton.Text = "My BMI";
			_backButton.Click += (object sender, EventArgs e) => 
			{
				base.OnBackPressed();
			};
		}

		//------------------------ custom activity ----------------------//
		public void SetCustomActionBar () 
		{
			ActionBar.SetDisplayShowHomeEnabled (false);
			ActionBar.SetDisplayShowTitleEnabled (false);
			ActionBar.SetCustomView (Resource.Layout.actionbar_custom);
			ActionBar.SetDisplayShowCustomEnabled (true);
		}
	}
}

