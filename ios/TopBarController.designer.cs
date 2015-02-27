// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace RCSI
{
	[Register ("TopBarController")]
	partial class TopBarController
	{
		[Outlet]
		UIKit.UIButton btnSettings { get; set; }

		[Outlet]
		UIKit.UIButton btnSync { get; set; }

		[Outlet]
		UIKit.UILabel lblSmartHealth { get; set; }

		[Action ("goToSettings:")]
		partial void goToSettings (UIKit.UIButton sender);

		[Action ("syncWithServer:")]
		partial void syncWithServer (Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (btnSettings != null) {
				btnSettings.Dispose ();
				btnSettings = null;
			}

			if (btnSync != null) {
				btnSync.Dispose ();
				btnSync = null;
			}

			if (lblSmartHealth != null) {
				lblSmartHealth.Dispose ();
				lblSmartHealth = null;
			}
		}
	}
}
