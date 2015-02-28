// This file has been autogenerated from a class added in the UI designer.

using System;

using Foundation;
using UIKit;
using MyHealthDB;

namespace RCSI
{
	public partial class NumberTableViewCell : UITableViewCell
	{

		public NumberTableViewCell (IntPtr handle) : base (handle)
		{
		}

		public NumberTableViewCell(UITableViewCellStyle style,string cellidentifier) : base(style,cellidentifier)
		{

		}

		public void UpdateCell(UsefullNumbers info, int index)
		{
			lblTitle.Text = info.Name;
			btnNumber.TouchUpInside += DialNumber;
			btnNumber.SetTitle( info.Number, UIControlState.Normal);
			btnEdit.SetTitle( index.ToString(), UIControlState.Normal);
		}

		public void DialNumber(object sender, EventArgs e)
		{
			string number = ((UIButton)sender).Title (UIControlState.Normal).Trim().Replace (" ", "");
			if(!string.IsNullOrEmpty(number))
				UIApplication.SharedApplication.OpenUrl (new NSUrl ("tel:" + number));
		}

	}
}