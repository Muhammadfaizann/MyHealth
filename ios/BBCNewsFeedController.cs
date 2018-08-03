// This file has been autogenerated from a class added in the UI designer.

using System;
using System.IO;
using System.Net;
using System.Xml;
using System.Collections.Generic;

using Foundation;
using UIKit;

namespace RCSI
{
	public enum RssFeedType
	{
		BBCNews = 1,
		PulseVoices = 2,
		IrishHealth = 3,
		irishTimes = 4,
	}

	public partial class BBCNewsFeedController : UIViewController
	{
		public RssFeedType SelectedRssFeedType { get; set; }


		private BBCNewsFeedSource _bbcNewsFeedSource;
		public BBCNewsFeedController (IntPtr handle) : base (handle)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			_bbcNewsFeedSource = new BBCNewsFeedSource (this);
			this.tableView.Source = _bbcNewsFeedSource;
			this.tableView.ClipsToBounds = true;
			this.tableView.SeparatorStyle = UITableViewCellSeparatorStyle.None;

		}
	}

	public class BBCNewsFeedSource : UITableViewSource
	{
		List<FeedItem> _feedItemList;
		BBCNewsFeedController _controller;
		public BBCNewsFeedSource (BBCNewsFeedController controller)
		{
			_controller = controller;

			switch (_controller.SelectedRssFeedType) {
			case RssFeedType.PulseVoices:
				_feedItemList = RSSManager.ReadRSSFeed ("http://pulsevoices.org/index.php?format=feed&type=rss&title=Pulse-Voices%20from%20the%20Heart%20of%20Medicine%20-%20Welcome%20to%20Pulse-Voices%20from%20the%20Heart%20of%20Medicine");
				break;
			case RssFeedType.IrishHealth:
				_feedItemList = RSSManager.ReadRSSFeed ("http://www.irishhealth.com/rss/ihfeed.php");
				break;
			case RssFeedType.irishTimes:
				_feedItemList = RSSManager.ReadRSSFeed ("https://www.irishtimes.com/cmlink/irish-times-health-1.1364620");
				break;
			case RssFeedType.BBCNews:
				_feedItemList = RSSManager.ReadRSSFeed ("http://feeds.bbci.co.uk/news/health/rss.xml?edition=uk#");
				break;
			default:
				_feedItemList = RSSManager.ReadRSSFeed ("http://feeds.bbci.co.uk/news/health/rss.xml?edition=uk#");
				break;
			}
		}

		public override nint NumberOfSections (UITableView tableView)
		{
			return 1;
		}

		public override nint RowsInSection (UITableView tableView, nint section)
		{
			return _feedItemList.Count;
		}
		//-- generate apk file
		static String cellIdentifier = "NewsCell";
		public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
		{
			UITableViewCell cell = null;
			cell = tableView.DequeueReusableCell (cellIdentifier);
			// if there are no cells to reuse, create a new one
			if (cell == null) {
				cell = new UITableViewCell (UITableViewCellStyle.Default, cellIdentifier);
				//cell.Accessory = UITableViewCellAccessory.DisclosureIndicator;
			}
			cell.Layer.CornerRadius = 30.0f;
			cell.Layer.BorderWidth = 1.5f;
			cell.Layer.MasksToBounds = true;
			cell.Layer.BorderColor = UIColor.FromRGB(233,239,239).CGColor;
			cell.BackgroundColor = UIColor.FromRGB(233,239,239);
			cell.IndentationWidth = 2.0f;
			((UILabel)cell.ViewWithTag (100)).Text = (_feedItemList [indexPath.Row].PubDate == null || _feedItemList [indexPath.Row].PubDate == DateTime.MinValue ? "" : Math.Round(DateTime.Now.Subtract(_feedItemList [indexPath.Row].PubDate).TotalHours).ToString() + " hours ago");
			((UILabel)cell.ViewWithTag (100)).TextColor = UIColor.Gray;

			UITextView lblTitle = ((UITextView)cell.ViewWithTag (101));
			lblTitle.UserInteractionEnabled = false;
			lblTitle.Text = _feedItemList [indexPath.Row].Title;
			lblTitle.TextColor = UIColor.FromRGB (72, 95, 98);
			lblTitle.BackgroundColor = UIColor.FromRGB (233, 239, 239);

			UITextView lblDescription = ((UITextView)cell.ViewWithTag (102));
			lblDescription.UserInteractionEnabled = false;
//			lblDescription.Text = _feedItemList [indexPath.Row].Description;
			lblDescription.TextColor = UIColor.FromRGB (160, 160, 160);
			lblDescription.BackgroundColor = UIColor.FromRGB (233, 239, 239);

			NSError error = null;
			NSString str = new NSString (_feedItemList [indexPath.Row].Description);
			NSAttributedStringDocumentAttributes importParams = new NSAttributedStringDocumentAttributes();
			importParams.DocumentType = NSDocumentType.HTML;
			NSAttributedString attrString = new NSAttributedString (str.Encode (NSStringEncoding.UTF8), importParams, ref error);
			lblDescription.AttributedText = attrString;

			if (!string.IsNullOrEmpty (_feedItemList [indexPath.Row].ImageUrl)) {
				NSUrl nsUrl = new NSUrl (_feedItemList [indexPath.Row].ImageUrl);
				NSData data = NSData.FromUrl (nsUrl);
				try {
					var myImage = new UIImage (data); 
					((UIImageView)cell.ViewWithTag (103)).Image = myImage;
				} catch (Exception ex) {
					Console.WriteLine ("Image Loaded Exception : {0}", ex.ToString ());
				}
			}

			return cell;
		}

		public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
		{
			string url = _feedItemList [indexPath.Row].Link;

            UIAlertView alert = new UIAlertView { Title = "Alert", Message = "you will now be redirected to an external website, Do you want to Proceed?" };
            alert.AddButton("OK");
            alert.AddButton("Cancel");
            alert.CancelButtonIndex = 1;
			alert.Clicked += (s, b) => {
				if(b.ButtonIndex == 0)
					// open in Safari
					UIApplication.SharedApplication.OpenUrl(new NSUrl(url));
				// return false so the UIWebView won't load the web page
			};
			alert.Show();

			//_controller.HideKeyboard ();
			//_controller.PerformSegue ("Details", tableView);
			tableView.DeselectRow (indexPath, true);
		}
	}

	public class FeedItem  { 

		public FeedItem() { }

		public string Title { get; set; }
		public string Link { get; set; }
		public DateTime PubDate { get; set; }
		public string ImageUrl { get; set; }
		public string Description { get; set; }
	}

	public class RSSManager
	{
		

		public static List<FeedItem> ReadRSSFeed(String url)
		{ 
			List<FeedItem> feedItemsList = new List<FeedItem>();
			NetworkStatus remoteHostStatus, internetStatus, localWifiStatus;

			remoteHostStatus = Reachability.RemoteHostStatus ();
			internetStatus = Reachability.InternetConnectionStatus ();
			localWifiStatus = Reachability.LocalWifiConnectionStatus ();
			var connected = (remoteHostStatus != NetworkStatus.NotReachable) && (internetStatus != NetworkStatus.NotReachable) || (localWifiStatus != NetworkStatus.NotReachable);
			if (connected) {
				
				try {
					WebRequest webRequest = WebRequest.Create (url);
					WebResponse webResponse = webRequest.GetResponse ();
					Stream stream = webResponse.GetResponseStream ();
					XmlDocument xmlDocument = new XmlDocument ();
					xmlDocument.Load (stream);
					XmlNamespaceManager nsmgr = new XmlNamespaceManager (xmlDocument.NameTable);
					nsmgr.AddNamespace ("media", xmlDocument.DocumentElement.GetNamespaceOfPrefix ("media"));
					XmlNodeList itemNodes = xmlDocument.SelectNodes ("rss/channel/item");

					for (int i = 0; i < itemNodes.Count; i++) {
						FeedItem feedItem = new FeedItem ();

						if (itemNodes [i].SelectSingleNode ("title") != null) {
							feedItem.Title = itemNodes [i].SelectSingleNode ("title").InnerText;
						}
						if (itemNodes [i].SelectSingleNode ("link") != null) {
							feedItem.Link = itemNodes [i].SelectSingleNode ("link").InnerText;
						}
						if (itemNodes [i].SelectSingleNode ("pubDate") != null) {
							feedItem.PubDate = Convert.ToDateTime (itemNodes [i].SelectSingleNode ("pubDate").InnerText);
						}
						if (itemNodes [i].SelectNodes ("media:thumbnail", nsmgr).Count > 0 && (itemNodes [i].SelectNodes ("media:thumbnail", nsmgr) [0]).Attributes ["url"].Value != null) {
							feedItem.ImageUrl = (itemNodes [i].SelectNodes ("media:thumbnail", nsmgr) [0]).Attributes ["url"].Value;
						}
						if (itemNodes [i].SelectSingleNode ("description") != null) {
							feedItem.Description = itemNodes [i].SelectSingleNode ("description").InnerText;
						}

						feedItemsList.Add (feedItem);
					}
				} catch (Exception) {
					throw;
				}
			}

			return feedItemsList;		
		}
	}
}
