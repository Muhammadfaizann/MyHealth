using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Collections.Generic;

namespace MyHealthDB
{
	public static class ServiceConsumer
	{

		public async static void updateApplication (string DeviceId) 
		{
			var serviceURL = "http://192.168.1.7:40927/";
			using (var client = new HttpClient ()) 
			{
				client.BaseAddress = new Uri (serviceURL);
				client.DefaultRequestHeaders.Accept.Clear ();
				client.DefaultRequestHeaders.Accept.Add (new MediaTypeWithQualityHeaderValue ("application/xml"));

				HttpResponseMessage response = await client.GetAsync ("api/UsefullNumber");
				if (response.IsSuccessStatusCode) 
				{
					Console.Write (response.Content);
					//List<UsefullNumbers> numbers = await response.Content.ReadAsAsync<UsefullNumbers> ();
				}
			}
		}

	}
}

