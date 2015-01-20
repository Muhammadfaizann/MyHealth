using System;
using System.Collections.Generic;
using MyHealthDB;

namespace MyHealthAndroid
{
	public class CommonData
	{
	
		public CommonData ()
		{

		}


		public IList <DiseaseCategory> GetAllDiseasesCategory () 
		{
			String[] _indexTitles = {@"Cancer", @"Diabeties", @"Heart", @"Obessity"};

			var categories = MyHealthDB.DiseaseCategoryManager.GetAllDiseaseCategories ();

			if (categories.Count <= 0) {
				for (int count= 0; count < _indexTitles.Length; count++) {
					MyHealthDB.DiseaseCategoryManager.SaveDiseaseCategory (new DiseaseCategory { 
						ID = count, 
						CategoryName = _indexTitles[count],
						CategoryDetails = "Some details if needed, like ever for " + _indexTitles[count]
					});
				}
				categories = MyHealthDB.DiseaseCategoryManager.GetAllDiseaseCategories ();
			}
			return categories; 
		}

		public IList <Disease> GetAllDiseases () 
		{
			String[] _items = {"Obesity", "Depression", "Heart Attack", "Lung Cancer","Heart Bypass",
				"Heart Failure", "Heart Murmurs", "Heart Valve Infection","Diabeties", "Asthma", 
				"Appendicitis", "Baby acne", "Burns", "Cold sores", "Dementia"};

			var disease = MyHealthDB.DiseaseManager.GetAllDiseases ();
			var randId = new Random ();
			if (disease.Count <= 0) {
				for (int count= 0; count < _items.Length; count++) {
					MyHealthDB.DiseaseManager.SaveDisease (new Disease { 
						ID = count, 
						Name = _items[count],
						Details = "Some details if needed, like ever for " + _items[count],
						DiseaseCategoryID = randId.Next(0,4)
					});
				}
				disease = MyHealthDB.DiseaseManager.GetAllDiseases ();
			}
			return disease; 
		}

		public List <County> GetAllCounty () 
		{
			var counties = MyHealthDB.CountyManager.GetAllCounties ();

			if (counties.Count <= 0) {

				MyHealthDB.CountyManager.SaveCounty (new County {
					ID = 0,
					Name = "First County",
				});

				MyHealthDB.CountyManager.SaveCounty (new County {
					ID = 1,
					Name = "Second County",
				});

				MyHealthDB.CountyManager.SaveCounty (new County {
					ID = 2,
					Name = "Third County",
				});

				counties = MyHealthDB.CountyManager.GetAllCounties ();
			}
			return counties; 
		}
			
		public List <Hospital> GetHospitalsInCounty (int countyID) 
		{
			var hospitals = MyHealthDB.HospitalManager.GetAllHospitals();
			var randId = new Random ();
			if (hospitals.Count <= 0) {

				MyHealthDB.HospitalManager.SaveHospital (new Hospital {
					ID = 0,
					Name = "Beaumont Hospital",
					PhoneNumber = "+353 1 809 3000", 
					URL = "www.beaumont.ie",
					CountyID = randId.Next(0,4)
				});

				MyHealthDB.HospitalManager.SaveHospital (new Hospital {
					ID = 1,
					Name = "Mater Hospital",
					PhoneNumber = "+353 1 803 2000", 
					URL = "www.mater.ie",
					CountyID = randId.Next(0,4)
				});

				MyHealthDB.HospitalManager.SaveHospital (new Hospital {
					ID = 2,
					Name = "Rotunda hospital",
					PhoneNumber = "+353 1 807 1700", 
					URL = "www.rotunda.ie",
					CountyID = randId.Next(0,4)
				});

				hospitals = MyHealthDB.HospitalManager.GetAllHospitals ();
			}
			return hospitals; 
		}

		public List<HPData> GetHealthProfessionals () {

			var data = new List<HPData> ();

			data.Add (new HPData { Id = 0, DisplayName = "Emergency", DisplayIcon = Resource.Drawable.emergency });
			data.Add (new HPData { Id = 1, DisplayName = "Organisations", DisplayIcon = Resource.Drawable.organisations });
			data.Add (new HPData { Id = 2, DisplayName = "Hospitals", DisplayIcon = Resource.Drawable.hospitals });
			data.Add (new HPData { Id = 3, DisplayName = "My Useful Numbers", DisplayIcon = Resource.Drawable.useful });
			data.Add (new HPData { Id = 4, DisplayName = "About", DisplayIcon = Resource.Drawable.RCSI });

			return data;
		}

		public List<String> GetDiseaseList () 
		{
			return null;
		}

		//public String[,] GetEmergencyContacts () 
		public List<EmergencyContacts> GetEmergencyContacts () 
		{
//			String[,] _items = new String[,] {
//				{"Emergency","Emergency police Fire Ambulance", "112"}, 
//				{"KDOC","Kildare and West Wicklow Doctors on Call", "1890 599 362"},
//				{"NEDOC","North East Doctor on Call", "1890 777 911"}, 
//				{"SouthDoc","Cork and Kerry", "1850 335 999"}
//			};

			List<EmergencyContacts> _items = MyHealthDB.EmergencyContactManager.GetAllEmergencyContacts ();
			if (_items.Count <= 0) {
				MyHealthDB.EmergencyContactManager.SaveEmergencyContact(new EmergencyContacts{
					ID = 0,
					Name = "Emergency",
					Description = "Emergency police Fire Ambulance",
					PhoneNumber = "112"
				});

				MyHealthDB.EmergencyContactManager.SaveEmergencyContact(new EmergencyContacts{
					ID = 1,
					Name = "KDOC",
					Description = "Kildare and West Wicklow Doctors on Call",
					PhoneNumber = "1890 599 362"
				});

				MyHealthDB.EmergencyContactManager.SaveEmergencyContact(new EmergencyContacts{
					ID = 2,
					Name = "NEDOC", 
					Description = "North East Doctor on Call",
					PhoneNumber = "1890 777 911"
				});

				MyHealthDB.EmergencyContactManager.SaveEmergencyContact(new EmergencyContacts{
					ID = 3,
					Name = "SouthDoc",
					Description = "Cork and Kerry", 
					PhoneNumber = "1850 335 999"
				});
				_items = MyHealthDB.EmergencyContactManager.GetAllEmergencyContacts ();
			}

			return _items;
		}

		public List<Organisation> GetOrgnisations()
		{
			List<Organisation> organisations = MyHealthDB.OrganisationManager.GetAllOrganisations ();
			if (organisations.Count <= 0) {
				MyHealthDB.OrganisationManager.SaveOrganisation (new Organisation {
					ID = 0,
					Name = "Irish Heart Foundation",
					PhoneNumber = "1890 432 787",
					URL = "www.irisheart.ie"
				});
				MyHealthDB.OrganisationManager.SaveOrganisation (new Organisation {
					ID = 1,
					Name = "Irish Cancer Society",
					PhoneNumber = "1800 200 700",
					URL = "www.cancer.ie"
				});
				MyHealthDB.OrganisationManager.SaveOrganisation (new Organisation {
					ID = 2,
					Name = "Diabetes Ireland",
					PhoneNumber = "1890 909 909",
					URL = "www.diabetes.ie"
				});
				MyHealthDB.OrganisationManager.SaveOrganisation (new Organisation {
					ID = 3,
					Name = "Aware",
					PhoneNumber = "016617211",
					URL = "www.aware.ie"
				});

				organisations = MyHealthDB.OrganisationManager.GetAllOrganisations ();
			}
			return organisations;	
		}

		public List<NewsChannels> GetAllChannels ()
		{
			//				channels.Add (new NewsChannels ("BBC Medical News", Resource.Drawable.NewsHealth));
			//				channels.Add (new NewsChannels ("Pulse Latest", Resource.Drawable.PULSE));
			//				channels.Add (new NewsChannels ("Irish Health", Resource.Drawable.IrishHealth));
			//				channels.Add (new NewsChannels ("Irish Times Health", Resource.Drawable.IrishTimes));
			var channels = MyHealthDB.NewsChannelsManager.GetAllNewsChannels ();
			if (channels.Count <= 0) {
				MyHealthDB.NewsChannelsManager.SaveNewsChannels ( new NewsChannels {
					ID = 0,
					Name = "BBC Medical News",
					resourceID = Resource.Drawable.NewsHealth,
					RSSFeedURL = "http://feeds.bbci.co.uk/news/health/rss.xml?edition=uk#"
				});
				MyHealthDB.NewsChannelsManager.SaveNewsChannels (new NewsChannels {
					ID = 1,
					Name = "Pulse Latest",
					resourceID = Resource.Drawable.PULSE,
					RSSFeedURL = "http://feeds.bbci.co.uk/news/health/rss.xml?edition=uk#"
				});

				MyHealthDB.NewsChannelsManager.SaveNewsChannels ( new NewsChannels {
					ID = 2,
					Name = "Irish Health",
					resourceID = Resource.Drawable.IrishHealth,
					RSSFeedURL = "http://feeds.bbci.co.uk/news/health/rss.xml?edition=uk#"
				});
				MyHealthDB.NewsChannelsManager.SaveNewsChannels ( new NewsChannels {
					ID = 3,
					Name = "Irish Times Health",
					resourceID = Resource.Drawable.IrishTimes,
					RSSFeedURL = "http://feeds.bbci.co.uk/news/health/rss.xml?edition=uk#"
				});
				channels = MyHealthDB.NewsChannelsManager.GetAllNewsChannels ();
			}
			return channels;
		}

		public List<HelpData> GetHelpList() {
			var help = new List <HelpData> ();
			help.Add(new HelpData("Blood Donation", Resource.Drawable.blood));
			help.Add(new HelpData("Organ Donors", Resource.Drawable.donor));
			help.Add (new HelpData("Feedback", Resource.Drawable.Feedback));
			help.Add (new HelpData ("My BMI", Resource.Drawable.bmi));
			return help;
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
				data.Add ("Antrim");
				data.Add ("Armagh");
				data.Add ("Carlow");
				data.Add ("Cavan");
				data.Add ("Clare");
				data.Add ("Cork");
				data.Add ("Derry");
				data.Add ("Donegal");
				data.Add ("Down");
				data.Add ("Dublin");
				data.Add ("Fermanagh");
				data.Add ("Galway");
				data.Add ("Kerry");
				data.Add ("Kildare");
				data.Add ("Kilkenny");
				data.Add ("Laois");
				data.Add ("Leitrim");
				data.Add ("Limerick");
				data.Add ("Longford");
				data.Add ("Louth");
				data.Add ("Mayo");
				data.Add ("Meath");
				data.Add ("Monaghan");
				data.Add ("Offaly");
				data.Add ("Roscommon");
				data.Add ("Sligo");
				data.Add ("Tipperary");
				data.Add ("Tyrone");
				data.Add ("Waterford");
				data.Add ("Westmeath");
				data.Add ("Wexford");
				data.Add ("Wicklow");
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

//	public class Organisation
//	{
//		public  Organisation()
//		{
//		}
//
//		public Organisation(string name, string phone, string website)
//		{
//			Name = name;
//			Phone = phone;
//			Website = website;
//		}
//
//		public string Name { get; set; }
//		public string Phone { get; set; }
//		public string Website { get; set; }
//	}

	public class MyUsefulNumbers
	{
		public string Title { get; set; }
		public string Number { get; set; }

		public MyUsefulNumbers()
		{
		}

		public MyUsefulNumbers(string title,string number)
		{
			Title = title;
			Number = number;
		}
	}

//	public class NewsChannels 
//	{
//		public string ChannelName { get; set;}
//		public int ChannelIcon { get; set;}
//
//		public NewsChannels (String Name, int res) 
//		{
//			ChannelName = Name;
//			ChannelIcon = res;
//		}
//	}
}

