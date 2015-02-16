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

		private List<String> HeightUnitBig;
		private List<String> HeightUnitSmall;
		private List<String> WeightUnitBig;
		private List<String> WeightUnitSmall;

		protected async override void OnCreate (Bundle bundle)
		{
			_data = new CommonData ();

			base.OnCreate (bundle);
			SetContentView (Resource.Layout.sub_activity_profile);

			SetCustomActionBar ();

			await LogManager.Log<LogUsage> (new LogUsage { 
				Date = DateTime.Now, 
				Page = Convert.ToInt32(Pages.MyProfile)
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

			syncButton.Click += async (object sender, EventArgs e) => {
				await MyHealthDB.ServiceConsumer.SyncDevice();
			};

			calculateBMIButton.Click += CalculateBMI;
			matricToggleButton.Click += (object sender, EventArgs e) => {
				SetSpinnersAdapter (matricToggleButton.Checked);
			};

			//populate the spinners
			SetSpinnersAdapter (false);

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

			AlertDialog.Builder alert = new AlertDialog.Builder(this);
			alert.SetTitle("BMI Calculation");
			alert.SetMessage("your BMI is " + bmi.ToString());

			alert.SetPositiveButton ("YES", (object senderAlert, DialogClickEventArgs Args) => {

			});

			alert.Show();

		}

		//------------------------------------- Set Spinners Data -------------------------------------//
		private void SetSpinnersAdapter (Boolean Matric)
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
			var CountryList = _data.GetCountries ();
			var AgeList = _data.GetAgeList ();
			var GenderList = _data.GetGenderList ();
			var BloodGroupList = _data.GetBloodGroups ();

			heightFeetSpinner.Adapter = new ArrayAdapter<String> (this, Android.Resource.Layout.SimpleSpinnerDropDownItem, HeightUnitBig);
			heightFeetSpinner.ItemSelected += (object sender, AdapterView.ItemSelectedEventArgs e) => {
				var F = HeightUnitBig[e.Position];
			};

			heightInchSpinner.Adapter = new ArrayAdapter<String> (this, Android.Resource.Layout.SimpleSpinnerDropDownItem, HeightUnitSmall);
			heightInchSpinner.ItemSelected += (object sender, AdapterView.ItemSelectedEventArgs e) => {
				var I = HeightUnitSmall[e.Position];
			};

			weightKiloSpinner.Adapter = new ArrayAdapter<String> (this, Android.Resource.Layout.SimpleSpinnerDropDownItem, WeightUnitBig);
			weightKiloSpinner.ItemSelected += (object sender, AdapterView.ItemSelectedEventArgs e) => {
				var F = WeightUnitBig[e.Position];
			};

			weightGramSpinner.Adapter = new ArrayAdapter<String> (this, Android.Resource.Layout.SimpleSpinnerDropDownItem, WeightUnitSmall);
			weightGramSpinner.ItemSelected += (object sender, AdapterView.ItemSelectedEventArgs e) => {
				var I = WeightUnitSmall[e.Position];
			};

			countrySpinner.Adapter = new ArrayAdapter<String> (this, Android.Resource.Layout.SimpleSpinnerDropDownItem, CountryList);
			countrySpinner.ItemSelected += (object sender, AdapterView.ItemSelectedEventArgs e) => {
				var I = CountryList[e.Position];
			};

			ageSpinner.Adapter = new ArrayAdapter<String> (this, Android.Resource.Layout.SimpleSpinnerDropDownItem, AgeList);
			ageSpinner.ItemSelected += (object sender, AdapterView.ItemSelectedEventArgs e) => {
				var I = AgeList[e.Position];
			};

			genderSpinner.Adapter = new ArrayAdapter<String> (this, Android.Resource.Layout.SimpleSpinnerDropDownItem, GenderList);
			genderSpinner.ItemSelected += (object sender, AdapterView.ItemSelectedEventArgs e) => {
				var I = GenderList[e.Position];
			};

			bloodGroupSpinner.Adapter = new ArrayAdapter<String> (this, Android.Resource.Layout.SimpleSpinnerDropDownItem, BloodGroupList);
			bloodGroupSpinner.ItemSelected += (object sender, AdapterView.ItemSelectedEventArgs e) => {
				var I = CountryList[e.Position];
			};
		}
	}
}

