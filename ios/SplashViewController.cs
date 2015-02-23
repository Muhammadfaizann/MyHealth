// This file has been autogenerated from a class added in the UI designer.

using System;

using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace RCSI
{
	public partial class SplashViewController : UIViewController
	{
		Boolean DatabaseExists;

		public SplashViewController (IntPtr handle) : base (handle)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			//bool isAgree = NSUserDefaults.StandardUserDefaults.BoolForKey ("Agree");
//			if (isAgree) {
//				//PerformSegue ("Home", this);
//			}
			DatabaseExists = NSUserDefaults.StandardUserDefaults.BoolForKey ("DatabaseExists");
		}

		public override void ViewDidAppear (bool animated)
		{
			base.ViewDidAppear (animated);

			UIAlertView dialog = new UIAlertView ();
			dialog.Title = "Accept Terms of Use";
			dialog.Message = "Read our terms and conditions";
			dialog.AddButton ("Don't Agree");
			dialog.AddButton ("Agree");
			dialog.CancelButtonIndex = 0;

			dialog.Clicked += async (object sender, UIButtonEventArgs e) => {

				switch (e.ButtonIndex) {
				case 0:
					// don't agree leads in exit of application
					NSThread.Exit();
					break;
				case 1:
					NSUserDefaults defs = NSUserDefaults.StandardUserDefaults;
					defs.SetBool(true, "Agree");
					defs.Synchronize();
					if (DatabaseExists) {
						PerformSegue ("Home", this);
					} else {
						var dbCreated = await MyHealthDB.ServiceConsumer.CreateDatabase("iOS");
						if (dbCreated) {
							//NSUserDefaults userDefaults = NSUserDefaults.StandardUserDefaults;
							//userDefaults.SetBool(true, "DatabaseExists");
							//userDefaults.Synchronize();
							PerformSegue("Home", this);
						}
					};
					break;
				}
			};

			dialog.Show ();
		}

		//------------------------ Get Device ID --------------------//
		private string GetDeviceID ()
		{
			return "";
		}
	}
}
