﻿
using System;
using System.Collections.Generic;
using System.Linq;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using Android.Preferences;
using MyHealthDB;
using MyHealthDB.Logger;
using System.Threading.Tasks;

namespace MyHealthAndroid
{
	[Activity (Label = "MyHealth", ScreenOrientation = global::Android.Content.PM.ScreenOrientation.Portrait)]			
	public class HealthSearchActivity : Activity
	{
		private Button backButton;
		private Button atozButton;
		private Button categoryButton;
		private Button recentButton;

		private ListView _diseaseList;
		private ExpandableListView _expandableDiseaseList;
		private EditText _searchText;
		//private ArrayAdapter _listAdapter;
		private DiseaseAdapter _customDiseaseAdapter;

		private Dictionary<string, List<Disease>> dictGroup;
		private Dictionary<string, List<int?>> dictCatergoryConditionIds;
		private List<Disease> recentDiseases;
		private List<Disease> diseases;

		private Boolean _isRecentListDisplayed;

		private CommonData model;

		protected async override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			SetContentView (Resource.Layout.activity_health_search);

			_isRecentListDisplayed = false;

			SetCustomActionBar ();

			model = new CommonData ();

			_diseaseList = FindViewById<ListView> (Resource.Id.diseaseList);
			_searchText  = FindViewById<EditText> (Resource.Id.searchText);

			//_listAdapter = new ExpandableDataAdapter (this, model.getAllDiseases ());
			diseases = await model.GetAllDiseases ();
			//_listAdapter = new ArrayAdapter<String> (this,Resource.Layout.SimpleListItem ,diseases.Select(x => x.Name).ToList<string>());
			_customDiseaseAdapter = new DiseaseAdapter (this, diseases);
			_diseaseList.Adapter = _customDiseaseAdapter; //_listAdapter;

			_diseaseList.ItemClick += OnListItemClicked;

			await CreateExpendableListData ();

			_expandableDiseaseList = FindViewById<ExpandableListView> (Resource.Id.expandableDiseaseList);
			_expandableDiseaseList.SetAdapter (new ExpendableListAdapter (this, dictGroup));
			_expandableDiseaseList.Visibility = ViewStates.Gone;

			_expandableDiseaseList.ChildClick += async (object sender, ExpandableListView.ChildClickEventArgs e) => {
				var item = dictGroup.ElementAt(e.GroupPosition).Value.ElementAt(e.ChildPosition);
				var DiseaseDetails =  new Intent(this, typeof (DiseaseDetailActivity));
				var cateogryid = Convert.ToInt32(dictCatergoryConditionIds.ElementAt(e.GroupPosition).Key);
				var conditionid = dictCatergoryConditionIds.ElementAt(e.GroupPosition).Value.ElementAt(e.ChildPosition);
				await LogManager.Log (new LogContent {
					Date = DateTime.Now,
					ConditionId = conditionid,
					CategoryId = cateogryid
				});

				SaveRecentDiseases(this, item);

				DiseaseDetails.PutExtra ("diseaseName", item.Name);
				DiseaseDetails.PutExtra ("diseaseId", item.ID.Value);
				StartActivity(DiseaseDetails);
			};

			//search for the label
			_searchText.TextChanged += performSearchOnTextChange;

			// back button
			backButton = FindViewById<Button> (Resource.Id.backButton);
			backButton.Text = "Health Conditions";
			backButton.Click += (object sender, EventArgs e) => 
			{
				base.OnBackPressed();
			};

			//atoz button
			atozButton = FindViewById<Button> (Resource.Id.atozButton);
			atozButton.Click += (object sender, EventArgs e) => {

				//_listAdapter = new ArrayAdapter<String> (this,Resource.Layout.SimpleListItem, diseases.Select(x => x.Name).ToList<string>());
				_customDiseaseAdapter = new DiseaseAdapter(this, diseases); 
				_diseaseList.Adapter = _customDiseaseAdapter; //_listAdapter;

				_diseaseList.Visibility = ViewStates.Visible;

				_expandableDiseaseList.Visibility = ViewStates.Gone;

				_searchText.Visibility = ViewStates.Visible;

				_isRecentListDisplayed = false;
			};

			categoryButton = FindViewById<Button> (Resource.Id.categoriesButton);
			categoryButton.Click += (object sender, EventArgs e) => {
				_diseaseList.Visibility = ViewStates.Gone;
				_expandableDiseaseList.Visibility = ViewStates.Visible;

				_isRecentListDisplayed = false;
				_searchText.Visibility = ViewStates.Gone;
			};

			recentButton = FindViewById<Button> (Resource.Id.recentButton);
			recentButton.Click += (object sender, EventArgs e) => {

				GetRecentDiseases (this);

				//_listAdapter = new ArrayAdapter<String> (this, Resource.Layout.SimpleListItem, recentDiseases.Select(d => d.Name).ToList());
				_customDiseaseAdapter = new DiseaseAdapter(this, recentDiseases);
				_diseaseList.Adapter = _customDiseaseAdapter; //new DiseaseAdapter(this, recentDiseases);//_listAdapter;

				_diseaseList.Visibility = ViewStates.Visible;
				_expandableDiseaseList.Visibility = ViewStates.Gone;

				_isRecentListDisplayed = true;
				_searchText.Visibility = ViewStates.Gone;
			};

			//Get the recent DiseaseList onLoad
			GetRecentDiseases (this);
			await LogManager.Log (new LogUsage {
				Date = DateTime.Now,
				Page = Convert.ToInt32(Pages.HealthSearch)
			});
		}
			

		//------------------------ custom activity ----------------------//
		public void SetCustomActionBar () 
		{
			ActionBar.SetDisplayShowHomeEnabled (false);
			ActionBar.SetDisplayShowTitleEnabled (false);
			ActionBar.SetCustomView (Resource.Layout.actionbar_custom);
			ActionBar.SetDisplayShowCustomEnabled (true);
		}

		//------------------------ List Item Clicked ----------------------//
		private async void OnListItemClicked (object sender, AdapterView.ItemClickEventArgs e) {
			Disease dis;
			var selected = _customDiseaseAdapter.GetItem (e.Position) as WrapDiseaseClass;
			if (_isRecentListDisplayed) {
				dis = recentDiseases.Where(x => x.ID == selected.Id).FirstOrDefault();
			} else {
				dis = diseases.Where(x => x.ID == selected.Id).FirstOrDefault();
				SaveRecentDiseases(this, dis);
			}
			var DiseaseDetails =  new Intent(this, typeof (DiseaseDetailActivity));
			DiseaseDetails.PutExtra ("diseaseId", dis.ID.Value);
			await LogManager.Log (new LogContent {
				Date = DateTime.Now, 
				ConditionId = dis.ID,
				CategoryId = dis.DiseaseCategoryID
			});
			StartActivity(DiseaseDetails);
		}

		//------------------------ Shared Preferences ----------------------//
		private void GetRecentDiseases (Context _context) {
			ISharedPreferences prefs = PreferenceManager.GetDefaultSharedPreferences (_context);
			//recentDiseases = (prefs.GetStringSet ("RecentDisease", null) == null) ? new List<String> () : prefs.GetStringSet ("RecentDisease", null).ToList<String>();

			Org.Json.JSONArray _arry = new Org.Json.JSONArray(prefs.GetString("RecentDisease", "[]"));
			if (_arry.Length () > 0) {
				List<int> list = new List<int> ();
				for (int i = 0; i < _arry.Length (); i++) {
					list.Add (_arry.GetInt (i));
				}
				recentDiseases = diseases.Where (d => list.Contains (d.ID.Value)).ToList ();
			} else {
				recentDiseases = new List<Disease> ();
			}
		}

		private void SaveRecentDiseases (Context _context, Disease _diseaseKey) {
			if (recentDiseases == null)
				recentDiseases = new List<Disease> ();
			var foundItem = recentDiseases.Where (i => i.ID.Value.Equals (_diseaseKey.ID.Value));
			if (foundItem.Count() == 0) {
				if (recentDiseases.Count > 19) {
					recentDiseases.Insert (0, _diseaseKey);
				} else {
					recentDiseases.Add(_diseaseKey);
				}
			}
			ISharedPreferences prefs = PreferenceManager.GetDefaultSharedPreferences (_context);
			ISharedPreferencesEditor editor = prefs.Edit ();

			Org.Json.JSONArray _arry = new Org.Json.JSONArray (recentDiseases.Select(d => d.ID.ToString()).ToList());

			editor.PutString ("RecentDisease", _arry.ToString ());
			editor.Apply ();
		}

		//------------------------ search items in list ----------------------//
		private async Task<Boolean> CreateExpendableListData ()
		{
			dictGroup = new Dictionary<string, List<Disease>> ();
			dictCatergoryConditionIds = new Dictionary<string, List<int?>> ();

			var diseases = await model.GetAllDiseases ();
			var categories = await model.GetAllCategory ();
			var diseaseForCategory = await model.GetAllDiseasesForCategory ();

			foreach (var category in categories) {
				//found diseases string against this category
				string ids = diseaseForCategory.FirstOrDefault (x => x.CategoryId == category.ID).ConditionId;
				//split that string to integer array.
				int[] diseaseIDs = ids.Split (new String[]{ "," }, StringSplitOptions.RemoveEmptyEntries).Select( x => Convert.ToInt32(x)).ToArray();

				var foundItems = diseases.Where (i => diseaseIDs.Contains (i.ID.Value)).ToList (); // here you have the diseases on the selected category.
				var diseaseId = diseases.Where (i => diseaseIDs.Contains (i.ID.Value)).Select(x => x.ID).ToList (); // here you have the diseases on the selected category.

				if (foundItems != null && foundItems.Count() > 0) {
					dictGroup.Add (category.CategoryName, foundItems);
					dictCatergoryConditionIds.Add (category.ID.ToString(), diseaseId);
				} else {
					if (!dictGroup.ContainsKey (category.CategoryName)) {
						dictGroup.Add (category.CategoryName, new List<Disease> ());
					}
					dictCatergoryConditionIds.Add (category.ID.ToString(), new List<int?>());
				}
			}
			return true;
		}

		//------------------------ search items in list ----------------------//
		private void performSearchOnTextChange (object sender, global::Android.Text.TextChangedEventArgs e)
		{
			//_listAdapter.Filter.InvokeFilter (_searchText.Text);
			//List<Disease> list = diseases.Where (x => x.Name.ToLower().Contains (_searchText.Text.ToLower())).ToList();
			List<Disease> list = diseases.Where (x => x.Name.ToLower().Contains (_searchText.Text.ToLower()) || x.MisSpelling == null ? x.Name.ToLower().Contains (_searchText.Text.ToLower()) : x.MisSpelling.Trim().ToLower().Contains (_searchText.Text.ToLower())).ToList(); //.Select (x => x.Name).ToList<String>();
			//_listAdapter = new ArrayAdapter (this, Resource.Layout.SimpleListItem, list);
			_customDiseaseAdapter = new DiseaseAdapter(this, list);//_listAdapter;
			_diseaseList.Adapter = _customDiseaseAdapter;//new DiseaseAdapter (this, list);//_listAdapter;


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

