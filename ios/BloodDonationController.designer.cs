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
		UIKit.UILabel oMinus { get; set; }

		[Outlet]
		UIKit.UILabel oPlus { get; set; }

		[Action ("goToIBTSSite:")]
		partial void goToIBTSSite (UIKit.UIButton sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (oPlus != null) {
				oPlus.Dispose ();
				oPlus = null;
			}

			if (oMinus != null) {
				oMinus.Dispose ();
				oMinus = null;
			}

			if (aPlus != null) {
				aPlus.Dispose ();
				aPlus = null;
			}

			if (aMinus != null) {
				aMinus.Dispose ();
				aMinus = null;
			}

			if (bPlus != null) {
				bPlus.Dispose ();
				bPlus = null;
			}

			if (bMinus != null) {
				bMinus.Dispose ();
				bMinus = null;
			}

			if (abPlus != null) {
				abPlus.Dispose ();
				abPlus = null;
			}

			if (abMinus != null) {
				abMinus.Dispose ();
				abMinus = null;
			}
		}
	}
}
