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
    [Register ("OrganDonationsController")]
    partial class OrganDonationsController
    {
        [Outlet]
        UIKit.UIWebView webView { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (webView != null) {
                webView.Dispose ();
                webView = null;
            }
        }
    }
}