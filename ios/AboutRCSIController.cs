// This file has been autogenerated from a class added in the UI designer.

using System;
using System.IO;
using Foundation;
using UIKit;
using MyHealthDB;
using MyHealthDB.Logger;
using MyHealthDB.Helper;

namespace RCSI
{
	public partial class AboutRCSIController : UIViewController
	{
		public AboutRCSIController (IntPtr handle) : base (handle)
		{
		}

		public async override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			await LogManager.Log<LogUsage> (new LogUsage (){
				Date = DateTime.Now,
				Page = Convert.ToInt32(Pages.MyUsefulNumbers)
			});

			AboutUs aboutus = await MyHealthDB.DatabaseManager.SelectAboutUs (0);
			string htmlString = Helper.BuildHtmlForAboutUs(aboutus);

			//webView.LoadDataWithBaseURL ("file:///android_asset/", htmlString, "text/html", "utf-8", null);

			//String fileName = "Content/AboutRCSI.html";
			//String localHtmlUrl = Path.Combine (NSBundle.MainBundle.BundlePath, fileName);
			string contentDirectoryPath = Path.Combine (NSBundle.MainBundle.BundlePath, "Content/");
			webview.LoadHtmlString(htmlString, new NSUrl(contentDirectoryPath, true));
			//webview.LoadRequest (new NSUrlRequest (new NSUrl(localHtmlUrl, false)));
			webview.ScalesPageToFit = false;
			webview.ShouldStartLoad = HandleShouldStartLoad;

			//imageView.Image = UIImage.LoadFromData (NSData.FromArray (aboutus.mainImage));//UIImage.FromFile ("images/RCSI_Front_Building_1.jpg");
			//imageView.ContentMode = UIViewContentMode.ScaleAspectFit;
			//webview.BackgroundColor = UIColor.Clear;
			//webview.Opaque = false;
			//webview.InsertSubview (new UIImageView(UIImage.FromBundle("images/RCSI_Front_Building_1.jpg")),0);
			//webview.BackgroundColor = UIColor.FromPatternImage (UIImage.FromFile ("images/RCSI_Front_Building_1.jpg"));

		}

		public bool HandleShouldStartLoad(UIWebView webView, NSUrlRequest request, UIWebViewNavigationType navigationType)
		{
			// you need to implement this method depending on your criteria
			if (navigationType.Equals(UIWebViewNavigationType.LinkClicked))
			{

                UIAlertView alert = new UIAlertView { Title = "Alert", Message = "This link will take you to an external website, Do you want to Proceed?" };
                alert.AddButton("OK");
                alert.AddButton("Cancel");
                alert.CancelButtonIndex = 1;
				alert.Clicked += (s, b) => {
					if(b.ButtonIndex == 0) {
						LogManager.Log<LogExternalLink> (new LogExternalLink (){ 
							Date = DateTime.Now, 
							Link = request.Url.AbsoluteString 
						});
						// open in Safari
						UIApplication.SharedApplication.OpenUrl(request.Url);
					}
					// return false so the UIWebView won't load the web page
				};
				alert.Show();

				return false;
			}

			// this.OpenInExternalBrowser(request) returned false -> let the UIWebView load the request
			return true;
		}
	}
}
