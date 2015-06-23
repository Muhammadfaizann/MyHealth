using System;

using Android.App;
using Android.Content;
using Android.OS;

using Android.Widget;
using Android.Views;

namespace MyHealthAndroid
{
	[Activity (Label = "MyHealth", ScreenOrientation = global::Android.Content.PM.ScreenOrientation.Portrait)]			
	public class MyBMI : Activity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			SetContentView (Resource.Layout.sub_activity_mybmi);

			SetCustomActionBar ();

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

			var txtAppTitle = ActionBar.CustomView.FindViewById (Resource.Id.txtAppTitle);
			if (txtAppTitle.LayoutParameters is ViewGroup.MarginLayoutParams) {
				ViewGroup.MarginLayoutParams p = (ViewGroup.MarginLayoutParams) txtAppTitle.LayoutParameters;
				p.RightMargin = 45;
				txtAppTitle.RequestLayout();
			}
		}
	}
}