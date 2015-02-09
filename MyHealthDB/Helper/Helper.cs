using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

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
	}

}