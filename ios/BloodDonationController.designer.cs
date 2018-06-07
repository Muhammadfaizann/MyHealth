// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
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
        UIKit.UILabel lblTitle { get; set; }

        [Action ("goToIBTSSite:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void goToIBTSSite (UIKit.UIButton sender);

        void ReleaseDesignerOutlets ()
        {
            if (abMinus != null) {
                abMinus.Dispose ();
                abMinus = null;
            }

            if (abPlus != null) {
                abPlus.Dispose ();
                abPlus = null;
            }

            if (aMinus != null) {
                aMinus.Dispose ();
                aMinus = null;
            }

            if (aPlus != null) {
                aPlus.Dispose ();
                aPlus = null;
            }

            if (bMinus != null) {
                bMinus.Dispose ();
                bMinus = null;
            }

            if (bPlus != null) {
                bPlus.Dispose ();
                bPlus = null;
            }

            if (lblDateMessage != null) {
                lblDateMessage.Dispose ();
                lblDateMessage = null;
            }

            if (lblTitle != null) {
                lblTitle.Dispose ();
                lblTitle = null;
            }

            if (oMinus != null) {
                oMinus.Dispose ();
                oMinus = null;
            }

            if (oPlus != null) {
                oPlus.Dispose ();
                oPlus = null;
            }
        }
    }
}