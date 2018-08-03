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
    [Register ("FeedbackTableViewController")]
    partial class FeedbackTableViewController
    {
        [Outlet]
        public UIKit.UITextField txtEmail { get; private set; }


        [Outlet]
        public UIKit.UITextField txtMessage { get; private set; }


        [Outlet]
        public UIKit.UITextField txtUserName { get; private set; }

        void ReleaseDesignerOutlets ()
        {
            if (txtEmail != null) {
                txtEmail.Dispose ();
                txtEmail = null;
            }

            if (txtMessage != null) {
                txtMessage.Dispose ();
                txtMessage = null;
            }

            if (txtUserName != null) {
                txtUserName.Dispose ();
                txtUserName = null;
            }
        }
    }
}