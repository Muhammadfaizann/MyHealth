using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MyHealthDB.WebClient
{
	public class Client
	{
		private static string baseURL = "http://devrcsiapi.tekenable-test.com:83/";

		public async static Task<HttpResponseMessage> GetAsync(string url, bool anonymous = false)
		{
			try
			{
				using (HttpClient client = new HttpClient())
				{
					client.BaseAddress = new Uri(Client.baseURL);

					// Add an Accept header for JSON format.
					client.DefaultRequestHeaders.Accept.Add(
						new MediaTypeWithQualityHeaderValue("application/json"));

					if (!anonymous)
						// Add an Authorization header for authentication
						client.DefaultRequestHeaders.Authorization =
							new AuthenticationHeaderValue("Basic",
								Convert.ToBase64String(Encoding.UTF8.GetBytes(String.Format("{0}:{1}", Helper.Helper.DeviceId, Helper.Helper.Hash))));

					Console.WriteLine("url : {0}, DeviceID : {1}, Hash : {2}", url, Helper.Helper.DeviceId, Helper.Helper.Hash);

					HttpResponseMessage msg = await client.GetAsync(url);
					msg.EnsureSuccessStatusCode();
					return msg;
				}
			}

			catch (Exception ex)
			{
				Console.WriteLine ("WebServiceException : " + ex);
				return null;
			}
		}

		//post the data using sync service
		public async static Task<HttpResponseMessage> Post<T>(T data, string url) where T : class,new()
		{
			try {
			//string url = string.Format("api/v1/LogContent/PostAll");
				using (HttpClient client = new HttpClient ()) {
					client.BaseAddress = new Uri (Client.baseURL);
					client.DefaultRequestHeaders.Authorization =
						new AuthenticationHeaderValue ("Basic",
						Convert.ToBase64String (Encoding.UTF8.GetBytes (String.Format ("{0}:{1}", Helper.Helper.DeviceId, Helper.Helper.Hash))));
					client.DefaultRequestHeaders.Accept.Add (new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue ("application/json"));
					Console.WriteLine("url : {0}, DeviceID : {1}, Hash : {2}", url, Helper.Helper.DeviceId, Helper.Helper.Hash);
					return await client.PostAsync (url, new StringContent (JsonConvert.SerializeObject (data), System.Text.Encoding.UTF8, "application/json"));

				}
			}
			catch (Exception ex)
			{
				Console.WriteLine ("WebServiceException : " + ex);
				return null;
			}
		}
	}
}

