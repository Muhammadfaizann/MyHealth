// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

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

		[Outlet]
		UIKit.UIImage lblRCSIHome { get; set; }

		[Action ("syncWithServer:")]
		partial void syncWithServer (Foundation.NSObject sender);

		[Action ("goToSettings:")]
		[GeneratedCode ("iOS Designer", "1.0")]
		partial void goToSettings (UIButton sender);

		[Action ("goToHome:")]
		[GeneratedCode ("iOS Designer", "1.0")]
		partial void goToHome (UIButton sender);

		void ReleaseDesignerOutlets ()
		{
		}
	}
}
