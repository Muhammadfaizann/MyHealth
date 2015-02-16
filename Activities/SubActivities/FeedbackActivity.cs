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

namespace MyHealthAndroid
{
	[Activity (Label = "My Health", ScreenOrientation = global::Android.Content.PM.ScreenOrientation.Portrait)]			
	public class FeedbackActivity : Activity
	{

		public EditText username;
		public EditText email;
		public EditText message;
		public Button saveFeedback;

		protected async override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			SetContentView (Resource.Layout.sub_activity_feedback);

			SetCustomActionBar ();

			username = FindViewById<EditText> (Resource.Id.feedbackUserName);
			email = FindViewById<EditText> (Resource.Id.feedbackUserEmail);
			message = FindViewById<EditText> (Resource.Id.feedbackUserMessage);
			saveFeedback = FindViewById<Button> (Resource.Id.saveFeedback);


			saveFeedback.Click += async (object sender, EventArgs e) => {
				if (username.Text.Equals ("") || email.Text.Equals ("") || message.Text.Equals ("")) {
					Toast.MakeText(this, "Please fill in all the feilds", ToastLength.Long).Show();
				} else {
					await LogManager.Log(new LogFeedback {
						Date = DateTime.Now,
						FeedbackText = string.Format("<name>{0}<name><email>{1}</email><message>{2}</message>", username.Text, email.Text, message.Text)
					});
					username.Text = email.Text = message.Text = "";
					Toast.MakeText(this, "Feedback Saved.!", ToastLength.Long).Show();
				}
			};

			await LogManager.Log<LogUsage> (new LogUsage { 
				Date = DateTime.Now, 
				Page = Convert.ToInt32(Pages.Feedback)
			});

			// back button
			var _backButton = FindViewById<Button> (Resource.Id.backButton);
			_backButton.Text = "Your Feedback";
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
}

