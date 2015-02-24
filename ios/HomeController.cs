// This file has been autogenerated from a class added in the UI designer.

using System;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.Threading.Tasks;

using MyHealthDB;
using MyHealthDB.Logger;

namespace RCSI
{
	public partial class HomeController : UIViewController
	{
		Boolean FirstTimeInstall;

		public HomeController (IntPtr handle) : base (handle)
		{
		}

		partial void begin_clicked (UIButton sender)
		{
		}

		partial void contacts_clicked (UIButton sender)
		{
		}

		partial void myProfile_clicked (UIButton sender)
		{
		}

		partial void next_clicked (UIButton sender)
		{
		}

		public async override void ViewDidLoad() {
			base.ViewDidLoad ();
			FirstTimeInstall = NSUserDefaults.StandardUserDefaults.BoolForKey ("MyHealthFirstInstall");
			UIAlertView _alert = new UIAlertView (null, "Internet is not accessible, please check your device settings and try again", null, "Ok", null);
			if (!FirstTimeInstall) {
				UIApplication.SharedApplication.NetworkActivityIndicatorVisible = true;

				var isRegistered = await MyHealthDB.ServiceConsumer.CheckRegisteredDevice ();
				if (!isRegistered) {
					_alert.Clicked += async (object sender, UIButtonEventArgs e) => {
						// no internet connection leads in exit of application
						NSThread.Exit();
					};
					var isInternetAvailable = await HelperMethods.CheckIfInternetAvailable ();
					if (!isInternetAvailable) {
						_alert.Show();
					}
					isRegistered =  await MyHealthDB.ServiceConsumer.RegisterDevice (Guid.NewGuid().ToString());
					if (!isRegistered) {
						_alert.Message = "Unable to register device, please try again later";
						_alert.Show();
					}
				}

				var isSyncSuccessful = await MyHealthDB.ServiceConsumer.SyncDevice ();
				if (!isSyncSuccessful) {
					_alert.Message = "Unable to Sync device with server, please try again later";
					_alert.Show();
				}

				UIApplication.SharedApplication.NetworkActivityIndicatorVisible = false;
			}

			await LogManager.Log<LogUsage> (new LogUsage (){ 
				Date = DateTime.Now, 
				Page = Convert.ToInt32(Pages.Home)
			});
		}

		partial void goBack (UIButton sender)
		{
			this.NavigationController.PopViewControllerAnimated(true);
		}
	}
}
