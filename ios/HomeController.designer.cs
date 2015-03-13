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
	[Register ("HomeController")]
	partial class HomeController
	{
		[Outlet]
		UIKit.UILabel lblImportantNoticeMessage { get; set; }

		[Action ("begin_clicked:")]
		partial void begin_clicked (UIKit.UIButton sender);

		[Action ("contacts_clicked:")]
		partial void contacts_clicked (UIKit.UIButton sender);

		[Action ("goBack:")]
		partial void goBack (UIKit.UIButton sender);

		[Action ("myProfile_clicked:")]
		partial void myProfile_clicked (UIKit.UIButton sender);

		[Action ("next_clicked:")]
		partial void next_clicked (UIKit.UIButton sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (lblImportantNoticeMessage != null) {
				lblImportantNoticeMessage.Dispose ();
				lblImportantNoticeMessage = null;
			}
		}
	}
}
