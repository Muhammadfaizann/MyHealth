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
    [Register ("NavBarController")]
    partial class NavBarController
    {
        [Outlet]
        UIKit.UILabel lblTitle { get; set; }

        [Action ("btnShareClicked:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void btnShareClicked (UIKit.UIButton sender);

        [Action ("goBack:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void goBack (UIKit.UIButton sender);

        void ReleaseDesignerOutlets ()
        {
            if (lblTitle != null) {
                lblTitle.Dispose ();
                lblTitle = null;
            }
        }
    }
}