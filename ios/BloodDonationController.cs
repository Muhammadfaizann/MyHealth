// This file has been autogenerated from a class added in the UI designer.

using System;

using Foundation;
using UIKit;

using MyHealthDB;
using MyHealthDB.Logger;

namespace RCSI
{
	public partial class BloodDonationController : UIViewController
	{
		public BloodDonationController (IntPtr handle) : base (handle)
		{
		}

		public async override void ViewDidLoad() {
			base.ViewDidLoad ();
			await LogManager.Log<LogUsage> (new LogUsage (){ 
				Date = DateTime.Now, 
				Page = Convert.ToInt32(Pages.BloodDonation)
			});
		}

		partial void goToIBTSSite (UIKit.UIButton sender)
		{
			UIApplication.SharedApplication.OpenUrl(NSUrl.FromString("http://www.giveblood.ie"));
		}
	}
}
