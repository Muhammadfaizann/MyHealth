using System;
using System.Collections.Generic;

namespace MyHealthAndroid
{
	public class CommonData
	{
	
		public CommonData ()
		{

		}

		public String[] GetAllDiseases () 
		{
			 String[] _items = {"Obesity", "Depression", "Heart Attack", "Lung Cancer","Heart Bypass",
				"Heart Failure", "Heart Murmurs", "Heart Valve Infection","Diabeties", "Asthma", 
				"Appendicitis", "Baby acne", "Burns", "Cold sores", "Dementia"};
			return _items; 
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

		public String[,] GetEmergencyContacts () 
		{
			String[,] _items = new String[,] {
				{"Emergency","Emergency police Fire Ambulance", "112"}, 
				{"KDOC","Kildare and West Wicklow Doctors on Call", "1890 599 362"},
				{"NEDOC","North East Doctor on Call", "1890 777 911"}, 
				{"SouthDoc","Cork and Kerry", "1850 335 999"}
			};
			return _items;
		}

		public List<Organisation> GetOrgnisations()
		{
			List<Organisation> organisations = new List<Organisation>();
			organisations.Add(new Organisation("Irish Heart Foundation","1890 432 787", "www.irisheart.ie"));
			organisations.Add(new Organisation("Irish Cancer Society","1800 200 700", "www.cancer.ie"));
			organisations.Add(new Organisation("Diabetes Ireland","1890 909 909", "www.diabetes.ie"));
			organisations.Add(new Organisation("Aware","016617211", "www.aware.ie"));

			return organisations;	
		}

		public List<NewsChannels> GetAllChannels ()
		{
			var channels = new List<NewsChannels> ();
			channels.Add (new NewsChannels ("BBC Medical News", Resource.Drawable.NewsHealth));
			channels.Add (new NewsChannels ("Pulse Latest", Resource.Drawable.PULSE));
			channels.Add (new NewsChannels ("Irish Health", Resource.Drawable.IrishHealth));
			channels.Add (new NewsChannels ("Irish Times Health", Resource.Drawable.IrishTimes));

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

		public List<String> GetFeetData () {
			var data = new List<String> ();
			for (int i = 1; i <= 12; i++) {
				data.Add (i.ToString () + " feet");
			}
			return data;
		}

		public List<String> GetInchData () {
			var data = new List<String> ();
			for (int i = 1; i < 12; i++) {
				data.Add (i.ToString () + " in");
			}
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

	public class Organisation
	{
		public  Organisation()
		{
		}

		public Organisation(string name, string phone, string website)
		{
			Name = name;
			Phone = phone;
			Website = website;
		}

		public string Name { get; set; }
		public string Phone { get; set; }
		public string Website { get; set; }
	}

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

	public class NewsChannels 
	{
		public string ChannelName { get; set;}
		public int ChannelIcon { get; set;}

		public NewsChannels (String Name, int res) 
		{
			ChannelName = Name;
			ChannelIcon = res;
		}
	}
}

