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
    [Register ("EmergencyTableViewCell")]
    partial class EmergencyTableViewCell
    {
        [Outlet]
        UIKit.UIButton btnTel { get; set; }


        [Outlet]
        UIKit.UILabel lblAddress { get; set; }


        [Outlet]
        UIKit.UILabel lblName { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (btnTel != null) {
                btnTel.Dispose ();
                btnTel = null;
            }

            if (lblAddress != null) {
                lblAddress.Dispose ();
                lblAddress = null;
            }

            if (lblName != null) {
                lblName.Dispose ();
                lblName = null;
            }
        }
    }
}