
using System;
using System.Collections.Generic;
using System.Linq;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using Android.Webkit;
using Android.Graphics;

using MyHealthDB;
using MyHealthDB.Logger;
using Android.Content.PM;
using Android.Annotation;

namespace MyHealthAndroid
{
	[Activity (Label = "MyHealth" ,ScreenOrientation = global::Android.Content.PM.ScreenOrientation.Portrait)]			
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

			var _homeButton = FindViewById<TextView> (Resource.Id.txtAppTitle);
			_homeButton.MovementMethod = Android.Text.Method.LinkMovementMethod.Instance;
			_homeButton.Touch += delegate {
				var homeActivity = new Intent (this, typeof(HomeActivity));
				StartActivity (homeActivity);
			};

			if (_selectedDiseaseId > 0) {
				var selectedDisease = await DatabaseManager.SelectDisease (_selectedDiseaseId);
				_backButtonTitle = selectedDisease.Name;

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

			var shareButton = FindViewById<ImageButton> (Resource.Id.shareButton);
			shareButton.Click += (object sender, EventArgs e) => {
				/*Intent sharingIntent = new Intent(Android.Content.Intent.ActionSend);
				sharingIntent.SetType("text/plain");

				String shareBody = "I have just researched \"" + _backButtonTitle + "\" to find out more please download the MyHealth app from myhealthapp.ie";

				sharingIntent.PutExtra(Android.Content.Intent.ExtraSubject, "Subject Here");
				sharingIntent.PutExtra(Android.Content.Intent.ExtraText, shareBody);


				/*Boolean facebookAppFound = false;
				IList<ResolveInfo> matches = PackageManager.QueryIntentActivities(sharingIntent, 0);
				foreach (ResolveInfo info in matches) {
					if (info.ActivityInfo.PackageName.ToLower().StartsWith("com.facebook.katana")) {
						sharingIntent.SetPackage(info.ActivityInfo.PackageName);
						facebookAppFound = true;
						//break;
					}
				}***

				//StartActivity(Intent.CreateChooser(sharingIntent, "Share via"));*/

				String shareBody = "I have just researched \"" + _backButtonTitle + "\" to find out more please download the MyHealth app from http://www.rcsimyhealth.ie";
				String shareSubject = _backButtonTitle + " - MyHealth app";

				Intent emailIntent = new Intent();
				emailIntent.SetAction(Intent.ActionSend);
				// Native email client doesn't currently support HTML, but it doesn't hurt to try in case they fix it
				emailIntent.PutExtra(Intent.ExtraText, shareBody);
				emailIntent.PutExtra(Intent.ExtraSubject, shareSubject);
				emailIntent.SetType("message/rfc822");

				Intent sendIntent = new Intent(Intent.ActionSend);     
				sendIntent.SetType("text/plain");

				Intent openInChooser = Intent.CreateChooser(emailIntent, "Share via");

				var resInfo = PackageManager.QueryIntentActivities(sendIntent, 0);
				List<LabeledIntent> intentList = new List<LabeledIntent>();
				for (int i = 0; i < resInfo.Count; i++) {
					// Extract the label, append it, and repackage it in a LabeledIntent
					ResolveInfo ri = resInfo.ElementAt(i);
					String packageName = ri.ActivityInfo.PackageName;
					if(packageName.Contains("android.email")) {
						emailIntent.SetPackage(packageName);
					} else if(packageName.Contains("twitter") || packageName.Contains("facebook") || packageName.Contains("mms") || packageName.Contains("android.gm")) {
						Intent intent = new Intent();
						intent.SetComponent(new ComponentName(packageName, ri.ActivityInfo.Name));
						intent.SetAction(Intent.ActionSend);
						intent.SetType("text/plain");
						if(packageName.Contains("twitter")) {
							intent.PutExtra(Intent.ExtraText, shareBody);
						} else if(packageName.Contains("facebook")) {
							// Warning: Facebook IGNORES our text. They say "These fields are intended for users to express themselves. Pre-filling these fields erodes the authenticity of the user voice."
							// One workaround is to use the Facebook SDK to post, but that doesn't allow the user to choose how they want to share. We can also make a custom landing page, and the link
							// will show the <meta content ="..."> text from that page with our link in Facebook.
							intent.PutExtra(Intent.ExtraTitle, shareSubject);
							intent.PutExtra(Intent.ExtraSubject, shareSubject);
							intent.PutExtra(Android.Content.Intent.ExtraText, shareBody);
							//intent.PutExtra(Intent.ExtraText, "http://myhealthapp.ie");
							//intent.PutExtra(
						} else if(packageName.Contains("mms")) {
							intent.PutExtra(Intent.ExtraText, shareBody);
						} else if(packageName.Contains("android.gm")) { // If Gmail shows up twice, try removing this else-if clause and the reference to "android.gm" above
							intent.PutExtra(Intent.ExtraText, shareBody);
							intent.PutExtra(Intent.ExtraSubject, shareSubject);
							intent.SetType("message/rfc822");
						}

						intentList.Add(new LabeledIntent(intent, packageName, ri.LoadLabel(PackageManager), ri.Icon));
					}
				}

				// convert intentList to array
				LabeledIntent[] extraIntents = intentList.ToArray();	// new LabeledIntent[ intentList.Count ]);

				openInChooser.PutExtra(Intent.ExtraInitialIntents, extraIntents);
				StartActivity(openInChooser);
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

		//------------------------ menu item ----------------------//
		public override bool OnCreateOptionsMenu (IMenu menu)
		{
			MenuInflater.Inflate (Resource.Menu.main_activity_actions, menu);
			return base.OnCreateOptionsMenu (menu);
		}

		public override bool OnMenuItemSelected (int featureId, IMenuItem item)
		{
			switch (item.ItemId) {

			case Resource.Id.action_profile:
				var newActivity = new Intent(this, typeof(MyProfileActivity));
				StartActivity(newActivity);
				break;
			}
			return base.OnMenuItemSelected (featureId, item);
		}
	}

	public class MyHealthWebViewClient : WebViewClient
	{
		Activity _activity;
		public MyHealthWebViewClient (Activity activity)
		{
			_activity = activity;
		}

        public override bool ShouldOverrideUrlLoading(WebView view, string url)
        {
            Handle(url);

            base.ShouldOverrideUrlLoading(view, url);

            return true;
        }

        [TargetApi(Value = (int)BuildVersionCodes.N)]
        public override bool ShouldOverrideUrlLoading(WebView view, IWebResourceRequest request)
        {
            Handle(request.Url.Path);

            base.ShouldOverrideUrlLoading(view, request);

            return true;
        }

        private void Handle(string url)
        {
            var alert = new AlertDialog.Builder(_activity);
            alert.SetTitle("");
            alert.SetMessage("This link will take you to an external website, Do you want to Proceed?");

            alert.SetCancelable(false);

            alert.SetPositiveButton("OK", (senderAlert, args) => {
                LogManager.Log<LogExternalLink>(new LogExternalLink() { Date = DateTime.Now, Link = url });

                // launch another Activity that handles URLs
                Intent intent = new Intent(Intent.ActionView, Android.Net.Uri.Parse(url));
                _activity.StartActivity(intent);
            });

            alert.SetNegativeButton("Cancel", (senderAlert, args) => {
                //perform your own task for this conditional button click
            });
            //run the alert in UI thread to display in the screen
            _activity.RunOnUiThread(() => {
                alert.Show();
            });
        }
    }
}