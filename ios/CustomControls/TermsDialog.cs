using System;
using System.Drawing;

using MonoTouch.UIKit;
using MonoTouch.Foundation;

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

		public override void Draw (RectangleF rect)
		{
			base.Draw (rect);
			//var context = UIGraphics.CurrentGraphics ();
		}
	}
}

