using System;
using System.Threading.Tasks;
using System.Net.Http;
using MyHealthDB.WebClient;
using System.Collections.Generic;

namespace MyHealthDB.Service
{
	public class WebService
	{
		public Task<HttpResponseMessage> HandShake(string DeviceId, string Hash)
		{
			//"api/v1/Application/HandShake/{DeviceId}/{Hash}"
			return Client.GetAsync(String.Format("api/v1/Application/HandShake/{0}/{1}", DeviceId, Hash), true );
		}


		public Task<HttpResponseMessage> GoodBye()
		{
			return Client.GetAsync("api/v1/Application/GoodBye");
		}

		public Task<HttpResponseMessage> GetAllConditions ()
		{
			return Client.GetAsync ("api/v1/Condition/GetAll");
		}

		public Task<HttpResponseMessage> GetAllCategories()
		{
			return Client.GetAsync("api/v1/Category/GetAll");
		}

		public Task<HttpResponseMessage> GetAllCategoriesNoDate()
		{
			return Client.GetAsync("api/v1/Category/GetAllNoDate");
		}

		public Task<HttpResponseMessage> GetConditionCategories()
		{
			return Client.GetAsync("api/v1/Condition/GetConditionCategories");
		}

		public Task<HttpResponseMessage> GetAllProvince ()
		{
			return Client.GetAsync ("api/v1/MyHealth/Province/GetAll");
		}

		public Task<HttpResponseMessage> GetAllCounty ()
		{
			return Client.GetAsync ("api/v1/MyHealth/County/GetAll");
		}

		public Task<HttpResponseMessage> GetHospitals()
		{
			return Client.GetAsync ("api/v1/MyHealth/Hospital/GetAll");
		}

		public Task<HttpResponseMessage> GetHospitalsNoDate()
		{
			return Client.GetAsync ("api/v1/MyHealth/Hospital/GetAllNoDate");
		}

		public Task<HttpResponseMessage> GetAllEmergencyNumbers ()
		{
			return Client.GetAsync ("api/v1/MyHealth/EmergencyNumber/GetAll");
		}

		public Task<HttpResponseMessage> GetAllOrgnisations ()
		{
			return Client.GetAsync ("api/v1/MyHealth/OrganizationsInfoService/GetAll");
		}

		public Task<HttpResponseMessage> GetAllOrgnisationsNoDate ()
		{
			return Client.GetAsync ("api/v1/MyHealth/OrganizationsInfoService/GetAllNoDate");
		}

		public Task<HttpResponseMessage> GetAllCpUsers ()
		{
			return Client.GetAsync ("api/v1/CPUser/GetAll");
		}

		public Task<HttpResponseMessage> GetAboutUs ()
		{
			return Client.GetAsync ("api/v1/MyHealth/AboutRCSI/GetAll");
		}

		public Task<HttpResponseMessage> GetImportantNotices ()
		{
			return Client.GetAsync ("api/v1/MyHealth/ImportantNotice/GetAll");
		}

        public Task<HttpResponseMessage> GetVideoLinks()
        {
            return Client.GetAsync("api/v1/MyHealth/videoLinks");
        }

        public Task<HttpResponseMessage> GetMediaCategories()
        {
            return Client.GetAsync("api/v1/MyHealth/mediaCategories");
        }

        public Task<HttpResponseMessage> RegisterDevice (string DeviceId, string Type, string UserName, string Hash, string OSVersion)
		{
			var url = string.Format ("api/v1/Application/RegisterMe/{0}/{1}/{2}/{3}/{4}", DeviceId, Type, UserName, Hash, OSVersion);
			var returnvalue = Client.GetAsync(url, true);
			return returnvalue;
			//return Client.GetAsync (url, true);
		}

		public Task<HttpResponseMessage> PostMyProfileData(MyProfile data)
		{
			string url = string.Format("api/v1/MyHealth/MyProfile/PostData");
			return Client.Post(data,url);

		}

		public Task<HttpResponseMessage> PostLogContent(List<LogContent> data)
		{
			string url = string.Format("api/v1/LogContent/PostAll");
			return Client.Post(data,url);

		}

		public Task<HttpResponseMessage> PostLogContent(List<LogFeedback> data)
		{
			string url = string.Format("api/v1/LogFeedback/PostAll");
			return Client.Post(data, url);
		}

		public Task<HttpResponseMessage> PostLogContent(List<LogExternalLink> data)
		{
			string url = string.Format ("api/v1/LogExternalLink/PostAll");
			return Client.Post (data, url);
		}

		public Task<HttpResponseMessage> PostLogContent(List<LogUsage> data)
		{
			string url = string.Format ("api/v1/LogUsage/PostAll");
			return Client.Post (data, url);
		}

		//		public async Task<HttpResponseMessage> GetApplicationUsersApp()
		//		{
		//			return await Client.GetAsync("api/v1/ApplicationUsersApps/GetAll");
		//		}
		//		public async Task<HttpResponseMessage> ResetSyncDate()
		//		{
		//			return await Client.GetAsync("api/v1/Application/ResetApp");
		//		}
	}
}

