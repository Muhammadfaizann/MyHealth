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
    [Register ("CategoryHeaderTableViewCell")]
    partial class CategoryHeaderTableViewCell
    {
        [Outlet]
        UIKit.UIButton btnImage { get; set; }


        [Outlet]
        UIKit.UIButton btnTitle { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (btnImage != null) {
                btnImage.Dispose ();
                btnImage = null;
            }

            if (btnTitle != null) {
                btnTitle.Dispose ();
                btnTitle = null;
            }
        }
    }
}