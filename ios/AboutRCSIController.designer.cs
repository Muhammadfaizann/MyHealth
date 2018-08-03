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
    [Register ("AboutRCSIController")]
    partial class AboutRCSIController
    {
        [Outlet]
        UIKit.UIImageView imageView { get; set; }


        [Outlet]
        UIKit.UIWebView webview { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (webview != null) {
                webview.Dispose ();
                webview = null;
            }
        }
    }
}