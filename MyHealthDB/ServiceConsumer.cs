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


namespace MyHealthDB
{
	public static class ServiceConsumer
	{
		private static WebService _service;
		private static HttpResponseMessage obj;
		private static string content = "";

		public async static Task<Boolean> RegisterDevice (String device)
		{
			_service = new WebService ();
			obj = new HttpResponseMessage();

			string DeviceId = Guid.NewGuid().ToString();
			string UserName = "Name" + DateTime.Now.Ticks.ToString();
			string Type = device; //DateTime.Now.Second % 2 == 0 ? "Android" : "IPhone";
			string Hash = Helper.Helper.GetRegistrationMD5(DeviceId, Type, UserName);
			obj = await _service.RegisterDevice(DeviceId, Type, UserName, Hash);
			content = await obj.Content.ReadAsStringAsync();
			SMApplicationUsersApp _SMtblRegisterDevice = JsonConvert.DeserializeObject<SMApplicationUsersApp>(content);

			//Save UserName, DeviceType, DeviceId into Database.
			//Use these for SMHandShake Call

			await DatabaseManager.SaveDevice (new RegisteredDevice {
				ID = _SMtblRegisterDevice.ID,
				UserName = _SMtblRegisterDevice.UserName,
				DeviceId = _SMtblRegisterDevice.DEVICE_ID,
				DeviceType = _SMtblRegisterDevice.Type
			});

			return true;
		}

		public async static Task<Boolean> CreateDatabase(String device = "Android")
		{
			await MyHealthDB.DatabaseManager.CreateAllTables ();
			await RegisterDevice (device);
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
			Helper.Helper.DeviceId = AllDevices [0].DeviceId;
			Helper.Helper.Hash  = Helper.Helper.generateMD5(Helper.Helper.DeviceId + Helper.Helper.PIN + DateTime.Now.Day);

			//the initial hand shake
			obj = await _service.HandShake(Helper.Helper.DeviceId, Helper.Helper.Hash);
			content = await obj.Content.ReadAsStringAsync();
			SMHandShake _SMHandShake = JsonConvert.DeserializeObject<SMHandShake>(content);
			Helper.Helper.Hash = _SMHandShake.Hash;
			if (_SMHandShake.StatusId != 1) {
				Console.WriteLine ("HandShake was rejected");
				return false;
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

			//obj = await _service.GoodBye();
			//TODO : Make a table and save successfull sync date. 
			return true;
		}
	}
}

