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
using Android.Views.InputMethods;

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

		private List<String> heightUnitBig, heightUnitSmall, weightUnitBig, weightUnitSmall;

		private List<String> countyList, ageList, genderList, bloodGroupList;
		Boolean isMetric;

		private String heightSelectedBig, heightSelectedSmall, weightSelectedBig, weightSelectedSmall,
		genderSelected, selectedCounty, ageSelected, bloodGroupSelected;

		protected async override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);
			SetContentView (Resource.Layout.sub_activity_profile);

			_data = new CommonData ();
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
				/*ISharedPreferencesEditor editor = preferences.Edit();

				editor.PutBoolean ("IsMetricSelected", isMetric);

				editor.PutString("heightBig", heightSelectedBig);
				editor.PutString("heightSmall", heightSelectedSmall);
				editor.PutString("weightBig", weightSelectedBig);
				editor.PutString ("weightSmall", weightSelectedSmall);

				editor.PutString("County", selectedCounty);
				editor.PutString("Age", ageSelected);
				editor.PutString ("Gender", genderSelected);
				editor.PutString("BloodGroup", bloodGroupSelected);

				editor.Apply();

				Toast.MakeText(this.BaseContext, "Profile Saved", ToastLength.Long).Show();*/
				Save_Click(sender, e);
			};

			//populate the spinners
			await SetCommonSpinnersAdapter ();

			SetSpinnersAdapter (false);

			GetFromSharedPreferences ();

			heightFeetSpinner.ItemSelected += (object sender, AdapterView.ItemSelectedEventArgs e) => {
				heightSelectedBig = heightUnitBig[e.Position];
			};
			heightInchSpinner.ItemSelected += (object sender, AdapterView.ItemSelectedEventArgs e) => {
				heightSelectedSmall = heightUnitSmall[e.Position];
			};
			weightKiloSpinner.ItemSelected += (object sender, AdapterView.ItemSelectedEventArgs e) => {
				weightSelectedBig = weightUnitBig[e.Position];
			};
			genderSpinner.ItemSelected += (object sender, AdapterView.ItemSelectedEventArgs e) => {
				genderSelected = genderList[e.Position];
			};
			weightGramSpinner.ItemSelected += (object sender, AdapterView.ItemSelectedEventArgs e) => {
				weightSelectedSmall = weightUnitSmall[e.Position];
			};

			calculateBMIButton.Click += CalculateBMI;
			matricToggleButton.Click += (object sender, EventArgs e) => {
				isMetric = matricToggleButton.Checked;
				MetricAnswerValueChanged(null, null);
			};

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
		private void CalculateBMI(object sender, EventArgs e) {
			double weight1 = 0;
			double weight2 = 0;
			double height1 = 0;
			double height2 = 0;
			double bmi = 0;

			if (matricToggleButton.Checked) {
				weight1 = Convert.ToDouble (weightUnitBig.ElementAt(weightKiloSpinner.SelectedItemPosition).Replace("kg", ""));
				weight2 = Convert.ToDouble (weightUnitSmall.ElementAt(weightGramSpinner.SelectedItemPosition).Replace("g","")) * 0.001;
				height1 = Convert.ToDouble (heightUnitBig.ElementAt(heightFeetSpinner.SelectedItemPosition).Replace("m",""));
				height2 = Convert.ToDouble (heightUnitSmall.ElementAt(heightInchSpinner.SelectedItemPosition).Replace("cm","")) * 0.01;

				bmi = Math.Round((weight1 + weight2) / ((height1 + height2) * (height1 + height2)),2);

			} else {
				weight1 = Convert.ToDouble (weightUnitBig.ElementAt(weightKiloSpinner.SelectedItemPosition).Replace("st", "")) * 14;
				weight2 = Convert.ToDouble (weightUnitSmall.ElementAt(weightGramSpinner.SelectedItemPosition).Replace("lbs",""));
				height1 = Convert.ToDouble (heightUnitBig.ElementAt(heightFeetSpinner.SelectedItemPosition).Replace("feet","")) * 12;
				height2 = Convert.ToDouble (heightUnitSmall.ElementAt(heightInchSpinner.SelectedItemPosition).Replace("in",""));

				bmi = Math.Round(((weight1 + weight2) / ((height1 + height2) * (height1 + height2))) * 703,2);
			}

			var activity = new Intent (this, typeof(MyBMI));
			activity.PutExtra ("MyBMI", bmi.ToString());
			StartActivity (activity);
		}

		//------------------------------------- Set Spinners Data -------------------------------------//
		private async Task SetCommonSpinnersAdapter ()
		{
			var counties = await _data.GetCountries ();
			countyList = counties.Select (c => c.Name).ToList ();
			ageList = _data.GetAgeList ();
			genderList = _data.GetGenderList ();
			bloodGroupList = _data.GetBloodGroups ();

			countrySpinner.Adapter = new ArrayAdapter<String> (this, Android.Resource.Layout.SimpleSpinnerDropDownItem, countyList);
			countrySpinner.ItemSelected += (object sender, AdapterView.ItemSelectedEventArgs e) => {
				selectedCounty = countyList[e.Position];
			};

			ageSpinner.Adapter = new ArrayAdapter<String> (this, Android.Resource.Layout.SimpleSpinnerDropDownItem, ageList);
			ageSpinner.ItemSelected += (object sender, AdapterView.ItemSelectedEventArgs e) => {
				ageSelected = ageList[e.Position];
			};

			genderSpinner.Adapter = new ArrayAdapter<String> (this, Android.Resource.Layout.SimpleSpinnerDropDownItem, genderList);
			genderSpinner.ItemSelected += (object sender, AdapterView.ItemSelectedEventArgs e) => {
				genderSelected = genderList[e.Position];
			};

			bloodGroupSpinner.Adapter = new ArrayAdapter<String> (this, Android.Resource.Layout.SimpleSpinnerDropDownItem, bloodGroupList);
			bloodGroupSpinner.ItemSelected += (object sender, AdapterView.ItemSelectedEventArgs e) => {
				bloodGroupSelected = bloodGroupList[e.Position];
			};
		}

		private void GetFromSharedPreferences () {
			heightSelectedBig = preferences.GetString("HeightMetre", String.Empty);
			heightSelectedSmall = preferences.GetString("HeightCm", String.Empty);
			weightSelectedBig = preferences.GetString("WeightKg", String.Empty);
			weightSelectedSmall = preferences.GetString ("WeightGram", String.Empty);

			selectedCounty = preferences.GetString ("County", String.Empty);
			ageSelected = preferences.GetString ("Age", String.Empty);
			genderSelected = preferences.GetString ("Gender", String.Empty);
			bloodGroupSelected = preferences.GetString ("BloodGroup", String.Empty);

			if (!String.IsNullOrEmpty (heightSelectedBig)) {
				isMetric = preferences.GetBoolean ("IsMetricSelected", false);

				matricToggleButton.Checked = isMetric;

				if (!isMetric) {
					MetricAnswerValueChanged (null, null);
				} else {
					SetSpinnersAdapter (isMetric);
				}
			}

			if (!String.IsNullOrEmpty (genderSelected)) {
				genderSpinner.SetSelection (genderList.IndexOf (genderSelected));
			}

			if (!String.IsNullOrEmpty (selectedCounty)) {
				countrySpinner.SetSelection (countyList.IndexOf (selectedCounty));
			}

			if (!String.IsNullOrEmpty (ageSelected)) {
				ageSpinner.SetSelection (ageList.IndexOf (ageSelected));
			}

			if (!String.IsNullOrEmpty (bloodGroupSelected)) {
				bloodGroupSpinner.SetSelection (bloodGroupList.IndexOf (bloodGroupSelected));
			}
		}

		private void SetSpinnersAdapter (Boolean isMetric)
		{
			if (!isMetric) {
				heightUnitBig = _data.GetHeightFeets ();
				heightUnitSmall = _data.GetHeightInches ();
				weightUnitBig = _data.GetWeightStones ();
				weightUnitSmall = _data.GetWeightPounds ();
			} else {
				heightUnitBig = _data.GetHeightMeters ();
				heightUnitSmall = _data.GetHeightCentimeters ();
				weightUnitBig = _data.GetWeightKilograms();
				weightUnitSmall = _data.GetWeightGrams ();
			}

			var adapter = new ArrayAdapter<String> (this, Android.Resource.Layout.SimpleSpinnerDropDownItem, heightUnitBig);
			adapter.SetDropDownViewResource (Android.Resource.Layout.SimpleSpinnerDropDownItem);
			heightFeetSpinner.Adapter = adapter;
			heightFeetSpinner.SetSelection (heightUnitBig.IndexOf (heightSelectedBig));

			adapter = new ArrayAdapter<String> (this, Android.Resource.Layout.SimpleSpinnerDropDownItem, heightUnitSmall);
			adapter.SetDropDownViewResource (Android.Resource.Layout.SimpleSpinnerDropDownItem);
			heightInchSpinner.Adapter = adapter;
			heightInchSpinner.SetSelection (heightUnitSmall.IndexOf (heightSelectedSmall));

			adapter = new ArrayAdapter<String> (this, Android.Resource.Layout.SimpleSpinnerDropDownItem, weightUnitBig);
			adapter.SetDropDownViewResource (Android.Resource.Layout.SimpleSpinnerDropDownItem);
			weightKiloSpinner.Adapter = adapter;
			weightKiloSpinner.SetSelection (weightUnitBig.IndexOf (weightSelectedBig));

			adapter = new ArrayAdapter<String> (this, Android.Resource.Layout.SimpleSpinnerDropDownItem, weightUnitSmall);
			adapter.SetDropDownViewResource (Android.Resource.Layout.SimpleSpinnerDropDownItem);
			weightGramSpinner.Adapter = adapter;
			weightGramSpinner.SetSelection (weightUnitSmall.IndexOf (weightSelectedSmall));
		}

		private void MetricAnswerValueChanged(object sender, CompoundButton.CheckedChangeEventArgs e)
		{
			double metres = 0;
			double feet = 0;
			double kg = 0;
			double stone = 0;
			char[] separators = {'.', ' '};
			if (isMetric)
			{
				if (!string.IsNullOrEmpty (heightSelectedBig) || !string.IsNullOrEmpty (heightSelectedSmall)) {

					if (string.IsNullOrEmpty (heightSelectedBig)) 
					{
						heightSelectedBig = "0 feet";
					}

					if (string.IsNullOrEmpty (heightSelectedSmall)) 
					{
						heightSelectedSmall = "0 in";
					}

					metres = (((Convert.ToDouble (heightSelectedBig.Split (separators, 2) [0]) * 12) + Convert.ToDouble (heightSelectedSmall.Split (separators, 2) [0])) * 2.54) / 100;
					if (metres.ToString ().Split (separators, 2).Length > 1) {
						heightSelectedBig = metres.ToString ().Split (separators, 2) [0] + " m";
						heightSelectedSmall = (Math.Floor((metres - Convert.ToDouble(heightSelectedBig.Split (separators,2) [0])) * 100 / 5) * 5).ToString() + " cm";
					}
				} 

				if (!string.IsNullOrEmpty (weightSelectedBig) || !string.IsNullOrEmpty (weightSelectedSmall)) 
				{
					if (string.IsNullOrEmpty (weightSelectedBig)) 
					{
						heightSelectedSmall = "0 st";
					}

					if (string.IsNullOrEmpty (weightSelectedSmall)) 
					{
						weightSelectedSmall = "0 lbs";
					}

					kg = (((Convert.ToDouble (weightSelectedBig.Split (separators,2) [0]) * 6.35029) + (Convert.ToDouble (weightSelectedSmall.Split (separators,2) [0])) * 0.453592));
					if (kg.ToString ().Split (separators,2).Length > 1) {
						weightSelectedBig = kg.ToString ().Split (separators,2) [0] + " kg";
						weightSelectedSmall = (Math.Floor((kg - Convert.ToDouble(weightSelectedBig.Split (separators,2) [0])) * 1000 / 50) * 50).ToString() + " g";
					}
				}
			}
			else 
			{
				if (!string.IsNullOrEmpty (heightSelectedBig) || !string.IsNullOrEmpty (heightSelectedSmall)) 
				{
					if (string.IsNullOrEmpty (heightSelectedBig)) 
					{
						heightSelectedBig = "0 m";
					}

					if (string.IsNullOrEmpty (heightSelectedSmall)) 
					{
						heightSelectedSmall = "0 cm";
					}

					feet = (Convert.ToDouble (heightSelectedBig.Split (separators, 2) [0]) * 100 + Convert.ToDouble (heightSelectedSmall.Split (separators, 2) [0])) * 0.0328084;
					heightSelectedBig = feet.ToString ().Split (separators, 2) [0] + " feet";
					heightSelectedSmall = Math.Round((feet - Convert.ToDouble(heightSelectedBig.Split (separators,2) [0])) * 12).ToString() + " in";
				}

				if (!string.IsNullOrEmpty (weightSelectedBig) || !string.IsNullOrEmpty (weightSelectedSmall)) 
				{
					if (string.IsNullOrEmpty (weightSelectedBig)) 
					{
						weightSelectedBig = "0 kg";
					}

					if (string.IsNullOrEmpty (weightSelectedSmall)) 
					{
						weightSelectedSmall = "0 g";
					}

					stone = ((Convert.ToDouble (weightSelectedBig.Split (separators,2) [0]) * 0.157473) + (Convert.ToDouble (weightSelectedSmall.Split (separators,2) [0]) * 0.000157473)) ;
					var st = Convert.ToDouble(stone.ToString ().Split (separators,2) [0]);
					var lbs = Math.Round((stone - st) * 14);
					if (lbs >= 14) {
						st += 1;
						lbs -= 14;
					}
					weightSelectedBig = st + " st";
					weightSelectedSmall = lbs.ToString() + " lbs";
				}
			}

			SetSpinnersAdapter (isMetric);
		}

		public void Save_Click(object sender, EventArgs e) {
			// hide the keyboard if opened
			InputMethodManager imm = (InputMethodManager)this.GetSystemService(Context.InputMethodService);
			if (imm.IsAcceptingText) {
				imm.HideSoftInputFromWindow (this.Window.CurrentFocus.WindowToken, 0);
			}
			/*String errorMessage = null;
			if (String.IsNullOrEmpty (txtName.Text)) {
				errorMessage = "Please enter your name";
			} else if (String.IsNullOrEmpty (txtDOB.Text)) {
				errorMessage = "Please select a Date of Birth";
			} else if (selectedDated > DateTime.Now.Date) {
				errorMessage = "Date of Birth cannot be in future";
			}else if (String.IsNullOrEmpty (selectedCounty)) {
				errorMessage = "Please select a county";
			} else if (String.IsNullOrEmpty(schoolsAutoComplete.Text)) {
				errorMessage = "Please enter school name";
			}

			if (!String.IsNullOrEmpty(errorMessage)) {
				Toast.MakeText(this.BaseContext, errorMessage, ToastLength.Long).Show();

				return;
			}*/

			String heightMeter = "", heightCM = "", weightKg = "", weightg = "";

			// if On then the values are already in meter
			// otherwise convert into meter and center etc
			if (isMetric) {
				heightMeter = heightSelectedBig;
				heightCM = heightSelectedSmall;
				weightKg = weightSelectedBig;
				weightg = weightSelectedSmall;
			} else {
				char[] separators = { '.', ' ' };
				// calculating height in meter and centimeter
				var heightFt = heightSelectedBig;
				var heightInc = heightSelectedSmall;
				if (string.IsNullOrEmpty (heightFt)) {
					heightFt = "0 feet";
				}

				if (string.IsNullOrEmpty (heightInc)) {
					heightInc = "0 in";
				}

				var metres = (((Convert.ToDouble (heightFt.Split (separators, 2) [0]) * 12) + Convert.ToDouble (heightInc.Split (separators, 2) [0])) * 2.54) / 100;
				if (metres.ToString ().Split (separators, 2).Length > 1) {
					heightMeter = metres.ToString ().Split (separators, 2) [0] + " m";
					heightCM = Math.Round ((metres - Convert.ToDouble (heightMeter.Split (separators, 2) [0])) * 100).ToString () + " cm";
				}

				String weightSt = weightSelectedBig;
				String weightLbs = weightSelectedSmall;
				// calculating weight in kg and g
				if (string.IsNullOrEmpty (weightSt)) {
					weightSt = "0 st";
				}

				if (string.IsNullOrEmpty (weightLbs)) {
					weightLbs = "0 lbs";
				}

				var kg = (((Convert.ToDouble (weightSt.Split (separators, 2) [0]) * 6.35029) + (Convert.ToDouble (weightLbs.Split (separators, 2) [0])) * 0.453592));
				if (kg.ToString ().Split (separators, 2).Length > 1) {
					weightKg = kg.ToString ().Split (separators, 2) [0] + " kg";
					weightg = Math.Round ((kg - Convert.ToDouble (weightKg.Split (separators, 2) [0])) * 1000).ToString () + " g";
				}
			}

			ISharedPreferencesEditor editor = preferences.Edit();

			editor.PutBoolean ("IsMetricSelected", isMetric);

			editor.PutString("HeightMetre", heightMeter);
			editor.PutString("HeightCm", heightCM);
			editor.PutString("WeightKg", weightKg);
			editor.PutString ("WeightGram", weightg);

			editor.PutString("County", selectedCounty);
			editor.PutString("Age", ageSelected);
			editor.PutString ("Gender", genderSelected);
			editor.PutString("BloodGroup", bloodGroupSelected);

			editor.Apply();

			Toast.MakeText(this.BaseContext, "Profile Saved", ToastLength.Long).Show();
		}
	}
}