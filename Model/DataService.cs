using System;
using System.Collections.Generic;
using System.IO;

namespace MyHealthAndroid
{
	public static class DataService
	{
		public static void SaveNumbers(List<MyUsefulNumbers> numbersList)
		{
			var documentsPath = Environment.GetFolderPath (Environment.SpecialFolder.Personal);
			var filePath = Path.Combine (documentsPath, "MyUsefulNumbers.xml");
			WriteXML (numbersList,filePath);
		}

		public static List<MyUsefulNumbers> LoadNumbers()
		{
			var documentsPath = Environment.GetFolderPath (Environment.SpecialFolder.Personal);
			var filePath = Path.Combine (documentsPath, "MyUsefulNumbers.xml");

			List<MyUsefulNumbers> _numbers = ReadXML(filePath);
			if (_numbers == null) 
			{
				_numbers = new List<MyUsefulNumbers> ();
				_numbers.Add (new MyUsefulNumbers ("My GP", "1234567890"));
				_numbers.Add (new MyUsefulNumbers ("My Dentist", "+353876416352"));
				_numbers.Add (new MyUsefulNumbers ("My Health Insurer", "1234567890"));
				_numbers.Add (new MyUsefulNumbers ("My Garda Station", ""));
				_numbers.Add (new MyUsefulNumbers ("My Pharmacy", ""));
				_numbers.Add (new MyUsefulNumbers ("My Public Health Nurse", ""));
			}
			return _numbers;
		}


		private static List<MyUsefulNumbers> ReadXML(string filePath)
		{
			List<MyUsefulNumbers> numberList = null;
			if (File.Exists (filePath)) 
			{
				System.Xml.Serialization.XmlSerializer reader = 
					new System.Xml.Serialization.XmlSerializer (typeof(List<MyUsefulNumbers>));
				System.IO.StreamReader file = new System.IO.StreamReader (filePath);

				numberList = (List<MyUsefulNumbers>)reader.Deserialize (file);
				file.Close ();
			}
			return numberList;
		}

		private static void WriteXML(List<MyUsefulNumbers> numberList, string filePath)
		{
			System.Xml.Serialization.XmlSerializer writer = 
				new System.Xml.Serialization.XmlSerializer(typeof(List<MyUsefulNumbers>));

			System.IO.StreamWriter file = new System.IO.StreamWriter(filePath);
			writer.Serialize(file, numberList);
			file.Close();
		}
	}
}

