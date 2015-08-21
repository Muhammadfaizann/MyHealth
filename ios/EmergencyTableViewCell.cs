// This file has been autogenerated from a class added in the UI designer.

using System;
using MyHealthDB;

using Foundation;
using UIKit;

namespace RCSI
{
	public partial class EmergencyTableViewCell : UITableViewCell
	{
		public EmergencyTableViewCell (IntPtr handle) : base (handle)
		{
		}

		public EmergencyTableViewCell(UITableViewCellStyle style,string cellidentifier) : base(style,cellidentifier)
		{

		}

		public void UpdateCell(EmergencyContacts contact)
		{
			string name = contact.Name;
			string address = contact.Description;
			string tel = contact.PhoneNumber;
			lblName.Text = name;
			lblAddress.Text = address;
			lblAddress.Lines = 8;
			btnTel.TouchUpInside += DialNumber;
			btnTel.SetTitle( tel, UIControlState.Normal);

		}

		public void DialNumber(object sender, EventArgs e)
		{
			string number = ((UIButton)sender).Title (UIControlState.Normal).Replace("Tel: ","").Trim().Replace (" ", "");
			if(!string.IsNullOrEmpty(number))
				UIApplication.SharedApplication.OpenUrl (new NSUrl ("tel:" + number));
		}
	}
}
