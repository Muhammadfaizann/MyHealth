using System;
using System.Threading.Tasks;
using System.Net.Http;
using MyHealthDB.WebClient;
using System.Collections.Generic;

namespace MyHealthDB.Service
{
	public class WebService
	{
		public async Task<HttpResponseMessage> HandShake(string DeviceId, string Hash)
		{
			//"api/v1/Application/HandShake/{DeviceId}/{Hash}"
			return await Client.GetAsync(String.Format("api/v1/Application/HandShake/{0}/{1}", DeviceId, Hash), true );
		}


		public async Task<HttpResponseMessage> GoodBye()
		{
			return await Client.GetAsync("api/v1/Application/GoodBye");
		}

		public async Task<HttpResponseMessage> GetAllConditions ()
		{
			return await Client.GetAsync ("api/v1/Condition/GetAll");
		}

		public async Task<HttpResponseMessage> GetAllCategories()
		{
			return await Client.GetAsync("api/v1/Category/GetAll");
		}

		public async Task<HttpResponseMessage> GetConditionCategories()
		{
			return await Client.GetAsync("api/v1/Condition/GetConditionCategories");
		}

		public async Task<HttpResponseMessage> GetAllCounty ()
		{
			return await Client.GetAsync ("api/v1/MyHealth/County/GetAll");
		}

		public async Task<HttpResponseMessage> GetHospitals()
		{
			return await Client.GetAsync ("api/v1/MyHealth/Hospital/GetAll");
		}

		public async Task<HttpResponseMessage> GetAllEmergencyNumbers ()
		{
			return await Client.GetAsync ("api/v1/MyHealth/EmergencyNumber/GetAll");
		}

		public async Task<HttpResponseMessage> GetAllOrgnisations ()
		{
			return await Client.GetAsync ("api/v1/MyHealth/OrganizationsInfoService/GetAll");
		}

		public async Task<HttpResponseMessage> RegisterDevice (string DeviceId, string Type, string UserName, string Hash)
		{
			var url = string.Format ("api/v1/Application/RegisterMe/{0}/{1}/{2}/{3}", DeviceId, Type, UserName, Hash);
			return await Client.GetAsync (url, true);
		}

		public async Task<HttpResponseMessage> PostLogContent(List<LogContent> data)
		{
			string url = string.Format("api/v1/LogContent/PostAll");
			return await Client.Post(data,url);

		}

		public async Task<HttpResponseMessage> PostLogContent(List<LogFeedback> data)
		{
			string url = string.Format("api/v1/LogFeedback/PostAll");
			return await Client.Post(data, url);
		}

		public async Task<HttpResponseMessage> PostLogContent(List<LogExternalLink> data)
		{
			string url = string.Format("api/v1/LogExternalLink/PostAll");
			return await Client.Post(data, url);
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

