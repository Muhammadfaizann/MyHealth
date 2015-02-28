// This file has been autogenerated from a class added in the UI designer.

using System;

using Foundation;
using UIKit;

namespace RCSI
{
	public partial class TopBarController : UIViewController
	{
		public TopBarController (IntPtr handle) : base (handle)
		{
			/*if (this.ParentViewController.GetType ().Equals (typeof(MyProfileController))) {
				btnSettings.RemoveFromSuperview ();
			}*/
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

				UITapGestureRecognizer labelTap = new UITapGestureRecognizer (() => {
					if (this.ParentViewController != null) 
					{						 
						if (this.ParentViewController.NavigationController.ViewControllers.Length > 2)
						{
							UIViewController homeController = this.ParentViewController.NavigationController.ViewControllers [1];						
							this.ParentViewController.NavigationController.PopToViewController (homeController, true);
						}
					}
				});

				lblSmartHealth.UserInteractionEnabled = true;
				lblSmartHealth.AddGestureRecognizer (labelTap);

			if (btnSync != null)
			btnSync.TouchUpInside += async (object sender, EventArgs e) => {
				UIApplication.SharedApplication.NetworkActivityIndicatorVisible = true;

				await MyHealthDB.ServiceConsumer.SyncDevice ();

				UIApplication.SharedApplication.NetworkActivityIndicatorVisible = false;
			};
		}

		partial void goToSettings (UIButton sender)
		{
			if (!this.ParentViewController.GetType().Equals(typeof(MyProfileController)))
			{
				var profileController = (MyProfileController)Storyboard.InstantiateViewController("MyProfileViewController");
				this.ParentViewController.NavigationController.PushViewController(profileController, true);
			}
		}
	}
}