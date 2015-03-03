// This file has been autogenerated from a class added in the UI designer.

using System;

using Foundation;
using UIKit;

namespace RCSI
{
	public partial class SearchButtonsController : UIViewController
	{
		public SearchButtonsController (IntPtr handle) : base (handle)
		{
		}

		partial void aToZ (NSObject sender)
		{
			//UIStoryboard storyboard = UIStoryboard.FromName("Main", null);

			var navigationController = this.ParentViewController.NavigationController;
			if (this.ParentViewController.GetType().Equals(typeof(CategoryController)))
			{
				navigationController.PopViewController(false);
			}
			var currentController = this.ParentViewController as BeginController;
			if (currentController == null
				|| !currentController.IsAtoZ)
			{
				BeginController viewController = (BeginController)Storyboard.InstantiateViewController("BeginViewController");
				viewController.IsAtoZ = true;
				navigationController.PushViewController(viewController, false);
			}
		}

		partial void category (NSObject sender)
		{
			var currentController = this.ParentViewController as BeginController;
			var navigationController = this.ParentViewController.NavigationController;
			if (currentController != null
				&& currentController.IsAtoZ)
			{
				navigationController.PopViewController(false);
			}
			if (!this.ParentViewController.GetType().Equals(typeof(CategoryController))) {
				CategoryController viewController = (CategoryController)Storyboard.InstantiateViewController("CategoryViewController");
				navigationController.PushViewController(viewController, false);
			}
		}

		partial void recent (NSObject sender)
		{

			var navigationController = this.ParentViewController.NavigationController;
			if (this.ParentViewController is IllnessDetailsController || this.ParentViewController is CategoryController)
			{
				navigationController.PopViewController(false);
			}

			if (!this.ParentViewController.GetType().Equals(typeof(RecentCategoryController))) {
				RecentCategoryController viewController = (RecentCategoryController)Storyboard.InstantiateViewController("RecentCategoryViewController");
				navigationController.PushViewController(viewController, false);
			}
		}
	}
}
