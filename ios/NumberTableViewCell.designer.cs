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
    [Register ("NumberTableViewCell")]
    partial class NumberTableViewCell
    {
        [Outlet]
        UIKit.UIButton btnEdit { get; set; }


        [Outlet]
        UIKit.UIButton btnNumber { get; set; }


        [Outlet]
        UIKit.UILabel lblTitle { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (btnEdit != null) {
                btnEdit.Dispose ();
                btnEdit = null;
            }

            if (btnNumber != null) {
                btnNumber.Dispose ();
                btnNumber = null;
            }

            if (lblTitle != null) {
                lblTitle.Dispose ();
                lblTitle = null;
            }
        }
    }
}