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
	[Register ("BloodDonationController")]
	partial class BloodDonationController
	{
		[Outlet]
		UIKit.UILabel abMinus { get; set; }

		[Outlet]
		UIKit.UILabel abPlus { get; set; }

		[Outlet]
		UIKit.UILabel aMinus { get; set; }

		[Outlet]
		UIKit.UILabel aPlus { get; set; }

		[Outlet]
		UIKit.UILabel bMinus { get; set; }

		[Outlet]
		UIKit.UILabel bPlus { get; set; }

		[Outlet]
		UIKit.UILabel lblDateMessage { get; set; }

		[Outlet]
		UIKit.UILabel oMinus { get; set; }

		[Outlet]
		UIKit.UILabel oPlus { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel lblTitle { get; set; }

		[Action ("goToIBTSSite:")]
		[GeneratedCode ("iOS Designer", "1.0")]
		partial void goToIBTSSite (UIButton sender);

		void ReleaseDesignerOutlets ()
		{
			if (lblTitle != null) {
				lblTitle.Dispose ();
				lblTitle = null;
			}
		}
	}
}
