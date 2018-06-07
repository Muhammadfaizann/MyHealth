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
    [Register ("HomeController")]
    partial class HomeController
    {
        [Outlet]
        UIKit.UILabel lblImportantNoticeMessage { get; set; }

        [Action ("begin_clicked:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void begin_clicked (UIKit.UIButton sender);

        [Action ("contacts_clicked:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void contacts_clicked (UIKit.UIButton sender);

        [Action ("goBack:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void goBack (UIKit.UIButton sender);

        [Action ("myProfile_clicked:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void myProfile_clicked (UIKit.UIButton sender);

        [Action ("next_clicked:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void next_clicked (UIKit.UIButton sender);

        void ReleaseDesignerOutlets ()
        {
            if (lblImportantNoticeMessage != null) {
                lblImportantNoticeMessage.Dispose ();
                lblImportantNoticeMessage = null;
            }
        }
    }
}