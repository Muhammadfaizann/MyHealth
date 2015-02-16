using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MyHealthDB.Helper
{
	public class Helper
	{
		public static string DeviceId = "9412D71E-ED92-4149-991E-5D2F26ED4D8F";
		public static string PIN = "1234";
		public static string Hash = "123123213123213213";


		public static string generateMD5(string input)
		{
			StringBuilder sBuilder = new StringBuilder ();
			using (MD5 md5Hash = MD5.Create ()) {
				byte[] data = md5Hash.ComputeHash (Encoding.UTF8.GetBytes (input));
				for (int slength = 0; slength < data.Length; slength++) {
					sBuilder.Append (data [slength].ToString ("x2"));
				}
			}
			return sBuilder.ToString ();
		}

		public static string GetRegistrationMD5(string DeviceId, string Type, string UserName)
		{
			string myHash = DeviceId + Type + UserName;

			if (DeviceId != null && DeviceId.Length > 0)
				myHash += DeviceId.Substring(0, 1);

			if (Type != null && Type.Length > 0)
				myHash += Type.Substring(0, 1);

			if (UserName != null && UserName.Length > 0)
				myHash += UserName.Substring(0, 1);

			if (DeviceId != null && DeviceId.Length > 1)
				myHash += DeviceId.Substring(DeviceId.Length - 1, 1);

			if (Type != null && Type.Length > 1)
				myHash += Type.Substring(Type.Length - 1, 1);

			if (UserName != null && UserName.Length > 1)
				myHash += UserName.Substring(UserName.Length - 1, 1);
			myHash += DateTime.Now.Month.ToString();
			myHash += DateTime.Now.Day.ToString();

			myHash = Helper.generateMD5(myHash);
			return myHash;

		}

		public static async Task<string> BuildHtmlForDisease(int diseaseId) {
			var selectedDisease = await DatabaseManager.SelectDisease (diseaseId);
			StringBuilder htmlString = new StringBuilder ();
			if (selectedDisease != null) {
				var selectedCpUser = await DatabaseManager.SelectCpUser (selectedDisease.CPUserId);

				htmlString.Append (@"<head><link rel=""stylesheet"" type=""text/css"" href=""css/bootstrap.min.css"">
										<script type=""text/javascript"" src=""js/bootstrap.min.js""></script>
									</head>
									<body> <div style=""padding: 5px"">");

				htmlString.AppendFormat ("<div>{0}</div> <div>{1}</div> <div>{2}</div> <div>{3}</div> ",
					selectedDisease.Name,
					selectedDisease.Description,
					selectedDisease.SignAndSymptoms,
					selectedDisease.PreventiveMeasures);

				if (selectedCpUser != null) {
					htmlString.AppendFormat ("<div><strong>Contact Details</strong><br /><br />{0}, {1}</div> ",
						selectedCpUser.CharityName, selectedCpUser.CharityAddress);

					if (!string.IsNullOrEmpty (selectedCpUser.Number)) {
						htmlString.AppendFormat ("<div>T: {0}</div> ",
							selectedCpUser.Number);
					}
					if (!string.IsNullOrEmpty (selectedCpUser.Fax)) {
						if (!string.IsNullOrEmpty (selectedCpUser.Number)) {
							htmlString.AppendFormat (", F: {0} ",
								selectedCpUser.Fax);
						} else {
							htmlString.AppendFormat ("<div>F: {0}</div> ",
								selectedCpUser.Fax);
						}
					}

					if (!string.IsNullOrEmpty (selectedCpUser.Helpline))
						htmlString.AppendFormat ("<div>Helpline: {0}</div> ",
							selectedCpUser.Helpline);
					if (!string.IsNullOrEmpty (selectedCpUser.LinkToDonate))
						htmlString.AppendFormat (@"<div><strong>Link to Website</strong><br /><br/>Click the link to the website
								<a href=""{0}"">{0}</a></div> ",
							selectedCpUser.LinkToDonate);
				}
				htmlString.Append ("</div> </body>");
			}

			return htmlString.ToString ();
		}
	}

}