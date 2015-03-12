
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
using System.Threading.Tasks;
using MyHealthDB.Logger;
using MyHealthDB.Helper;
using Android.Graphics;

namespace MyHealthAndroid
{
	public delegate void ShowHospitalsEventHandler(int province);
	[Activity (Label = "My Health")]			
	public class HPDetailsActivity : Activity
	{
		private HPData _caller;
		private CommonData _model;
		private ListView _commonListView;
		private Button _addNumberButton;
		//private Button _saveNumberButton;
		private Button _backButton;
		private WebView _webView;
		private ImageView _imageView;

		private HPUserfulNumberAdapter _contactAdapter;


		protected async override void OnCreate (Bundle bundle)
		{

			base.OnCreate (bundle);
			_model = new CommonData ();
			_caller = _model.GetHealthProfessionals().ElementAt(Intent.GetIntExtra("callerCellPosition",0));

			//based on the extra that will be receied form last activity
			//all the resource will be set. 
			await SetContentAsPerCaller (_caller);
			SetCustomActionBar ();
		}

		//------------------------ custom activity ----------------------//
		public void SetCustomActionBar () 
		{
			ActionBar.SetDisplayShowHomeEnabled (false);
			ActionBar.SetDisplayShowTitleEnabled (false);
			ActionBar.SetCustomView (Resource.Layout.actionbar_custom);
			ActionBar.SetDisplayShowCustomEnabled (true);
		}

		//-------------------------------------- Setup Layout --------------------------------------//
		private async Task SetContentAsPerCaller (HPData data) {
		
			switch (data.Id) 
			{
			case 0:  
				await setLayoutWithTable (data.DisplayName);
				break;
			case 1:
				await setLayoutWithTable (data.DisplayName);
				break;

			case 2:
				await setSimpleLayout (data.DisplayName);
				break;

			case 3:
				await setLayoutWithTableContacts ();
				break;

			case 4:
				await setSimpleLayout (data.DisplayName);
				break;

			}

			//implement the back button 
			_backButton = FindViewById<Button> (Resource.Id.backButton);
			_backButton.Text = (data.DisplayName.Equals("Hospitals")) ? "Counties" : data.DisplayName;
			_backButton.Click += (object sender, EventArgs e) => 
			{
				base.OnBackPressed();
			};
		}

		private async Task setLayoutWithTable (string resourceName) 
		{
			SetContentView (Resource.Layout.activity_hp_details_table);
			_commonListView = FindViewById<ListView> (Resource.Id.emergencyList);
			if (resourceName.Equals ("Emergency")) {
				var emergencyAdapter = new HPEmergencyAdapter (this);
				await emergencyAdapter.loadData ();
				_commonListView.Adapter = emergencyAdapter;
				await LogManager.Log (new LogUsage {
					Date = DateTime.Now,
					Page = Convert.ToInt32(Pages.Emergency)
				});
			} else {
				var emergencyAdapter = new HPOrgnisationAdapter (this);
				await emergencyAdapter.loadData ();
				_commonListView.Adapter = emergencyAdapter;
				await LogManager.Log (new LogUsage {
					Date = DateTime.Now,
					Page = Convert.ToInt32(Pages.Organisations)
				});
			}
		}

		private async Task setLayoutWithTableContacts () 
		{
			SetContentView (Resource.Layout.activity_hp_details_contacts);
			_commonListView = FindViewById<ListView> (Resource.Id.hpUsefulContactList);
			_contactAdapter = new HPUserfulNumberAdapter (this);
			await _contactAdapter.loadData ();
			_commonListView.Adapter = _contactAdapter;

			await LogManager.Log (new LogUsage {
				Date = DateTime.Now, 
				Page = Convert.ToInt32(Pages.MyUsefulNumbers)
			});


			_addNumberButton = FindViewById<Button> (Resource.Id.addContactButton);
			//_saveNumberButton = FindViewById<Button> (Resource.Id.saveContactButton);
			//_saveNumberButton.Visibility = ViewStates.Invisible;
			_addNumberButton.Click += onAddButtonClicked;
			//_saveNumberButton.Click += onSaveButtonClicked;
		}

		private async Task setSimpleLayout (string resourceName) 
		{
			SetContentView (Resource.Layout.activity_hp_details_simple);
			_webView = FindViewById<WebView> (Resource.Id.simpleWebView);
			_imageView = FindViewById<ImageView> (Resource.Id.simpleDetailImage);

			if (resourceName.Equals ("Hospitals")) {
				//_webView.Visibility = ViewStates.Invisible;
				//_imageView.SetImageResource (Resource.Drawable.map_large);
				//_imageView.Clickable = true;
				_imageView.Visibility = ViewStates.Gone;

				MyWebViewClient _webClient = new MyWebViewClient ();
				_webClient.OnShowHospitals += (int province) => {
					var intent = new Intent(this, typeof(HospitalsInCountyActivity));
					intent.PutExtra("province", province);
					StartActivity(intent);
				};
				_webView.SetWebViewClient (_webClient);
				_webView.LoadUrl ("file:///android_asset/Content/Provinces.html");

				await LogManager.Log ( new LogUsage {
					Date = DateTime.Now,
					Page = Convert.ToInt32(Pages.Hospitals)
				});

				/*_imageView.Click += (object sender, EventArgs e) => {
					var intent = new Intent(this, typeof(HospitalsInCountyActivity));
					intent.PutExtra("county", "somecounty");
					StartActivity(intent);
				};*/

			} else {

				AboutUs aboutus = await MyHealthDB.DatabaseManager.SelectAboutUs (0);
				string htmlString = Helper.BuildHtmlForAboutUs(aboutus);

				_webView.LoadDataWithBaseURL ("file:///android_asset/", htmlString, "text/html", "utf-8", null);
				_imageView.SetImageBitmap(BitmapFactory.DecodeByteArray(aboutus.mainImage,0,aboutus.mainImage.Length));

				await LogManager.Log (new LogUsage {
					Date = DateTime.Now,
					Page = Convert.ToInt32(Pages.AboutRCSI)
				});

				//_webView.LoadUrl ("file:///android_asset/Content/AboutRCSI.html");
				//_imageView.SetImageResource (Resource.Drawable.RCSI_Front_Building_1);
			}
		}

		//-------------------------------------- Event Handlers --------------------------------------//

		protected void onAddButtonClicked (object sender, EventArgs e) 
		{
			//this calls the same dialog so it can add new number.
			ShowInputDialog (-1, _contactAdapter.contactList);
		}

		protected void onSaveButtonClicked (object sender, EventArgs e) 
		{

		}

		//-------------------------------------- Public functions --------------------------------------//

		public void ShowInputDialog(int index, List<UsefullNumbers> contactList) 
		{
			//var contactList = await MyHealthDB.DatabaseManager.SelectAllUsefullNumbers ();
			AlertDialog.Builder alert = new AlertDialog.Builder(this);


			//pass the contactsList from where you call this function. 
			alert.SetTitle("Add / Edit Contact");
			var view = this.LayoutInflater.Inflate (Resource.Layout.alertview_custom_layout, null);
			alert.SetView (view);

			var UserName = view.FindViewById<EditText>(Resource.Id.contactNameInput);
			var UserNumber = view.FindViewById<EditText>(Resource.Id.contactNumberInput);
			if (index >= 0) {
				UserName.Text = contactList.ElementAt(index).Name;
				UserNumber.Text = contactList.ElementAt(index).Number;
			}

			alert.SetPositiveButton ("Save", async (object sender, DialogClickEventArgs e) => {

				if (!UserName.Text.Equals("")) {
					UsefullNumbers contact;
					if (index >= 0) {
						contact = contactList.ElementAt(index);
						contact.Name = UserName.Text;
						contact.Number = UserNumber.Text;
					} else {
						contact = new UsefullNumbers();
						contact.ID = contactList.Max(x => x.ID) + 1;
						contact.Name = UserName.Text; 
						contact.Number = UserNumber.Text;
					}
					await MyHealthDB.DatabaseManager.SaveUsefullNumber(contact);
					_contactAdapter = new HPUserfulNumberAdapter (this);
					await _contactAdapter.loadData();
					_commonListView.Adapter = _contactAdapter;
					Toast.MakeText(this, "Updated usefull numbers list", ToastLength.Long).Show();
				}

			});

			alert.SetNegativeButton ("Cancel", (object sender, DialogClickEventArgs e) => {

			});

			alert.Show();
		}

		//-------------------------------------- Private functions --------------------------------------//

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

	public class MyWebViewClient : WebViewClient
	{

		public event ShowHospitalsEventHandler OnShowHospitals;


		public override bool ShouldOverrideUrlLoading (WebView view, string url)
		{
			Int32 province = Convert.ToInt32 (url.Substring(url.IndexOf("?") + 1));
			if (OnShowHospitals != null) {
				OnShowHospitals (province);
			}
			return true;
		}

		/*public override void OnPageStarted (WebView view, string url, Android.Graphics.Bitmap favicon)
		{
			base.OnPageStarted (view, url, favicon);
		}

		public override void OnPageFinished (WebView view, string url)
		{
			base.OnPageFinished (view, url);
		}

		public override void OnReceivedError (WebView view, ClientError errorCode, string description, string failingUrl)
		{
			base.OnReceivedError (view, errorCode, description, failingUrl);
		}*/
	}

}

