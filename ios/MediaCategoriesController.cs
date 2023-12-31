// This file has been autogenerated from a class added in the UI designer.

using System;

using Foundation;
using UIKit;
using MyHealthDB;
using System.Collections.Generic;
using System.Linq;

namespace RCSI
{
	public partial class MediaCategoriesController : UIViewController, IUITableViewDataSource, IUITableViewDelegate
	{
		public MediaCategoriesController (IntPtr handle) : base (handle)
		{
		}

		private IReadOnlyList<MediaCategory> mediaCategories;

		public override async void ViewDidLoad()
		{
			base.ViewDidLoad();
			this.Title = "MyHealth Media";

			mediaCategories = await DatabaseManager.GetAllMediaCategoriesAsync();
			tableView.ReloadData();
		}

		public override void PrepareForSegue(UIStoryboardSegue segue, NSObject sender)
		{
			if ("Videos".Equals(segue.Identifier))
			{
				var mediaCategory = mediaCategories.ElementAt(tableView.IndexPathForSelectedRow.Row);

				var viewController = segue.DestinationViewController as EmergencyController;
				viewController.IsVideos = true;
				viewController.MediaCategoryId = mediaCategory.ID.Value;
				viewController.Title = mediaCategory.CategoryTitle;

				tableView.DeselectRow(tableView.IndexPathForSelectedRow, true);
			}

			base.PrepareForSegue(segue, sender);
		}

		static string cellIdentifier = "MediaCategoryCell";
		public UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
		{
			var cell = tableView.DequeueReusableCell(cellIdentifier);

			cell.TextLabel.Text = mediaCategories[indexPath.Row].CategoryTitle;

			return cell;
		}

		public nint RowsInSection(UITableView tableView, nint section)
		{
			return mediaCategories?.Count ?? 0;
		}

		[Export("tableView:didSelectRowAtIndexPath:")]
		public virtual void RowSelected(UITableView tableView, NSIndexPath indexPath)
		{
			this.PerformSegue("Videos", tableView);
		}
	}
}
