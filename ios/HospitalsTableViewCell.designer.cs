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
    [Register ("HospitalsTableViewCell")]
    partial class HospitalsTableViewCell
    {
        [Outlet]
        UIKit.UIButton btnTel { get; set; }


        [Outlet]
        UIKit.UIButton btnWebsite { get; set; }


        [Outlet]
        UIKit.UILabel lblName { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (btnTel != null) {
                btnTel.Dispose ();
                btnTel = null;
            }

            if (btnWebsite != null) {
                btnWebsite.Dispose ();
                btnWebsite = null;
            }

            if (lblName != null) {
                lblName.Dispose ();
                lblName = null;
            }
        }
    }
}