using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;
using System.Threading.Tasks;
using System.Text;
using MyHealthDB;
using MyHealthDB.Model;
using MyHealthDB.Service;
using MyHealthDB.Logger;
using UIKit;
using System.Drawing;
using CoreGraphics;


namespace MyHealthDB
{
	public static class ServiceConsumer
	{
		private static WebService _service;
		private static HttpResponseMessage obj;
		private static string content = "";

		public async static Task<Boolean> CheckRegisteredDevice ()
		{
			var device = await DatabaseManager.SelectDevice ();
			if (device == null) {
				return false;
			}
			return true;
		}

		public async static Task<Boolean> RegisterDevice (String device)
		{
			_service = new WebService ();
			obj = new HttpResponseMessage();

			string DeviceId = Guid.NewGuid().ToString();
			string UserName = "Name" + DateTime.Now.Ticks.ToString();
			string Type = device; //DateTime.Now.Second % 2 == 0 ? "Android" : "IPhone";
			string OSVer = UIDevice.CurrentDevice.SystemVersion;
			string OSVersion = OSVer.Replace (".", "-");
			string Hash = Helper.Helper.GetRegistrationMD5(DeviceId, Type, UserName);
			obj = await _service.RegisterDevice(DeviceId, Type, UserName, Hash, OSVersion);
			content = await obj.Content.ReadAsStringAsync();
			SMApplicationUsersApp _SMtblRegisterDevice = JsonConvert.DeserializeObject<SMApplicationUsersApp>(content);
			if (_SMtblRegisterDevice.DEVICE_ID.ToLower () == DeviceId.ToLower ()) {
				//Save UserName, DeviceType, DeviceId into Database.
				//Use these for SMHandShake Call

				await DatabaseManager.SaveDevice (new RegisteredDevice {
					ID = _SMtblRegisterDevice.ID,
					UserName = _SMtblRegisterDevice.UserName,
					DeviceId = _SMtblRegisterDevice.DEVICE_ID,
					DeviceType = _SMtblRegisterDevice.Type
				});
			} else {
				return false;
			}

			return true;
		}

		public async static Task<Boolean> CreateDatabase(String device = "Android")
		{
			await MyHealthDB.DatabaseManager.CreateAllTables ();
			return true;
		}

		// create this as a generic function to get //
		public async static Task<Boolean> SyncDevice ()
		{
			_service = new WebService ();

			obj = new HttpResponseMessage();

			//Helper.Helper.DeviceId = "9412D71E-ED92-4149-991E-5D2F26ED4D8F";
			//Helper.Helper.PIN = "1234";
			var AllDevices = await DatabaseManager.SelectAllDevices ();
//			if (AllDevices.Count < 1)
//				return false;
			Helper.Helper.DeviceId = AllDevices [0].DeviceId;
			Helper.Helper.Hash  = Helper.Helper.generateMD5(Helper.Helper.DeviceId + Helper.Helper.PIN + DateTime.Now.Day);

			try {
				//the initial hand shake
				obj = await _service.HandShake(Helper.Helper.DeviceId, Helper.Helper.Hash);
				content = await obj.Content.ReadAsStringAsync();
				SMHandShake _SMHandShake = JsonConvert.DeserializeObject<SMHandShake>(content);
				Helper.Helper.Hash = _SMHandShake.Hash;
				if (_SMHandShake.StatusId != 1) {
					Console.WriteLine ("HandShake was rejected");
					return false;
				}

				//get the latest about us
				obj = await _service.GetAboutUs ();
				content = await obj.Content.ReadAsStringAsync ();
				List<AboutUs> aboutus = JsonConvert.DeserializeObject<List<AboutUs>> (content);
				if (aboutus != null && aboutus.Count > 0 ) {
					if (await UpdateDBManager.UpdateAboutUs (aboutus [0])) {
						Console.WriteLine ("About us was updated. ");
					}
				}

				//call the service for updates.
				obj = await _service.GetAllConditions ();
				content = await obj.Content.ReadAsStringAsync();
				List<SMtblCPCondition> _AllDiseases = JsonConvert.DeserializeObject<List<SMtblCPCondition>>(content);
				if (await UpdateDBManager.UpdateDiseases (_AllDiseases)) {
					Console.WriteLine ("\n Diseases are updated. \n");
				}

				obj = await _service.GetAllCategories ();
				content = await obj.Content.ReadAsStringAsync();
				List<SMtblCPCategory> _AllCategories = JsonConvert.DeserializeObject<List<SMtblCPCategory>>(content);
				if (await UpdateDBManager.UpdateCategories (_AllCategories)) {
					Console.WriteLine ("\n Categories are updated. \n");
				}

				obj = await _service.GetConditionCategories();
				content = await obj.Content.ReadAsStringAsync();
				List<SMConditionCategories> _ConditionCategory = JsonConvert.DeserializeObject<List<SMConditionCategories>>(content);
				if (await UpdateDBManager.UpdateDiseasesCategories (_ConditionCategory)) {
					Console.WriteLine ("\n DiseasesForCategory are updated. \n");
				}

				obj = await _service.GetAllProvince ();
				content = await obj.Content.ReadAsStringAsync();
				List<SMtblProvince> _AllProvinces = JsonConvert.DeserializeObject<List<SMtblProvince>>(content);
				if (await UpdateDBManager.UpdateProvince (_AllProvinces)) {
					Console.WriteLine ("\n All Provinces are updated. \n");
				}

				obj = await _service.GetAllCounty ();
				content = await obj.Content.ReadAsStringAsync();
				List<SMtblCounty> _AllCounties = JsonConvert.DeserializeObject<List<SMtblCounty>>(content);
				if (await UpdateDBManager.UpdateCounty (_AllCounties)) {
					Console.WriteLine ("\n DiseasesForCategory are updated. \n");
				}

				obj = await _service.GetHospitals ();
				content = await obj.Content.ReadAsStringAsync();
				List<SMtblHealthHospital> _AllHospitals = JsonConvert.DeserializeObject<List<SMtblHealthHospital>>(content);
				if (await UpdateDBManager.UpdateHospitals(_AllHospitals)) {
					Console.WriteLine ("\n Hospitals are updated. \n");
				}

				obj = await _service.GetAllEmergencyNumbers ();
				content = await obj.Content.ReadAsStringAsync();
				List<SMtblHealthEmergencyNumber> _AllEmergencyContacts = JsonConvert.DeserializeObject<List<SMtblHealthEmergencyNumber>>(content);
				if (await UpdateDBManager.UpdateEmergencyNumber(_AllEmergencyContacts)) {
					Console.WriteLine ("\n Emergency Numbers are updated. \n");
				}

				obj = await _service.GetAllOrgnisations ();
				content = await obj.Content.ReadAsStringAsync();
				List<SMtblHealthOrganizationsInfo> _AllOrganisations = JsonConvert.DeserializeObject<List<SMtblHealthOrganizationsInfo>>(content);
				if (await UpdateDBManager.UpdateOrganizations (_AllOrganisations)) {
					Console.WriteLine ("\n Organisations are updated.");
				}

				obj = await _service.GetAllCpUsers ();
				content = await obj.Content.ReadAsStringAsync();
				List<SMtblCpUser> _allCpUsers = JsonConvert.DeserializeObject<List<SMtblCpUser>>(content);
				if (await UpdateDBManager.UpdateCpUsers (_allCpUsers)) {
					Console.WriteLine ("\n CpUsers are updated.");
				}

				obj = await _service.GetImportantNotices ();
				content = await obj.Content.ReadAsStringAsync();
				List<SMtblHealthImportantNotice> _allImportantNotices = JsonConvert.DeserializeObject<List<SMtblHealthImportantNotice>>(content);
				if (await UpdateDBManager.UpdateImportantNotices (_allImportantNotices)) {
					Console.WriteLine ("\n ImportantNotice are updated.");
				}
					
				obj = await _service.GoodBye();

			} catch (Exception ex) {
				Console.WriteLine ("Exception : {0}", ex);
			}
			await LogManager.SyncAllLogs ();
			//TODO : Make a table and save successfull sync date. 
			return true;
		}

		async public static Task<List<BloodSupply>> GetBloodDonationInfo(String url)
		{ 
			List<BloodSupply> bloodSupplyList = new List<BloodSupply>();
			try
			{
				System.Net.WebRequest webRequest = System.Net.WebRequest.Create(url);
				System.Net.WebResponse webResponse = await webRequest.GetResponseAsync();
				Stream stream = webResponse.GetResponseStream();
				System.Xml.XmlDocument xmlDocument = new System.Xml.XmlDocument();
				xmlDocument.Load(stream);
				System.Xml.XmlNamespaceManager nsmgr = new System.Xml.XmlNamespaceManager(xmlDocument.NameTable);
				nsmgr.AddNamespace("media", xmlDocument.DocumentElement.GetNamespaceOfPrefix("media"));
				System.Xml.XmlNodeList itemNodes = xmlDocument.SelectNodes("CurrentBloodSupply/BloodSupply");

				for (int i = 0; i < itemNodes.Count; i++)
				{
					BloodSupply bloodSupplyItem = new BloodSupply();

					if (itemNodes[i].SelectSingleNode("Type") != null)
					{
						bloodSupplyItem.BloodGroup = itemNodes[i].SelectSingleNode("Type").InnerText;
					}
					if (itemNodes[i].SelectSingleNode("SupplyDays") != null)
					{
						bloodSupplyItem.SupplyDays = itemNodes[i].SelectSingleNode("SupplyDays").InnerText;
					}

					bloodSupplyList.Add(bloodSupplyItem);
				}
			}
			catch (Exception)
			{
				throw;
			}

			return bloodSupplyList;		
		}
	}

	public class BloodSupply
	{
		public String BloodGroup {
			get;
			set;
		}

		public String SupplyDays {
			get;
			set;
		}
	}



}

