using System;
using System.Collections.Generic;
using MyHealthDB;
using System.Threading.Tasks;

namespace RCSI
{
	public class CommonData
	{
	
		public CommonData ()
		{

		}

		public async Task<List <DiseaseCategory>> GetAllCategory () 
		{
			String[] _indexTitles = {@"Cancer", @"Diabeties", @"Heart", @"Obessity"};

			var categories = await MyHealthDB.DatabaseManager.SelectAllDiseaseCategories ();

//			if (categories != null && categories.Count <= 0) {
//				for (int count= 0; count < _indexTitles.Length; count++) {
//					MyHealthDB.DiseaseCategoryManager.SaveDiseaseCategory (new DiseaseCategory { 
//						ID = count, 
//						CategoryName = _indexTitles[count],
//						CategoryDetails = "Some details if needed, like ever for " + _indexTitles[count]
//					});
//				}
//				categories = MyHealthDB.DiseaseCategoryManager.GetAllDiseaseCategories ();
//			}
			return categories; 
		}

		public async  Task<List<Disease>> GetAllDiseases () 
		{
			String[] _items = {"Obesity", "Depression", "Heart Attack", "Lung Cancer","Heart Bypass",
				"Heart Failure", "Heart Murmurs", "Heart Valve Infection","Diabeties", "Asthma", 
				"Appendicitis", "Baby acne", "Burns", "Cold sores", "Dementia"};

			var disease = await MyHealthDB.DatabaseManager.SelectAllDiseases ();
//			var randId = new Random ();
//			if (disease != null && disease.Count <= 0) {
//				for (int count= 0; count < _items.Length; count++) {
//					MyHealthDB.DiseaseManager.SaveDisease (new Disease { 
//						ID = count, 
//						Name = _items[count],
//						//Details = "Some details if needed, like ever for " + _items[count],
//						DiseaseCategoryID = randId.Next(0,4)
//					});
//				}
//				disease = MyHealthDB.DiseaseManager.GetAllDiseases ();
//			}
			return disease; 
		}

		public async Task<List<DiseasesForCategory>> GetAllDiseasesForCategory () 
		{

			var categories = await MyHealthDB.DatabaseManager.SelectAllDiseasesForCategory ();
			return categories; 
		}

		public async Task<List <County>> GetAllCounty () 
		{
			var counties = await MyHealthDB.DatabaseManager.SelectAllCounties();

//			if (counties.Count <= 0) {
//
//				MyHealthDB.CountyManager.SaveCounty (new County {
//					ID = 0,
//					Name = "First County",
//				});
//
//				MyHealthDB.CountyManager.SaveCounty (new County {
//					ID = 1,
//					Name = "First County",
//				});
//
//				MyHealthDB.CountyManager.SaveCounty (new County {
//					ID = 0,
//					Name = "First County",
//				});
//
//				counties = MyHealthDB.CountyManager.GetAllCounties ();
//			}
			return counties; 
		}
			
		public async Task<List <Hospital>> GetHospitalsInCounty (int countyID) 
		{
			List<Hospital> hospitals;
			if (countyID < 1) {
				hospitals = await MyHealthDB.DatabaseManager.SelectAllHospitals ();
			} else {
				hospitals = await MyHealthDB.DatabaseManager.SelectHospitalsByCounty (countyID);
			}
//			var randId = new Random (); 
//			if (hospitals.Count <= 0) {
//
//				MyHealthDB.HospitalManager.SaveHospital (new Hospital {
//					ID = 0,
//					Name = "Beaumont Hospital",
//					PhoneNumber = "+353 1 809 3000", 
//					URL = "www.beaumont.ie",
//					CountyID = randId.Next(0,4)
//				});
//
//				MyHealthDB.HospitalManager.SaveHospital (new Hospital {
//					ID = 1,
//					Name = "Mater Hospital",
//					PhoneNumber = "+353 1 803 2000", 
//					URL = "www.mater.ie",
//					CountyID = randId.Next(0,4)
//				});
//
//				MyHealthDB.HospitalManager.SaveHospital (new Hospital {
//					ID = 2,
//					Name = "Rotunda hospital",
//					PhoneNumber = "+353 1 807 1700", 
//					URL = "www.rotunda.ie",
//					CountyID = randId.Next(0,4)
//				});
//
//				hospitals = MyHealthDB.HospitalManager.GetAllHospitals ();
//			}
			return hospitals; 
		}



//		public List<String> GetDiseaseList () 
//		{
//			return null;
//		}

		//public String[,] GetEmergencyContacts () 
		public async Task<List<EmergencyContacts>> GetEmergencyContacts () 
		{
//			String[,] _items = new String[,] {
//				{"Emergency","Emergency police Fire Ambulance", "112"}, 
//				{"KDOC","Kildare and West Wicklow Doctors on Call", "1890 599 362"},
//				{"NEDOC","North East Doctor on Call", "1890 777 911"}, 
//				{"SouthDoc","Cork and Kerry", "1850 335 999"}
//			};

			var _items = await MyHealthDB.DatabaseManager.SelectAllEmergencyContacts ();
//			if (_items.Count <= 0) {
//				MyHealthDB.EmergencyContactManager.SaveEmergencyContact(new EmergencyContacts{
//					ID = 0,
//					Name = "Emergency",
//					Description = "Emergency police Fire Ambulance",
//					PhoneNumber = "112"
//				});
//
//				MyHealthDB.EmergencyContactManager.SaveEmergencyContact(new EmergencyContacts{
//					ID = 1,
//					Name = "KDOC",
//					Description = "Kildare and West Wicklow Doctors on Call",
//					PhoneNumber = "1890 599 362"
//				});
//
//				MyHealthDB.EmergencyContactManager.SaveEmergencyContact(new EmergencyContacts{
//					ID = 2,
//					Name = "NEDOC", 
//					Description = "North East Doctor on Call",
//					PhoneNumber = "1890 777 911"
//				});
//
//				MyHealthDB.EmergencyContactManager.SaveEmergencyContact(new EmergencyContacts{
//					ID = 3,
//					Name = "SouthDoc",
//					Description = "Cork and Kerry", 
//					PhoneNumber = "1850 335 999"
//				});
//				_items = MyHealthDB.EmergencyContactManager.GetAllEmergencyContacts ();
//			}
//
			return _items;
		}

		public async  Task<List<Organisation>> GetOrgnisations()
		{
			var organisations = await MyHealthDB.DatabaseManager.SelectAllOrganisations ();
//			if (organisations.Count <= 0) {
//				MyHealthDB.OrganisationManager.SaveOrganisation (new Organisation {
//					ID = 0,
//					Name = "Irish Heart Foundation",
//					PhoneNumber = "1890 432 787",
//					URL = "www.irisheart.ie"
//				});
//				MyHealthDB.OrganisationManager.SaveOrganisation (new Organisation {
//					ID = 1,
//					Name = "Irish Cancer Society",
//					PhoneNumber = "1800 200 700",
//					URL = "www.cancer.ie"
//				});
//				MyHealthDB.OrganisationManager.SaveOrganisation (new Organisation {
//					ID = 2,
//					Name = "Diabetes Ireland",
//					PhoneNumber = "1890 909 909",
//					URL = "www.diabetes.ie"
//				});
//				MyHealthDB.OrganisationManager.SaveOrganisation (new Organisation {
//					ID = 3,
//					Name = "Aware",
//					PhoneNumber = "016617211",
//					URL = "www.aware.ie"
//				});
//
//				organisations = MyHealthDB.OrganisationManager.GetAllOrganisations ();
//			}
			return organisations;	
		}

		public async Task<List<NewsChannels>> GetAllChannels ()
		{
			//				channels.Add (new NewsChannels ("BBC Medical News", Resource.Drawable.NewsHealth));
			//				channels.Add (new NewsChannels ("Pulse Latest", Resource.Drawable.PULSE));
			//				channels.Add (new NewsChannels ("Irish Health", Resource.Drawable.IrishHealth));
			//				channels.Add (new NewsChannels ("Irish Times Health", Resource.Drawable.IrishTimes));
			var channels = await MyHealthDB.DatabaseManager.SelectAllNewsChannels ();
//			if (channels.Count <= 0) {
//				MyHealthDB.NewsChannelsManager.SaveNewsChannels ( new NewsChannels {
//					ID = 0,
//					Name = "BBC Medical News",
//					resourceID = Resource.Drawable.NewsHealth,
//					RSSFeedURL = "http://feeds.bbci.co.uk/news/health/rss.xml?edition=uk#"
//				});
//				MyHealthDB.NewsChannelsManager.SaveNewsChannels (new NewsChannels {
//					ID = 1,
//					Name = "Pulse Latest",
//					RSSFeedURL = "http://feeds.bbci.co.uk/news/health/rss.xml?edition=uk#"
//				});
//
//				MyHealthDB.NewsChannelsManager.SaveNewsChannels ( new NewsChannels {
//					ID = 2,
//					Name = "Irish Health",
//					RSSFeedURL = "http://feeds.bbci.co.uk/news/health/rss.xml?edition=uk#"
//				});
//				MyHealthDB.NewsChannelsManager.SaveNewsChannels ( new NewsChannels {
//					ID = 3,
//					Name = "Irish Times Health",
//					RSSFeedURL = "http://feeds.bbci.co.uk/news/health/rss.xml?edition=uk#"
//				});
//				channels = MyHealthDB.NewsChannelsManager.GetAllNewsChannels ();
//			}
			return channels;
		}
			

		public List<String> GetHeightFeets () {
			var data = new List<String> ();
			for (int i = 1; i <= 12; i++) {
				data.Add (i.ToString () + " feet");
			}
			return data;
		}

		public List<String> GetHeightInches () {
			var data = new List<String> ();
			for (int i = 1; i < 12; i++) {
				data.Add (i.ToString () + " in");
			}
			return data;
		}

		public List<String> GetHeightMeters () {
			var data = new List<String> ();
			for (int i = 1; i <= 5; i++) {
				data.Add (i.ToString () + " m");
			}
			return data;
		}

		public List<String> GetHeightCentimeters () {
			var data = new List<String> ();
			for (int i = 1; i < 100; i++) {
				data.Add (i.ToString () + " cm");
			}
			return data;
		}

		public List<String> GetWeightStones () {
			var data = new List<String> ();
			for (int i = 1; i <= 100; i++) {
				data.Add (i.ToString () + " st");
			}
			return data;
		}

		public List<String> GetWeightPounds () {
			var data = new List<String> ();
			for (int i = 1; i < 14; i++) {
				data.Add (i.ToString () + " lbs");
			}
			return data;
		}

		public List<String> GetWeightKilograms () {
			var data = new List<String> ();
			for (int i = 1; i <= 500; i++) {
				data.Add (i.ToString () + " kg");
			}
			return data;
		}

		public List<String> GetWeightGrams () {
			var data = new List<String> ();
			for (int i = 1; i < 1000; i++) {
				data.Add (i.ToString () + " g");
			}
			return data;
		}

		public List<String> GetGenderList () {
			var data = new List<String> ();
				data.Add ("Male");
				data.Add ("female");
			return data;
		}

		public List<String> GetAgeList () {
			var data = new List<String> ();
				data.Add ("Under 18");
				data.Add ("18-25");
				data.Add ("26-40");
				data.Add ("41-60");
				data.Add ("60+");
			return data;
		}

		public List<String> GetBloodGroups () {
			var data = new List<String> ();
				data.Add ("O Negative");
				data.Add ("O Positive");
				data.Add ("A Negative");
				data.Add ("A Positive");
				data.Add ("B Negative");
				data.Add ("B Positive");
				data.Add ("AB Negative");
				data.Add ("AB Positive");
			return data;
		}

		public List<String> GetCountries () {
			var data = new List<String> ();
			data.Add ("Leinster");
			data.Add ("Ulster");
			data.Add ("Munster");
			data.Add ("Connacht");
			return data;
		}
	}

	//------------------------------------ custom classes ------------------------------------//

	public class HPData
	{
		public long Id { get; set;}
		public string DisplayName { get; set;}
		public int DisplayIcon { get; set;}
	}

	public class HelpData
	{
		public string HelpName { get; set; }
		public int HelpIcon { get; set;}

		public HelpData () {
		}

		public HelpData ( string name , int icon) {
			HelpName = name;
			HelpIcon = icon;
		}
	}
}

