
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
using Android.Graphics;

using MyHealthDB;
using MyHealthDB.Logger;
using System.Threading.Tasks;

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

			var _selectedDiseaseId = Intent.GetIntExtra ("diseaseId", -1);

			if (_selectedDiseaseId > 0) {
				var selectedDisease = await DatabaseManager.SelectDisease (_selectedDiseaseId);
				this.Title = selectedDisease.Name;

				await LogManager.Log<LogContent> (new LogContent () {
					Date = DateTime.Now,
					ConditionId = selectedDisease.ID,
					CategoryId = selectedDisease.DiseaseCategoryID
				});

				var selectedCpUser = await DatabaseManager.SelectCpUser (selectedDisease.CPUserId);

				if (selectedCpUser != null) {
						// Convert bytes data into a Bitmap
						try {
							if(selectedCpUser.CharityLogo.Length > 0) {
								Bitmap bmp = BitmapFactory.DecodeByteArray(selectedCpUser.CharityLogo, 0, selectedCpUser.CharityLogo.Length);
								_imageView.SetImageBitmap(bmp);
							}
						} catch (Exception ex) {
							Console.WriteLine ("Image Load Exception : {0}", ex.ToString());
						}
						//ImageView imageView = new ImageView(ConversationsActivity.this);
						// Set the Bitmap data to the ImageView
				}
			}

			var htmlString = await MyHealthDB.Helper.Helper.BuildHtmlForDisease (_selectedDiseaseId);
			if (!string.IsNullOrEmpty(htmlString)) {
				//_imageView.SetImageResource (Resource.Drawable.Cancer);
				_webView.LoadDataWithBaseURL ("file:///android_asset/", htmlString, "text/html", "utf-8", null);
			} else if (_selectedDiseases.Contains ("Heart") || _selectedDiseases.Contains ("heart")) {
				_imageView.SetImageResource (Resource.Drawable.ihf);
				_webView.LoadUrl ("file:///android_asset/Content/Heart.html");
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

