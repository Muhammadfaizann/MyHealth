// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System;
using System.CodeDom.Compiler;

namespace RCSI
{
	[Register ("NavBarController")]
	partial class NavBarController
	{
		[Outlet]
		MonoTouch.UIKit.UILabel lblTitle { get; set; }

		[Action ("goBack:")]
		[GeneratedCode ("iOS Designer", "1.0")]
		partial void goBack (UIButton sender);

		void ReleaseDesignerOutlets ()
		{
		}
	}
}
