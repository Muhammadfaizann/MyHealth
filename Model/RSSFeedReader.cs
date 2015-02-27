using System;
using System.Collections.Generic;
using System.Net;
using System.IO;
using System.Xml;

namespace MyHealthAndroid
{
	public class RSSFeedReader
	{
		public static List<FeedItem> Read(String url)
		{ 
			List<FeedItem> feedItemsList = new List<FeedItem>();
			try
			{
				WebRequest webRequest = WebRequest.Create(url);
				WebResponse webResponse = webRequest.GetResponse();
				Stream stream = webResponse.GetResponseStream();
				XmlDocument xmlDocument = new XmlDocument();
				xmlDocument.Load(stream);
				XmlNamespaceManager nsmgr = new XmlNamespaceManager(xmlDocument.NameTable);
				nsmgr.AddNamespace("media", xmlDocument.DocumentElement.GetNamespaceOfPrefix("media"));
				XmlNodeList itemNodes = xmlDocument.SelectNodes("rss/channel/item");

				for (int i = 0; i < itemNodes.Count; i++)
				{
					FeedItem feedItem = new FeedItem();

					if (itemNodes[i].SelectSingleNode("title") != null)
					{
						feedItem.Title = itemNodes[i].SelectSingleNode("title").InnerText;
					}
					if (itemNodes[i].SelectSingleNode("link") != null)
					{
						feedItem.Link = itemNodes[i].SelectSingleNode("link").InnerText;
					}
					if (itemNodes[i].SelectSingleNode("pubDate") != null)
					{
						feedItem.PubDate = Convert.ToDateTime(itemNodes[i].SelectSingleNode("pubDate").InnerText);
					}
					if (itemNodes[i].SelectNodes("media:thumbnail", nsmgr).Count > 0 && (itemNodes[i].SelectNodes("media:thumbnail", nsmgr)[0]).Attributes["url"].Value != null)
					{
						feedItem.ImageUrl = (itemNodes[i].SelectNodes("media:thumbnail", nsmgr)[0]).Attributes["url"].Value;
					}
					if (itemNodes[i].SelectSingleNode("description") != null)
					{
						feedItem.Description = itemNodes[i].SelectSingleNode("description").InnerText;
					}

					feedItemsList.Add(feedItem);
				}
			}
			catch (Exception)
			{
				throw;
			}

			return feedItemsList;		
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
}

