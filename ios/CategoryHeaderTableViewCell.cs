// This file has been autogenerated from a class added in the UI designer.

using System;

using Foundation;
using UIKit;

namespace RCSI
{
	public partial class CategoryHeaderTableViewCell : UITableViewCell
	{
		public CategoryHeaderTableViewCell (IntPtr handle) : base (handle)
		{
		}

		public CategoryHeaderTableViewCell(UITableViewCellStyle style,string cellidentifier) : base(style,cellidentifier)
		{

		}

		public event EventHandler ToggleSectionEvent {
			add {

				btnImage.AddTarget (value, UIControlEvent.TouchUpInside);
				btnTitle.AddTarget (value, UIControlEvent.TouchUpInside);
			}
			remove {
				btnImage.RemoveTarget (value, UIControlEvent.TouchUpInside);
				btnTitle.RemoveTarget (value, UIControlEvent.TouchUpInside);
			}
		}

		public void UpdateCell(string headerText,int section,bool isCollapsed)
		{
			btnTitle.SetTitle( "      " + headerText, UIControlState.Normal);
			btnImage.Tag = section;
			btnTitle.Tag = section;
			if (isCollapsed) {
				btnImage.SetImage (UIImage.FromBundle ("images/grey_arrow_right.png"), UIControlState.Normal);
			} else {
				btnImage.SetImage (UIImage.FromBundle ("images/grey_arrow_down.png"), UIControlState.Normal);
			}
		}

		/*public void ToggleSections(object sender, EventArgs e)
		{
			int section = ((UIButton)sender).Tag;
		}*/

	}
}
