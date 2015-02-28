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
	[Register ("FeedbackTableViewController")]
	partial class FeedbackTableViewController
	{
		[Outlet]
		public UIKit.UITextField txtEmail { get; private set; }

		[Outlet]
		public UIKit.UITextField txtMessage { get; private set; }

		[Outlet]
		public UIKit.UITextField txtUserName { get; private set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (txtEmail != null) {
				txtEmail.Dispose ();
				txtEmail = null;
			}

			if (txtMessage != null) {
				txtMessage.Dispose ();
				txtMessage = null;
			}

			if (txtUserName != null) {
				txtUserName.Dispose ();
				txtUserName = null;
			}
		}
	}
}
