using System;
using System.Net.Http;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;
using System.Threading.Tasks;
using MyHealthDB.Model;
using MyHealthDB.Service;
using MyHealthDB.Logger;

namespace MyHealthDB
{
    public static class ServiceConsumer
	{
		private static WebService _service;
		private static HttpResponseMessage obj;
		private static string content = "";

        static ServiceConsumer()
        {
            _service = new WebService();
        }

        public async static Task<Boolean> CheckRegisteredDevice ()
		{
			var device = await DatabaseManager.SelectDevice ();
			if (device == null) {
				return false;
			}
			return true;
		}

		public async static Task<Boolean> RegisterDevice (String device, string OSVersion)
		{
			obj = new HttpResponseMessage();

			string DeviceId = Guid.NewGuid().ToString();
			string UserName = "Name" + DateTime.Now.Ticks.ToString();
			string Type = device; //DateTime.Now.Second % 2 == 0 ? "Android" : "IPhone";
			string Hash = Helper.Helper.GetRegistrationMD5(DeviceId, Type, UserName);
			//HC

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
			//Helper.Helper.DeviceId = "9412D71E-ED92-4149-991E-5D2F26ED4D8F";
			//Helper.Helper.PIN = "1234";
			var AllDevices = await DatabaseManager.SelectAllDevices ();
//			if (AllDevices.Count < 1)
//				return false;
			Helper.Helper.DeviceId = AllDevices [0].DeviceId;
			Helper.Helper.Hash  = Helper.Helper.generateMD5(Helper.Helper.DeviceId + Helper.Helper.PIN + DateTime.Now.Day);
			//Helper.Helper.Hash  = Helper.Helper.generateMD5(Helper.Helper.DeviceId + Helper.Helper.PIN + syncDate.Day);

			try {
				String serviceContent;

				// the initial hand shake
				using (var responseMessageObj = await _service.HandShake(Helper.Helper.DeviceId, Helper.Helper.Hash)) {
					serviceContent = await responseMessageObj.Content.ReadAsStringAsync();
					SMHandShake _SMHandShake = JsonConvert.DeserializeObject<SMHandShake>(serviceContent);
					Helper.Helper.Hash = _SMHandShake.Hash;
					if (_SMHandShake.StatusId != 1) {
						Console.WriteLine ("HandShake was rejected");
						return false;
					}
				}

				//get the latest about us
				using (var responseMessageObj = await _service.GetAboutUs ()) {
					serviceContent = await responseMessageObj.Content.ReadAsStringAsync ();
					List<AboutUs> aboutus = JsonConvert.DeserializeObject<List<AboutUs>> (serviceContent);
					if (aboutus != null && aboutus.Count > 0) {
						if (await UpdateDBManager.UpdateAboutUs (aboutus [0])) {
							Console.WriteLine ("About us was updated. ");
						}
					}
				}

				//call the service for updates.
				using (var responseMessageObj = await _service.GetAllConditions ()) {
					serviceContent = await responseMessageObj.Content.ReadAsStringAsync();
					List<SMtblCPCondition> _AllDiseases = JsonConvert.DeserializeObject<List<SMtblCPCondition>>(serviceContent);
					if (await UpdateDBManager.UpdateDiseases (_AllDiseases)) {
						Console.WriteLine ("\n Diseases are updated. \n");
					}
				}

				using (var responseMessageObj = await _service.GetAllCategories ()) {
					serviceContent = await responseMessageObj.Content.ReadAsStringAsync();
					List<SMtblCPCategory> _AllCategories = JsonConvert.DeserializeObject<List<SMtblCPCategory>>(serviceContent);
					if (await UpdateDBManager.UpdateCategories (_AllCategories)) {
						Console.WriteLine ("\n Categories are updated. \n");
					}
				}

				using (var responseMessageObj = await _service.GetConditionCategories()) {
					serviceContent = await responseMessageObj.Content.ReadAsStringAsync();
					List<SMConditionCategories> _ConditionCategory = JsonConvert.DeserializeObject<List<SMConditionCategories>>(serviceContent);
					if (await UpdateDBManager.UpdateDiseasesCategories (_ConditionCategory)) {
						Console.WriteLine ("\n DiseasesForCategory are updated. \n");
					}
				}

				using (var responseMessageObj = await _service.GetAllProvince ()) {
					serviceContent = await responseMessageObj.Content.ReadAsStringAsync();
					List<SMtblProvince> _AllProvinces = JsonConvert.DeserializeObject<List<SMtblProvince>>(serviceContent);
					if (await UpdateDBManager.UpdateProvince (_AllProvinces)) {
						Console.WriteLine ("\n All Provinces are updated. \n");
					}
				}

				using (var responseMessageObj = await _service.GetAllCounty ()) {
					serviceContent = await responseMessageObj.Content.ReadAsStringAsync();
					List<SMtblCounty> _AllCounties = JsonConvert.DeserializeObject<List<SMtblCounty>>(serviceContent);
					if (await UpdateDBManager.UpdateCounty (_AllCounties)) {
						Console.WriteLine ("\n DiseasesForCategory are updated. \n");
					}
				}

				using (var responseMessageObj = await _service.GetHospitalsNoDate ()) {
					serviceContent = await responseMessageObj.Content.ReadAsStringAsync();
					List<SMtblHealthHospital> _AllHospitals = JsonConvert.DeserializeObject<List<SMtblHealthHospital>>(serviceContent);
					if (await UpdateDBManager.UpdateHospitals(_AllHospitals)) {
						Console.WriteLine ("\n Hospitals are updated. \n");
					}
				}

				using (var responseMessageObj = await _service.GetAllEmergencyNumbers ()) {
					serviceContent = await responseMessageObj.Content.ReadAsStringAsync();
					List<SMtblHealthEmergencyNumber> _AllEmergencyContacts = JsonConvert.DeserializeObject<List<SMtblHealthEmergencyNumber>>(serviceContent);
					if (await UpdateDBManager.UpdateEmergencyNumber(_AllEmergencyContacts)) {
						Console.WriteLine ("\n Emergency Numbers are updated. \n");
					}
				}

				using (var responseMessageObj = await _service.GetAllOrgnisationsNoDate ()) {
					serviceContent = await responseMessageObj.Content.ReadAsStringAsync();
					List<SMtblHealthOrganizationsInfo> _AllOrganisations = JsonConvert.DeserializeObject<List<SMtblHealthOrganizationsInfo>>(serviceContent);
					if (await UpdateDBManager.UpdateOrganizations (_AllOrganisations)) {
						Console.WriteLine ("\n Organisations are updated.");
					}
				}

				using (var responseMessageObj = await _service.GetAllCpUsers ()) {
					serviceContent = await responseMessageObj.Content.ReadAsStringAsync();
					List<SMtblCpUser> _allCpUsers = JsonConvert.DeserializeObject<List<SMtblCpUser>>(serviceContent);
					if (await UpdateDBManager.UpdateCpUsers (_allCpUsers)) {
						Console.WriteLine ("\n CpUsers are updated.");
					}
				}

				using (var responseMessageObj = await _service.GetImportantNotices ()) {
					serviceContent = await responseMessageObj.Content.ReadAsStringAsync();
					List<SMtblHealthImportantNotice> _allImportantNotices = JsonConvert.DeserializeObject<List<SMtblHealthImportantNotice>>(serviceContent);
					if (await UpdateDBManager.UpdateImportantNotices (_allImportantNotices)) {
						Console.WriteLine ("\n ImportantNotice are updated.");
					}
				}

                using (var responseMessageObj = await _service.GetVideoLinks())
                {
                    serviceContent = await responseMessageObj.Content.ReadAsStringAsync();
                    var allVideoLinks = JsonConvert.DeserializeObject<List<SMtblVideoLink>>(serviceContent);

                    if (await UpdateDBManager.UpdateVideoLinks(allVideoLinks))
                    {
                        Console.WriteLine("\n Video links are updated.");
                    }
                }

                using (var responseMessageObj = await _service.GetMediaCategories())
                {
                    serviceContent = await responseMessageObj.Content.ReadAsStringAsync();
                    var allMediaCategories = JsonConvert.DeserializeObject<List<SMtblMediaCategory>>(serviceContent);

                    if (await UpdateDBManager.UpdateMediaCategories(allMediaCategories))
                    {
                        Console.WriteLine("\n Media Categories are updated");
                    }
                }

                using (var responseMessageObj = await _service.GoodBye())
				{
				}

			} catch (Exception ex) {
				Console.WriteLine ("Exception : {0}", ex);
			}
			await LogManager.SyncAllLogs ();
			//TODO : Make a table and save successfull sync date. 
			return true;
		}

		// sync device for the first time
		public async static Task<Boolean> FirstTimeSyncDevice ()
		{
			var AllDevices = await DatabaseManager.SelectAllDevices ();
			Helper.Helper.DeviceId = AllDevices [0].DeviceId;
			Helper.Helper.Hash  = Helper.Helper.generateMD5(Helper.Helper.DeviceId + Helper.Helper.PIN + DateTime.Now.Day);

			try {
				String serviceContent;

				// the initial hand shake to get token to be use in further requests
				using (var responseMessageObj = await _service.HandShake(Helper.Helper.DeviceId, Helper.Helper.Hash)) {
					serviceContent = await responseMessageObj.Content.ReadAsStringAsync();
					SMHandShake _SMHandShake = JsonConvert.DeserializeObject<SMHandShake>(serviceContent);
					Helper.Helper.Hash = _SMHandShake.Hash;
					if (_SMHandShake.StatusId != 1) {
						Console.WriteLine ("HandShake was rejected");
						return false;
					}
				}

				//get the latest about us
				using (var responseMessageObj = await _service.GetAboutUs ()) {
					serviceContent = await responseMessageObj.Content.ReadAsStringAsync ();
					List<AboutUs> aboutus = JsonConvert.DeserializeObject<List<AboutUs>> (serviceContent);
					if (aboutus != null && aboutus.Count > 0) {
						if (await UpdateDBManager.UpdateAboutUs (aboutus [0])) {
							Console.WriteLine ("About us was updated. ");
						}
					}
				}

				using (var responseMessageObj = await _service.GetAllProvince ()) {
					serviceContent = await responseMessageObj.Content.ReadAsStringAsync();
					List<SMtblProvince> _AllProvinces = JsonConvert.DeserializeObject<List<SMtblProvince>>(serviceContent);
					if (await UpdateDBManager.UpdateProvince (_AllProvinces)) {
						Console.WriteLine ("\n All Provinces are updated. \n");
					}
				}

				using (var responseMessageObj = await _service.GetAllCounty ()) {
					serviceContent = await responseMessageObj.Content.ReadAsStringAsync();
					List<SMtblCounty> _AllCounties = JsonConvert.DeserializeObject<List<SMtblCounty>>(serviceContent);
					if (await UpdateDBManager.UpdateCounty (_AllCounties)) {
						Console.WriteLine ("\n DiseasesForCategory are updated. \n");
					}
				}

				using (var responseMessageObj = await _service.GetAllEmergencyNumbers ()) {
					serviceContent = await responseMessageObj.Content.ReadAsStringAsync();
					List<SMtblHealthEmergencyNumber> _AllEmergencyContacts = JsonConvert.DeserializeObject<List<SMtblHealthEmergencyNumber>>(serviceContent);
					if (await UpdateDBManager.UpdateEmergencyNumber(_AllEmergencyContacts)) {
						Console.WriteLine ("\n Emergency Numbers are updated. \n");
					}
				}

				using (var responseMessageObj = await _service.GetAllCpUsers ()) {
					serviceContent = await responseMessageObj.Content.ReadAsStringAsync();
					List<SMtblCpUser> _allCpUsers = JsonConvert.DeserializeObject<List<SMtblCpUser>>(serviceContent);
					if (await UpdateDBManager.UpdateCpUsers (_allCpUsers)) {
						Console.WriteLine ("\n CpUsers are updated.");
					}
				}

				using (var responseMessageObj = await _service.GetImportantNotices ()) {
					serviceContent = await responseMessageObj.Content.ReadAsStringAsync();
					List<SMtblHealthImportantNotice> _allImportantNotices = JsonConvert.DeserializeObject<List<SMtblHealthImportantNotice>>(serviceContent);
					if (await UpdateDBManager.UpdateImportantNotices (_allImportantNotices)) {
						Console.WriteLine ("\n ImportantNotice are updated.");
					}
				}

                using (var responseMessageObj = await _service.GetVideoLinks())
                {
                    serviceContent = await responseMessageObj.Content.ReadAsStringAsync();
                    var allVideoLinks = JsonConvert.DeserializeObject<List<SMtblVideoLink>>(serviceContent);

                    if (await UpdateDBManager.UpdateVideoLinks(allVideoLinks))
                    {
                        Console.WriteLine("\n Video links are updated.");
                    }
                }

                using (var responseMessageObj = await _service.GoodBye())
				{
				}

			} catch (Exception ex) {
				Console.WriteLine ("Exception : {0}", ex);
			}
			await LogManager.SyncAllLogs ();
			//TODO : Make a table and save successfull sync date. 
			return true;
		}

		public static async Task<Boolean> SyncConditionsData()
		{
			var AllDevices = await DatabaseManager.SelectAllDevices ();
			Helper.Helper.DeviceId = AllDevices [0].DeviceId;
			Helper.Helper.Hash  = Helper.Helper.generateMD5(Helper.Helper.DeviceId + Helper.Helper.PIN + DateTime.Now.Day);

			try {
				String serviceContent;

				// the initial hand shake to get token to be use in further requests
				using (var responseMessageObj = await _service.HandShake(Helper.Helper.DeviceId, Helper.Helper.Hash)) {
					serviceContent = await responseMessageObj.Content.ReadAsStringAsync();
					SMHandShake _SMHandShake = JsonConvert.DeserializeObject<SMHandShake>(serviceContent);
					Helper.Helper.Hash = _SMHandShake.Hash;
					if (_SMHandShake.StatusId != 1) {
						Console.WriteLine ("HandShake was rejected");
						return false;
					}
				}

				//call the service for updates.
				using (var responseMessageObj = await _service.GetAllConditions ()) {
					serviceContent = await responseMessageObj.Content.ReadAsStringAsync();
					List<SMtblCPCondition> _AllDiseases = JsonConvert.DeserializeObject<List<SMtblCPCondition>>(serviceContent);
					if (await UpdateDBManager.UpdateDiseases (_AllDiseases)) {
						Console.WriteLine ("\n Diseases are updated. \n");
					}
				}

				using (var responseMessageObj = await _service.GetAllCategoriesNoDate ()) {
					serviceContent = await responseMessageObj.Content.ReadAsStringAsync();
					List<SMtblCPCategory> _AllCategories = JsonConvert.DeserializeObject<List<SMtblCPCategory>>(serviceContent);
					if (await UpdateDBManager.UpdateCategories (_AllCategories)) {
						Console.WriteLine ("\n Categories are updated. \n");
					}
				}

				using (var responseMessageObj = await _service.GetConditionCategories()) {
					serviceContent = await responseMessageObj.Content.ReadAsStringAsync();
					List<SMConditionCategories> _ConditionCategory = JsonConvert.DeserializeObject<List<SMConditionCategories>>(serviceContent);
					if (await UpdateDBManager.UpdateDiseasesCategories (_ConditionCategory)) {
						Console.WriteLine ("\n DiseasesForCategory are updated. \n");
					}
				}

                using (var responseMessageObj = await _service.GetVideoLinks())
                {
                    serviceContent = await responseMessageObj.Content.ReadAsStringAsync();
                    var allVideoLinks = JsonConvert.DeserializeObject<List<SMtblVideoLink>>(serviceContent);

                    if (await UpdateDBManager.UpdateVideoLinks(allVideoLinks))
                    {
                        Console.WriteLine("\n Video links are updated.");
                    }
                }

                using (var responseMessageObj = await _service.GoodBye())
				{
				}
			}
			catch (Exception ex) {
				Console.WriteLine ("Exception in SyncConditionsData method: {0}", ex.Message);
			}
			return true;
		}

		public static async Task<Boolean> SyncHospitalsData()
		{
			var AllDevices = await DatabaseManager.SelectAllDevices ();
			Helper.Helper.DeviceId = AllDevices [0].DeviceId;
			Helper.Helper.Hash  = Helper.Helper.generateMD5(Helper.Helper.DeviceId + Helper.Helper.PIN + DateTime.Now.Day);

			try {
				String serviceContent;

				// the initial hand shake to get token to be use in further requests
				using (var responseMessageObj = await _service.HandShake(Helper.Helper.DeviceId, Helper.Helper.Hash)) {
					serviceContent = await responseMessageObj.Content.ReadAsStringAsync();
					SMHandShake _SMHandShake = JsonConvert.DeserializeObject<SMHandShake>(serviceContent);
					Helper.Helper.Hash = _SMHandShake.Hash;
					if (_SMHandShake.StatusId != 1) {
						Console.WriteLine ("HandShake was rejected");
						return false;
					}
				}

				using (var responseMessageObj = await _service.GetHospitalsNoDate ()) {
					serviceContent = await responseMessageObj.Content.ReadAsStringAsync();
					List<SMtblHealthHospital> _AllHospitals = JsonConvert.DeserializeObject<List<SMtblHealthHospital>>(serviceContent);
					if (await UpdateDBManager.UpdateHospitals(_AllHospitals)) {
						Console.WriteLine ("\n Hospitals are updated. \n");
					}
				}

//				using (var responseMessageObj = await _service.GetAllOrgnisationsNoDate ()) {
//					serviceContent = await responseMessageObj.Content.ReadAsStringAsync();
//					List<SMtblHealthOrganizationsInfo> _AllOrganisations = JsonConvert.DeserializeObject<List<SMtblHealthOrganizationsInfo>>(serviceContent);
//					if (await UpdateDBManager.UpdateOrganizations (_AllOrganisations)) {
//						Console.WriteLine ("\n Organisations are updated.");
//					}
//				}

				using (var responseMessageObj = await _service.GoodBye())
				{
				}
			}
			catch (Exception ex) {
				Console.WriteLine ("Exception in SyncHospitalsData method: {0}", ex.Message);
			}
			return true;
		}

		public static async Task<Boolean> SyncOrganisationData()
		{
			var AllDevices = await DatabaseManager.SelectAllDevices ();
			Helper.Helper.DeviceId = AllDevices [0].DeviceId;
			Helper.Helper.Hash  = Helper.Helper.generateMD5(Helper.Helper.DeviceId + Helper.Helper.PIN + DateTime.Now.Day);

			try {
				String serviceContent;

				// the initial hand shake to get token to be use in further requests
				using (var responseMessageObj = await _service.HandShake(Helper.Helper.DeviceId, Helper.Helper.Hash)) {
					serviceContent = await responseMessageObj.Content.ReadAsStringAsync();
					SMHandShake _SMHandShake = JsonConvert.DeserializeObject<SMHandShake>(serviceContent);
					Helper.Helper.Hash = _SMHandShake.Hash;
					if (_SMHandShake.StatusId != 1) {
						Console.WriteLine ("HandShake was rejected");
						return false;
					}
				}

				using (var responseMessageObj = await _service.GetAllOrgnisationsNoDate ()) {
					serviceContent = await responseMessageObj.Content.ReadAsStringAsync();
					List<SMtblHealthOrganizationsInfo> _AllOrganisations = JsonConvert.DeserializeObject<List<SMtblHealthOrganizationsInfo>>(serviceContent);
					if (await UpdateDBManager.UpdateOrganizations (_AllOrganisations)) {
						Console.WriteLine ("\n Organisations are updated.");
					}
				}

				using (var responseMessageObj = await _service.GoodBye())
				{
				}
			}
			catch (Exception ex) {
				Console.WriteLine ("Exception in SyncHospitalsData method: {0}", ex.Message);
			}
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

		public async static Task<Boolean> SendMyProfileData (MyProfile myprofile)
		{
			var service = new WebService ();
			var obj = new HttpResponseMessage();
			//uncomment the line below to input dummy data
			//await inputFakeData ();

			var AllDevices = await DatabaseManager.SelectAllDevices ();
			Helper.Helper.DeviceId = AllDevices [0].DeviceId;
			Helper.Helper.Hash  = Helper.Helper.generateMD5(Helper.Helper.DeviceId + Helper.Helper.PIN + DateTime.Now.Day);

			//the initial hand shake
			obj = await service.HandShake(Helper.Helper.DeviceId, Helper.Helper.Hash);
			content = await obj.Content.ReadAsStringAsync();
			SMHandShake _SMHandShake = JsonConvert.DeserializeObject<SMHandShake>(content);
			Helper.Helper.Hash = _SMHandShake.Hash;
			if (_SMHandShake.StatusId != 1) {
				Console.WriteLine ("HandShake was rejected");
				return false;
			}
			myprofile.DeviceId = Helper.Helper.DeviceId;
			obj = await service.PostMyProfileData (myprofile);
			content = await obj.Content.ReadAsStringAsync ();
			return true;
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

