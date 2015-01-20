using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;
using System.Threading.Tasks;


namespace MyHealthDB
{
	public static class ServiceConsumer
	{

		private static String serviceURL = "http://192.168.1.5:40927/api/UsefullNumber/";
		public static String DeviceID;

		public async static void updateUsefullNumbers (string DeviceId) 
		{

			Console.WriteLine ("Device ID :> " + DeviceID);

			using (var client = new HttpClient ()) 
			{
				client.BaseAddress = new Uri (serviceURL);
				client.DefaultRequestHeaders.Accept.Clear ();
				client.DefaultRequestHeaders.Accept.Add (new MediaTypeWithQualityHeaderValue ("application/xml"));

				HttpResponseMessage response = await client.GetAsync ("allnumbers");
				if (response.IsSuccessStatusCode) 
				{
					Console.Write (response.Content);
					var jsonStream = await response.Content.ReadAsStringAsync ();
					var numberList = JsonConvert.DeserializeObject<List<UsefullNumbers>> (jsonStream);
					foreach (UsefullNumbers num in numberList) 
					{
						UsefullNumberManager.SaveUsefullNumbers (num);
					}
				}
			}
		}
	}
}

