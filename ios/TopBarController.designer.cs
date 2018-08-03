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
        partial void goToSettings (UIKit.UIButton sender);

        partial void goToHome(UIKit.UIButton sender);

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

            if (lblRCSIHome != null) {
                lblRCSIHome.Dispose ();
                lblRCSIHome = null;
            }

            if (lblSmartHealth != null) {
                lblSmartHealth.Dispose ();
                lblSmartHealth = null;
            }
        }
    }
}