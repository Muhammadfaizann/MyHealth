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
	[Register ("MyProfileController")]
	partial class MyProfileController
	{
		[Outlet]
		UIKit.UIButton btnCalBMI { get; set; }

		[Outlet]
		UIKit.UIButton btnSave { get; set; }

		/*[Outlet]
		MonoTouch.UIKit.UIButton btnSyncWithServer { get; set; }*/

		[Outlet]
		UIKit.UISwitch metricAnswer { get; set; }

		[Outlet]
		UIKit.UITextField txtAge { get; set; }

		[Outlet]
		UIKit.UITextField txtBloodGroup { get; set; }

		[Outlet]
		UIKit.UITextField txtCounty { get; set; }

		[Outlet]
		UIKit.UITextField txtGender { get; set; }

		[Outlet]
		UIKit.UITextField txtHeightFt { get; set; }

		[Outlet]
		UIKit.UITextField txtHeightInc { get; set; }

		[Outlet]
		UIKit.UITextField txtWeightLbs { get; set; }

		[Outlet]
		UIKit.UITextField txtWeightSt { get; set; }

		[Action ("syncWithServer:")]
		partial void syncWithServer (Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (btnCalBMI != null) {
				btnCalBMI.Dispose ();
				btnCalBMI = null;
			}

			if (metricAnswer != null) {
				metricAnswer.Dispose ();
				metricAnswer = null;
			}

			if (txtAge != null) {
				txtAge.Dispose ();
				txtAge = null;
			}

			if (txtBloodGroup != null) {
				txtBloodGroup.Dispose ();
				txtBloodGroup = null;
			}

			if (txtCounty != null) {
				txtCounty.Dispose ();
				txtCounty = null;
			}

			if (txtGender != null) {
				txtGender.Dispose ();
				txtGender = null;
			}

			if (txtHeightFt != null) {
				txtHeightFt.Dispose ();
				txtHeightFt = null;
			}

			if (txtHeightInc != null) {
				txtHeightInc.Dispose ();
				txtHeightInc = null;
			}

			if (txtWeightLbs != null) {
				txtWeightLbs.Dispose ();
				txtWeightLbs = null;
			}

			if (txtWeightSt != null) {
				txtWeightSt.Dispose ();
				txtWeightSt = null;
			}

			if (btnSave != null) {
				btnSave.Dispose ();
				btnSave = null;
			}

			/*if (btnSyncWithServer != null) {
				btnSyncWithServer.Dispose ();
				btnSyncWithServer = null;
			}*/
		}
	}
}
