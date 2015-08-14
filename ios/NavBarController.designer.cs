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
	[Register ("NavBarController")]
	partial class NavBarController
	{
		[Outlet]
		UIKit.UILabel lblTitle { get; set; }

		[Action ("btnShareClicked:")]
		[GeneratedCode ("iOS Designer", "1.0")]
		partial void btnShareClicked (UIButton sender);

		[Action ("goBack:")]
		[GeneratedCode ("iOS Designer", "1.0")]
		partial void goBack (UIButton sender);

		void ReleaseDesignerOutlets ()
		{
		}
	}
}
