
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
using Android.Webkit;

using MyHealthDB;
using MyHealthDB.Logger;

namespace MyHealthAndroid
{
	[Activity (Label = "My Health")]			
	public class DiseaseDetailActivity : Activity
	{
		private Button _backButton;
		private ImageView _imageView;
		private WebView _webView;

		//data from preceeding activity
		private String _backButtonTitle = "";
		private String _selectedDiseases = "";

		protected override async void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			SetContentView (Resource.Layout.activity_disease_details);

			SetCustomActionBar ();

			_imageView = FindViewById<ImageView> (Resource.Id.diseaseDetailImage);
			_webView = FindViewById<WebView> (Resource.Id.diseaseDetailWebView);

			_selectedDiseases = Intent.GetStringExtra ("diseaseName");
			var _selectedDiseaseId = Convert.ToInt32(Intent.GetStringExtra ("diseaseId"));

			if (_selectedDiseaseId > 0) {
				var selectedDisease = await DatabaseManager.SelectDisease (_selectedDiseaseId);
				await LogManager.Log<LogContent> (new LogContent () {
					Date = DateTime.Now,
					ConditionId = selectedDisease.ID,
					CategoryId = selectedDisease.DiseaseCategoryID
				});
			}

			if (_selectedDiseases.Contains ("Heart") || _selectedDiseases.Contains ("heart")) {
				_imageView.SetImageResource (Resource.Drawable.ihf);
				_webView.LoadUrl("file:///android_asset/Content/Heart.html");
			} else {
				_imageView.SetImageResource (Resource.Drawable.Cancer);
				_webView.LoadUrl("file:///android_asset/Content/LungCancer.html");
			}

			_backButton = FindViewById<Button> (Resource.Id.backButton);
			_backButton.Text = _backButtonTitle;
			_backButton.Click += (object sender, EventArgs e) => 
			{
				base.OnBackPressed();
			};

			_webView.SetWebViewClient(new MyHealthWebViewClient(this));
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

	public class MyHealthWebViewClient : WebViewClient
	{
		Activity _activity;
		public MyHealthWebViewClient (Activity activity)
		{
			_activity = activity;
		}

		public override bool ShouldOverrideUrlLoading (WebView view, string url)
		{
			LogManager.Log<LogExternalLink> (new LogExternalLink (){ Date = DateTime.Now, Link = url });

			// launch another Activity that handles URLs
			Intent intent = new Intent (Intent.ActionView, Android.Net.Uri.Parse (url));
			_activity.StartActivity (intent);

			base.ShouldOverrideUrlLoading (view, url);
			return true;
		}
	}
}

