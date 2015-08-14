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
	[Register ("FeedbackController")]
	partial class FeedbackController
	{
		[Action ("saveTheFeeback:")]
		[GeneratedCode ("iOS Designer", "1.0")]
		partial void saveTheFeeback (UIButton sender);

		[Outlet]
		UIKit.UITextField username { get; set; }

		[Outlet]
		UIKit.UITextField useremail { get; set; }

		[Outlet]
		UIKit.UITextField usermessage { get; set; }

		void ReleaseDesignerOutlets ()
		{
		}
	}
}
