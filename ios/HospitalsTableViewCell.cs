// This file has been autogenerated from a class added in the UI designer.

using System;

using Foundation;
using UIKit;
using CoreGraphics;
using MyHealthDB.Logger;
using MyHealthDB;

namespace RCSI
{
	public partial class HospitalsTableViewCell : UITableViewCell
	{
		public HospitalsTableViewCell ()
		{
			
		}

		public HospitalsTableViewCell (IntPtr handle) : base (handle)
		{
		}

		public HospitalsTableViewCell(UITableViewCellStyle style,string cellidentifier) : base(style,cellidentifier)
		{

		}

		public void UpdateCell(string name, string tel,string website)
		{
			/*UIFont cellFont = lblName.Font;
			CGSize constraintSize = new CGSize(lblName.Bounds.Size.Width, float.MaxValue);
			//CGSize labelSize = name.SizeWithFont(cellFont, constraintSize, UILineBreakMode.WordWrap);

			CGSize labelSize = lblName.SizeThatFits (constraintSize);
			//lblName.Frame = new CGRect(lblName.Frame.X, lblName.Frame.Y, lblName.Frame.Width, labelSize.Height);
			lblName.SizeToFit ();*/


			lblName.Text = name;
			btnWebsite.SetTitle( "Website: " + website, UIControlState.Normal);
			btnWebsite.TouchUpInside -= OpenWebSite;
			btnWebsite.TouchUpInside += OpenWebSite;
			btnTel.TouchUpInside -= DialNumber;
			btnTel.TouchUpInside += DialNumber;
			btnTel.SetTitle( "Tel: " + tel, UIControlState.Normal);
		}

		public void DialNumber(object sender, EventArgs e)
		{
			string number = ((UIButton)sender).Title (UIControlState.Normal).Replace("Tel: ","").Trim().Replace (" ", "");
			if(!string.IsNullOrEmpty(number))
				UIApplication.SharedApplication.OpenUrl (new NSUrl ("telprompt://" + number));
		}

		public void OpenWebSite(object sender, EventArgs e)
		{
			string siteUrl = ((UIButton)sender).Title (UIControlState.Normal).Replace("Website: ","").Trim().Replace (" ", "");
			if (!string.IsNullOrEmpty (siteUrl)) {
				if (!siteUrl.StartsWith ("http://", StringComparison.InvariantCultureIgnoreCase)) {
					siteUrl = "http://" + siteUrl;
				}
				UIAlertView alert = new UIAlertView ();
                alert.Title = "Alert";
                alert.Message = "This link will take you to an external website, Do you want to Proceed?";
                alert.AddButton("Ok");
                alert.AddButton("Cancel");

				alert.Clicked += (s, b) => {
					if(b.ButtonIndex == 0) {
						 LogManager.Log<LogExternalLink> (new LogExternalLink (){ 
							Date = DateTime.Now, 
							Link = siteUrl
								
						});
						// open in Safari
						UIApplication.SharedApplication.OpenUrl (new NSUrl (siteUrl));
					}
					// return false so the UIWebView won't load the web page
				};
				alert.Show();
				alert.Dispose ();
			}
		}

		public nfloat GetHeight (string name, string tel,string website)
		{
			UIFont cellFont = lblName.Font;
			CGSize constraintSize = new CGSize(lblName.Bounds.Size.Width, float.MaxValue);
			//CGSize labelSize = name.SizeWithFont(cellFont, constraintSize, UILineBreakMode.WordWrap);

			CGSize labelSize = lblName.SizeThatFits (constraintSize);

			var cellCurrentHeight = this.Frame.Height;
			var labelCurrentHeight = lblName.Frame.Height;

			if (labelSize.Height > labelCurrentHeight) {
				var diff = labelSize.Height - labelCurrentHeight;
				cellCurrentHeight += diff;
			}

			return cellCurrentHeight;
		}
	}
}
