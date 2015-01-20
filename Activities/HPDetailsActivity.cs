
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

namespace MyHealthAndroid
{
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


		protected override void OnCreate (Bundle bundle)
		{

			base.OnCreate (bundle);
			_model = new CommonData ();
			_caller = _model.GetHealthProfessionals().ElementAt(Intent.GetIntExtra("callerCellPosition",0));

			//based on the extra that will be receied form last activity
			//all the resource will be set. 
			SetContentAsPerCaller (_caller);
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
		private void SetContentAsPerCaller (HPData data) {
		
			switch (data.Id) 
			{
			case 0:  
				setLayoutWithTable (data.DisplayName);

				break;
			case 1:
				setLayoutWithTable (data.DisplayName);

				break;

			case 2:
				setSimpleLayout (data.DisplayName);
				break;

			case 3:
				setLayoutWithTableContacts ();
				break;

			case 4:
				setSimpleLayout (data.DisplayName);
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

		private void setLayoutWithTable (string resourceName) 
		{
			SetContentView (Resource.Layout.activity_hp_details_table);
			_commonListView = FindViewById<ListView> (Resource.Id.emergencyList);
			if (resourceName.Equals ("Emergency")) {
				var emergencyAdapter = new HPEmergencyAdapter (this);
				_commonListView.Adapter = emergencyAdapter;
			} else {
				var emergencyAdapter = new HPOrgnisationAdapter (this);
				_commonListView.Adapter = emergencyAdapter;
			}
		}

		private void setLayoutWithTableContacts () 
		{
			SetContentView (Resource.Layout.activity_hp_details_contacts);
			_commonListView = FindViewById<ListView> (Resource.Id.hpUsefulContactList);
			_contactAdapter = new HPUserfulNumberAdapter (this);
			_commonListView.Adapter = _contactAdapter;

			_addNumberButton = FindViewById<Button> (Resource.Id.addContactButton);
			//_saveNumberButton = FindViewById<Button> (Resource.Id.saveContactButton);
			//_saveNumberButton.Visibility = ViewStates.Invisible;

			_addNumberButton.Click += onAddButtonClicked;
			//_saveNumberButton.Click += onSaveButtonClicked;
		}

		private void setSimpleLayout (string resourceName) 
		{
			SetContentView (Resource.Layout.activity_hp_details_simple);
			_webView = FindViewById<WebView> (Resource.Id.simpleWebView);
			_imageView = FindViewById<ImageView> (Resource.Id.simpleDetailImage);

			if (resourceName.Equals ("Hospitals")) {
				_webView.Visibility = ViewStates.Invisible;
				_imageView.SetImageResource (Resource.Drawable.map_large);
				_imageView.Clickable = true;
				_imageView.Click += (object sender, EventArgs e) => {
					var intent = new Intent(this, typeof(HospitalsInCountyActivity));
					intent.PutExtra("county", "somecounty");
					StartActivity(intent);
				};

			} else {
				_webView.LoadUrl ("file:///android_asset/Content/AboutRCSI.html");
				_imageView.SetImageResource (Resource.Drawable.RCSI_Front_Building_1);
			}
		}

		//-------------------------------------- Event Handlers --------------------------------------//

		protected void onAddButtonClicked (object sender, EventArgs e) 
		{
			//this calls the same dialog so it can add new number.
			ShowInputDialog (-1);
		}

		protected void onSaveButtonClicked (object sender, EventArgs e) 
		{

		}

		//-------------------------------------- Public functions --------------------------------------//

		public void ShowInputDialog(int index) 
		{
			AlertDialog.Builder alert = new AlertDialog.Builder(this);

			alert.SetTitle("Add / Edit Contact");
			var view = this.LayoutInflater.Inflate (Resource.Layout.alertview_custom_layout, null);
			alert.SetView (view);

			var contactList = MyHealthDB.UsefullNumberManager.GetAllUsefullNumbers ();
			var UserName = view.FindViewById<EditText>(Resource.Id.contactNameInput);
			var UserNumber = view.FindViewById<EditText>(Resource.Id.contactNumberInput);
			if (index >= 0) {
				UserName.Text = contactList.ElementAt(index).Name;
				UserNumber.Text = contactList.ElementAt(index).Number;
			}

			alert.SetPositiveButton ("Save", (object sender, DialogClickEventArgs e) => {

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
					MyHealthDB.UsefullNumberManager.SaveUsefullNumbers(contact);
					_contactAdapter = new HPUserfulNumberAdapter (this);
					_commonListView.Adapter = _contactAdapter;
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
}

