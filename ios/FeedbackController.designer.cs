// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace RCSI
{
	[Register ("FeedbackController")]
	partial class FeedbackController
	{
		[Outlet]
		UIKit.UITextField useremail { get; set; }

		[Outlet]
		UIKit.UITextField usermessage { get; set; }

		[Outlet]
		UIKit.UITextField username { get; set; }

		[Action ("saveTheFeeback:")]
		partial void saveTheFeeback (Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (username != null) {
				username.Dispose ();
				username = null;
			}

			if (useremail != null) {
				useremail.Dispose ();
				useremail = null;
			}

			if (usermessage != null) {
				usermessage.Dispose ();
				usermessage = null;
			}
		}
	}
}
