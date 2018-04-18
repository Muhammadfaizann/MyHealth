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
		private static string baseURL = "http://myhealthapp.ie/MyHealth_Webservices/";	//http://devrcsiapi.tekenable-test.com:83/";

        private static HttpClient _client;

        static Client()
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri(Client.baseURL);
            // Add an Accept header for JSON format.
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue ("application/json"));
        }

        ~Client()
        {
            _client?.Dispose();
            _client = null;
        }

		public async static Task<HttpResponseMessage> GetAsync(string url, bool anonymous = false)
		{
			try
			{
                if (anonymous)
                    _client.DefaultRequestHeaders.Authorization = null;
                else
						// Add an Authorization header for authentication
					_client.DefaultRequestHeaders.Authorization =
							new AuthenticationHeaderValue("Basic",
								Convert.ToBase64String(Encoding.UTF8.GetBytes(String.Format("{0}:{1}", Helper.Helper.DeviceId, Helper.Helper.Hash))));

					Console.WriteLine("url : {0}, DeviceID : {1}, Hash : {2}", url, Helper.Helper.DeviceId, Helper.Helper.Hash);

				HttpResponseMessage msg = await _client.GetAsync(url);
					msg.EnsureSuccessStatusCode();
					return msg;
				}

			catch (Exception ex)
			{
				Console.WriteLine ("WebServiceException : " + ex);
				return null;
			}
		}

		//post the data using sync service
		public static Task<HttpResponseMessage> Post<T>(T data, string url) where T : class,new()
		{
			try {
				_client.DefaultRequestHeaders.Authorization =
						new AuthenticationHeaderValue ("Basic",
						Convert.ToBase64String (Encoding.UTF8.GetBytes (String.Format ("{0}:{1}", Helper.Helper.DeviceId, Helper.Helper.Hash))));
					Console.WriteLine("url : {0}, DeviceID : {1}, Hash : {2}", url, Helper.Helper.DeviceId, Helper.Helper.Hash);
				return _client.PostAsync (url, new StringContent (JsonConvert.SerializeObject (data), Encoding.UTF8, "application/json"));
			}
			catch (Exception ex)
			{
				Console.WriteLine ("WebServiceException : " + ex);
				return null;
			}
		}
	}
}

