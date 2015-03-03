using System;
using CoreGraphics;

using UIKit;
using Foundation;

namespace RCSI
{
	[Register("TermsDialog")]
	public class TermsDialog : UIView
	{
		public TermsDialog ()
		{
		}

		public TermsDialog(IntPtr handle) : base(handle)
		{
		}

		public override void Draw (CGRect rect)
		{
			base.Draw (rect);
			//var context = UIGraphics.CurrentGraphics ();
		}
	}
}

