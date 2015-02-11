using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using MyHealthDB.Service;
using System.Net.Http;
using MyHealthDB.Model;
using Newtonsoft.Json;

namespace MyHealthDB.Logger
{
	public static class LogManager
	{
		private static string content = "";
		public async static Task Log<T> (T log)
		{
			try {
				if (log.GetType () == typeof(LogContent)) {
					var logContent = log as LogContent;
					await DatabaseManager.SaveLogContent (logContent);
				}
				if (log.GetType () == typeof(LogUsage)) {
					var logUsage = log as LogUsage;
					await DatabaseManager.SaveLogUsage (logUsage);
				}
				if (log.GetType () == typeof(LogExternalLink)) {
					var logExternalLink = log as LogExternalLink;
					await DatabaseManager.SaveLogExternalLink (logExternalLink);
				}
				if (log.GetType() == typeof(LogUsage)) {
					var logUsage = log as LogUsage;
					await DatabaseManager.SaveLogUsage(logUsage);
				}
			} catch {
				// supressing any exception
			}
		}

		public async static Task<Boolean> SyncAllLogs ()
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

			var logContent = await DatabaseManager.SelectLogContent ();
			var logExternalLink = await DatabaseManager.SelectLogExternalLinkList ();
			var logFeedback = await DatabaseManager.SelectLogFeedbackList ();
			var logUsage = await DatabaseManager.SelectLogUsageList ();

			obj = await service.PostLogContent(logContent);
			content = await obj.Content.ReadAsStringAsync ();
			if (!content.Equals("Saved")) { return false; }
			await DatabaseManager.DeleteLogContentList (logContent);
			obj = await service.PostLogContent(logExternalLink);
			content = await obj.Content.ReadAsStringAsync ();
			if (!content.Equals("Saved")) { return false; }
			await DatabaseManager.DeleteLogExternalLinkList (logExternalLink);
			obj = await service.PostLogContent(logFeedback);
			content = await obj.Content.ReadAsStringAsync ();
			if (!content.Equals("Saved")) { return false; }
			await DatabaseManager.DeleteLogFeedbackList (logFeedback);
			obj = await service.PostLogContent (logUsage);
			content = await obj.Content.ReadAsStringAsync ();
			if (!content.Equals ("Saved")) { return false; }
			await DatabaseManager.DeleteLogUsageList (logUsage);

			return true;
		}

		private async static Task<Boolean> inputFakeData() 
		{
			try {
				Random r = new Random();
				for (var count=0; count < 100; count++) {
					await DatabaseManager.SaveLogContent( new LogContent {
						AppId = 1,
						ConditionId = ((count % 2) ==  1)? 1 : 2,
						CategoryId = ((count % 2) ==  1)? 1 : 2,
						Date = DateTime.Now,
						ID = 0
					});

					await DatabaseManager.SaveLogExternalLink ( new LogExternalLink {
						AppId = 1,
						Date = DateTime.Now,
						Link = string.Format("http://www.linnk{0}.com", count),
						ID = 0
					});

					await DatabaseManager.SaveLogFeedback ( new LogFeedback {

						Date = DateTime.Now,
						FeedbackText = "Some good old feedback text.",
						ID = 0
					});

					await DatabaseManager.SaveLogUsage(new LogUsage {
						Date = DateTime.Now,
						Page = r.Next(10),
						ID = 0
					});
				}

			} catch (Exception ex) {
				Console.WriteLine ();
				return false;
			}
			return true;
		}
	}

	public enum Pages {
		Home = 1,
		HealthSearch = 2,
		HealthProfessionals = 3,
		HealthNews = 4,
		IWantToHelp = 5,
		HealthProfessionalDetails = 6,

		BloodDonation = 7,
		OrganDonors = 8,
		Feedback = 9,
		MyProfile = 10,
	}
}

