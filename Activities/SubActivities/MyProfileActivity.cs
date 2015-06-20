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
using System.Threading.Tasks;

namespace MyHealthAndroid
{
	[Activity (Label = "MyHealth", ScreenOrientation = global::Android.Content.PM.ScreenOrientation.Portrait)]			
	public class MyProfileActivity : Activity
	{
		private CommonData _data;
		private Spinner heightFeetSpinner;
		private Spinner heightInchSpinner;
		private Spinner weightKiloSpinner;
		private Spinner weightGramSpinner;
		private Spinner countrySpinner;
		private Spinner ageSpinner;
		private Spinner genderSpinner;
		private Spinner bloodGroupSpinner;
		private ToggleButton matricToggleButton;
		private Button calculateBMIButton;
		private Button syncButton;
		private Button saveButton;
		private ISharedPreferences preferences;

		private List<String> HeightUnitBig;
		private List<String> HeightUnitSmall;
		private List<String> WeightUnitBig;
		private List<String> WeightUnitSmall;

		protected async override void OnCreate (Bundle bundle)
		{
			_data = new CommonData ();

			base.OnCreate (bundle);
			SetContentView (Resource.Layout.sub_activity_profile);

			//Get the shared Preferences
			preferences = PreferenceManager.GetDefaultSharedPreferences (this.ApplicationContext); 

			SetCustomActionBar ();

			await LogManager.Log<LogUsage> (new LogUsage { 
				Date = DateTime.Now, 
				Page = Convert.ToInt32(Pages.MyBMI)
			});

			heightFeetSpinner = FindViewById<Spinner> (Resource.Id.heightFeetSpinner);
			heightInchSpinner = FindViewById<Spinner> (Resource.Id.heightInchSpinner);
			weightKiloSpinner = FindViewById<Spinner> (Resource.Id.weightKiloSpinner);
			weightGramSpinner = FindViewById<Spinner> (Resource.Id.weightGramSpinner);
			countrySpinner = FindViewById<Spinner> (Resource.Id.countrySpinner);
			ageSpinner = FindViewById<Spinner> (Resource.Id.ageSpinner);
			genderSpinner = FindViewById<Spinner> (Resource.Id.genderSpinner);
			bloodGroupSpinner = FindViewById<Spinner> (Resource.Id.bloodGroupSpinner);
			matricToggleButton = FindViewById<ToggleButton> (Resource.Id.matricToggleButton);
			calculateBMIButton = FindViewById<Button> (Resource.Id.calculateBMIButton);
			syncButton = FindViewById<Button> (Resource.Id.syncButton);
			saveButton = FindViewById<Button> (Resource.Id.saveProfileButton);


			syncButton.Visibility = ViewStates.Gone;
			syncButton.Click += async (object sender, EventArgs e) => {
				try {
					ISharedPreferencesEditor editor = preferences.Edit();
					Toast.MakeText(this, "Updating database, Please wait.", ToastLength.Long).Show();
					editor.PutBoolean("applicationUpdated", false);
					editor.Apply();
					await MyHealthDB.ServiceConsumer.SyncDevice();
					Toast.MakeText(this, "Successfully updated the system.", ToastLength.Long).Show();
					editor.PutBoolean("applicationUpdated", true);
					editor.Apply();
				} catch (Exception ex) {
					Toast.MakeText(this, ex.ToString(), ToastLength.Long).Show();
				}
			};

			saveButton.Click += (object sender, EventArgs e) => {

				ISharedPreferencesEditor editor = preferences.Edit();

				editor.PutInt("heightFeetSpinnerPos", heightFeetSpinner.SelectedItemPosition);
				editor.PutInt("heightInchSpinnerPos", heightInchSpinner.SelectedItemPosition);
				editor.PutInt("weightKiloSpinnerPos", weightKiloSpinner.SelectedItemPosition);
				editor.PutInt ("weightGramSpinnerPos", weightGramSpinner.SelectedItemPosition);
				editor.PutInt("countrySpinnerPos", countrySpinner.SelectedItemPosition);
				editor.PutInt("ageSpinnerPos", ageSpinner.SelectedItemPosition);
				editor.PutInt ("genderSpinnerPos", genderSpinner.SelectedItemPosition);
				editor.PutInt("bloodGroupSpinnerPos", bloodGroupSpinner.SelectedItemPosition);

				editor.Apply();

				Toast.MakeText(this.BaseContext, "Profile Saved", ToastLength.Long).Show();

			};

			calculateBMIButton.Click += CalculateBMI;
			matricToggleButton.Click += async (object sender, EventArgs e) => {
				await SetSpinnersAdapter (matricToggleButton.Checked);
			};

			//populate the spinners
			await SetSpinnersAdapter (false);

			//chcek to see where this activity was launched from
			var ifFromLaunch = Intent.GetBooleanExtra ("fromLaunchAvtivity", false);

			// back button
			var _backButton = FindViewById<Button> (Resource.Id.backButton);
			_backButton.Text = "My Profile";
			_backButton.Click += (object sender, EventArgs e) => 
			{
				if (ifFromLaunch) {
					StartActivity(new Intent(this, typeof(HomeActivity)));
				} else {
					base.OnBackPressed();
				}
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

		//------------------------------------- Calculate BMI -------------------------------------//
		private void CalculateBMI(object sender, EventArgs e)
		{
			double weight1 = 0;
			double weight2 = 0;
			double height1 = 0;
			double height2 = 0;
			double bmi = 0;

			if (matricToggleButton.Checked) 
			{
				weight1 = Convert.ToDouble (WeightUnitBig.ElementAt(weightKiloSpinner.SelectedItemPosition).Replace("kg", ""));
				weight2 = Convert.ToDouble (WeightUnitSmall.ElementAt(weightGramSpinner.SelectedItemPosition).Replace("g","")) * 0.001;
				height1 = Convert.ToDouble (HeightUnitBig.ElementAt(heightFeetSpinner.SelectedItemPosition).Replace("m",""));
				height2 = Convert.ToDouble (HeightUnitSmall.ElementAt(heightInchSpinner.SelectedItemPosition).Replace("cm","")) * 0.01;

				bmi = Math.Round((weight1 + weight2) / ((height1 + height2) * (height1 + height2)),2);

			} else 
			{
				weight1 = Convert.ToDouble (WeightUnitBig.ElementAt(weightKiloSpinner.SelectedItemPosition).Replace("st", "")) * 14;
				weight2 = Convert.ToDouble (WeightUnitSmall.ElementAt(weightGramSpinner.SelectedItemPosition).Replace("lbs",""));
				height1 = Convert.ToDouble (HeightUnitBig.ElementAt(heightFeetSpinner.SelectedItemPosition).Replace("feet","")) * 12;
				height2 = Convert.ToDouble (HeightUnitSmall.ElementAt(heightInchSpinner.SelectedItemPosition).Replace("in",""));

				bmi = Math.Round(((weight1 + weight2) / ((height1 + height2) * (height1 + height2))) * 703,2);
			}

//			AlertDialog.Builder alert = new AlertDialog.Builder(this);
//			alert.SetTitle("BMI Calculation");
//			alert.SetMessage("your BMI is " + bmi.ToString());
//
//			alert.SetPositiveButton ("Ok", (object senderAlert, DialogClickEventArgs Args) => {
//
//			});
//
//			alert.Show();

			var activity = new Intent (this, typeof(MyBMI));
			activity.PutExtra ("MyBMI", bmi.ToString());
			StartActivity (activity);

		}

		//------------------------------------- Set Spinners Data -------------------------------------//
		private async Task SetSpinnersAdapter (Boolean Matric)
		{
			if (!Matric) {
				HeightUnitBig = _data.GetHeightFeets ();
				HeightUnitSmall = _data.GetHeightInches ();
				WeightUnitBig = _data.GetWeightStones ();
				WeightUnitSmall = _data.GetWeightPounds ();
			} else {
				HeightUnitBig = _data.GetHeightMeters ();
				HeightUnitSmall = _data.GetHeightCentimeters ();
				WeightUnitBig = _data.GetWeightKilograms();
				WeightUnitSmall = _data.GetWeightGrams ();
			}
			var CountryList = await _data.GetCountries ();
			var AgeList = _data.GetAgeList ();
			var GenderList = _data.GetGenderList ();
			var BloodGroupList = _data.GetBloodGroups ();

			heightFeetSpinner.Adapter = new ArrayAdapter<String> (this, Android.Resource.Layout.SimpleSpinnerDropDownItem, HeightUnitBig);
			heightFeetSpinner.ItemSelected += (object sender, AdapterView.ItemSelectedEventArgs e) => {
				var F = HeightUnitBig[e.Position];
			};
			heightFeetSpinner.SetSelection (preferences.GetInt ("heightFeetSpinnerPos", 0));

			heightInchSpinner.Adapter = new ArrayAdapter<String> (this, Android.Resource.Layout.SimpleSpinnerDropDownItem, HeightUnitSmall);
			heightInchSpinner.ItemSelected += (object sender, AdapterView.ItemSelectedEventArgs e) => {
				var I = HeightUnitSmall[e.Position];
			};
			heightInchSpinner.SetSelection (preferences.GetInt ("heightInchSpinnerPos", 0));

			weightKiloSpinner.Adapter = new ArrayAdapter<String> (this, Android.Resource.Layout.SimpleSpinnerDropDownItem, WeightUnitBig);
			weightKiloSpinner.ItemSelected += (object sender, AdapterView.ItemSelectedEventArgs e) => {
				var F = WeightUnitBig[e.Position];
			};
			weightKiloSpinner.SetSelection (preferences.GetInt("weightKiloSpinnerPos", 0));

			weightGramSpinner.Adapter = new ArrayAdapter<String> (this, Android.Resource.Layout.SimpleSpinnerDropDownItem, WeightUnitSmall);
			weightGramSpinner.ItemSelected += (object sender, AdapterView.ItemSelectedEventArgs e) => {
				var I = WeightUnitSmall[e.Position];
			};

			weightGramSpinner.SetSelection (preferences.GetInt ("weightGramSpinnerPos", 0));

			countrySpinner.Adapter = new ArrayAdapter<String> (this, Android.Resource.Layout.SimpleSpinnerDropDownItem, CountryList.Select(x => x.Name).ToList());
			countrySpinner.ItemSelected += (object sender, AdapterView.ItemSelectedEventArgs e) => {
				var I = CountryList[e.Position];
			};
			countrySpinner.SetSelection (preferences.GetInt("countrySpinnerPos", 0));

			ageSpinner.Adapter = new ArrayAdapter<String> (this, Android.Resource.Layout.SimpleSpinnerDropDownItem, AgeList);
			ageSpinner.ItemSelected += (object sender, AdapterView.ItemSelectedEventArgs e) => {
				var I = AgeList[e.Position];
			};
			ageSpinner.SetSelection (preferences.GetInt("ageSpinnerPos", 0));

			genderSpinner.Adapter = new ArrayAdapter<String> (this, Android.Resource.Layout.SimpleSpinnerDropDownItem, GenderList);
			genderSpinner.ItemSelected += (object sender, AdapterView.ItemSelectedEventArgs e) => {
				var I = GenderList[e.Position];
			};
			genderSpinner.SetSelection (preferences.GetInt ("genderSpinnerPos", 0));

			bloodGroupSpinner.Adapter = new ArrayAdapter<String> (this, Android.Resource.Layout.SimpleSpinnerDropDownItem, BloodGroupList);
			bloodGroupSpinner.ItemSelected += (object sender, AdapterView.ItemSelectedEventArgs e) => {
				var I = CountryList[e.Position];
			};
			bloodGroupSpinner.SetSelection (preferences.GetInt("bloodGroupSpinnerPos", 0));

		}
	}
}

