// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace RCSI
{
	[Register ("HospitalsMapController")]
	partial class HospitalsMapController
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIWebView webView { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (webView != null) {
				webView.Dispose ();
				webView = null;
			}
		}
	}
}
