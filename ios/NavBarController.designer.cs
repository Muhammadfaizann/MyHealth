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
	[Register ("NavBarController")]
	partial class NavBarController
	{
		[Outlet]
		UIKit.UILabel lblTitle { get; set; }

		[Action ("btnShareClicked:")]
		partial void btnShareClicked (Foundation.NSObject sender);

		[Action ("goBack:")]
		partial void goBack (UIKit.UIButton sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (lblTitle != null) {
				lblTitle.Dispose ();
				lblTitle = null;
			}
		}
	}
}
