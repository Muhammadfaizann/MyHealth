
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
using Android.Preferences;
using MyHealthDB;
using MyHealthDB.Logger;
using System.Threading.Tasks;

namespace MyHealthAndroid
{
	[Activity (Label = "My Health" , ScreenOrientation = global::Android.Content.PM.ScreenOrientation.Portrait)]			
	public class HealthSearchActivity : Activity
	{
		private Button backButton;
		private Button atozButton;
		private Button categoryButton;
		private Button recentButton;

		private ListView _diseaseList;
		private ExpandableListView _expandableDiseaseList;
		private EditText _searchText;
		private ArrayAdapter _listAdapter;

		private Dictionary<string, List<string>> dictGroup;
		private List<String> recentDiseases;
		private List<Disease> diseases;

		private CommonData model;

		protected async override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			SetContentView (Resource.Layout.activity_health_search);

			SetCustomActionBar ();

			await LogManager.Log<LogUsage> (new LogUsage (){ Date = DateTime.Now, Page = Convert.ToInt32(Pages.HealthSearch).ToString() });

			model = new CommonData ();

			_diseaseList = FindViewById<ListView> (Resource.Id.diseaseList);
			_searchText  = FindViewById<EditText> (Resource.Id.searchText);

			//_listAdapter = new ExpandableDataAdapter (this, model.getAllDiseases ());
			diseases = await model.GetAllDiseases ();
			_listAdapter = new ArrayAdapter<String> (this,Resource.Layout.SimpleListItem ,diseases.Select(x => x.Name).ToList<string>());
			_diseaseList.Adapter = _listAdapter;

			_diseaseList.ItemClick += OnListItemClicked;

			await CreateExpendableListData ();

			_expandableDiseaseList = FindViewById<ExpandableListView> (Resource.Id.expandableDiseaseList);
			_expandableDiseaseList.SetAdapter (new ExpendableListAdapter (this, dictGroup));
			_expandableDiseaseList.Visibility = ViewStates.Gone;

			_expandableDiseaseList.ChildClick += (object sender, ExpandableListView.ChildClickEventArgs e) => {
				var item = dictGroup.ElementAt(e.GroupPosition).Value.ElementAt(e.ChildPosition);
				var DiseaseDetails =  new Intent(this, typeof (DiseaseDetailActivity));
				DiseaseDetails.PutExtra ("diseaseName", item);
				StartActivity(DiseaseDetails);
			};

			//search for the label
			_searchText.TextChanged += performSearchOnTextChange;

			// back button
			backButton = FindViewById<Button> (Resource.Id.backButton);
			backButton.Text = "Health Search";
			backButton.Click += (object sender, EventArgs e) => 
			{
				base.OnBackPressed();
			};

			//atoz button
			atozButton = FindViewById<Button> (Resource.Id.atozButton);
			atozButton.Click += (object sender, EventArgs e) => {

				_listAdapter = new ArrayAdapter<String> (this,Resource.Layout.SimpleListItem ,diseases.Select(x => x.Name).ToList<string>());
				_diseaseList.Adapter = _listAdapter;

				_diseaseList.Visibility = ViewStates.Visible;
				_expandableDiseaseList.Visibility = ViewStates.Gone;
			};

			categoryButton = FindViewById<Button> (Resource.Id.categoriesButton);
			categoryButton.Click += (object sender, EventArgs e) => {
				_diseaseList.Visibility = ViewStates.Gone;
				_expandableDiseaseList.Visibility = ViewStates.Visible;
			};

			recentButton = FindViewById<Button> (Resource.Id.recentButton);
			recentButton.Click += (object sender, EventArgs e) => {

				GetRecentDiseases (this);

				_listAdapter = new ArrayAdapter<String> (this, Resource.Layout.SimpleListItem, recentDiseases);
				_diseaseList.Adapter = _listAdapter;

				_diseaseList.Visibility = ViewStates.Visible;
				_expandableDiseaseList.Visibility = ViewStates.Gone;
			};

			//Get the recent DiseaseList onLoad
			GetRecentDiseases (this);

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
		private void OnListItemClicked (object sender, AdapterView.ItemClickEventArgs e) {
			var dis = diseases.ElementAtOrDefault (e.Position);
			var t = dis.Name;
			SaveRecentDiseases(this, t);
			var DiseaseDetails =  new Intent(this, typeof (DiseaseDetailActivity));
			DiseaseDetails.PutExtra ("diseaseName", t);
			DiseaseDetails.PutExtra ("diseaseId", dis.ID.ToString());
			StartActivity(DiseaseDetails);
		}

		//------------------------ Shared Preferences ----------------------//
		private void GetRecentDiseases (Context _context) {
			ISharedPreferences prefs = PreferenceManager.GetDefaultSharedPreferences (_context);
			recentDiseases = (prefs.GetStringSet ("RecentDisease", null) == null) ? new List<String> () : prefs.GetStringSet ("RecentDisease", null).ToList<String>();
			 
		}

		private void SaveRecentDiseases (Context _context, String _key) {
			if (recentDiseases == null)
				recentDiseases = new List<String> ();
			var foundItem = recentDiseases.Where (i => i.Contains (_key));
			if (foundItem.Count() == 0) {
				if (recentDiseases.Count > 19) {
					recentDiseases.Insert (0, _key);
				} else {
					recentDiseases.Add(_key);
				}
			}
			ISharedPreferences prefs = PreferenceManager.GetDefaultSharedPreferences (_context);
			ISharedPreferencesEditor editor = prefs.Edit ();

			editor.PutStringSet ("RecentDisease", recentDiseases);
			editor.Apply ();
		}

		//------------------------ search items in list ----------------------//
		private async Task<Boolean> CreateExpendableListData ()
		{
			dictGroup = new Dictionary<string, List<string>> ();

			var diseases = await model.GetAllDiseases ();
			var categories = await model.GetAllCategory ();
			var diseaseForCategory = await model.GetAllDiseasesForCategory ();

			foreach (var category in categories) {
				//found diseases string against this category
				string ids = diseaseForCategory.FirstOrDefault (x => x.CategoryId == category.ID).ConditionId;
				//split that string to integer array.
				int[] diseaseIDs = ids.Split (new String[]{ "," }, StringSplitOptions.RemoveEmptyEntries).Select( x => Convert.ToInt32(x)).ToArray();

				var foundItems = diseases.Where (i => diseaseIDs.Contains (i.ID.Value)).Select(x => x.Name).ToList (); // here you have the diseases on the selected category.

				if (foundItems != null && foundItems.Count() > 0) {
					dictGroup.Add (category.CategoryName, foundItems);
				} else {
					dictGroup.Add (category.CategoryName, new List<string>());
				}
			}
			return true;
		}

		//------------------------ search items in list ----------------------//
		private void performSearchOnTextChange (object sender, global::Android.Text.TextChangedEventArgs e)
		{
			_listAdapter.Filter.InvokeFilter (_searchText.Text);
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

