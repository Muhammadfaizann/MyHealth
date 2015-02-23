// This file has been autogenerated from a class added in the UI designer.

using System;

using MonoTouch.Foundation;
using MonoTouch.UIKit;

using MyHealthDB;
using MyHealthDB.Logger;

namespace RCSI
{
	public partial class EmergencyController : UIViewController
	{
		private EmergencyContactSource _emergencyContactSource;
		public EmergencyController (IntPtr handle) : base (handle)
		{
		}

		async public override void ViewDidLoad() {
			base.ViewDidLoad ();

			await LogManager.Log<LogUsage> (new LogUsage (){
				Date = DateTime.Now,
				Page = Convert.ToInt32(Pages.Emergency)
			});
		
			_emergencyContactSource = new EmergencyContactSource (this);
			this.tableView.Source = _emergencyContactSource;


			/*UITapGestureRecognizer gestureRecognizer = new UITapGestureRecognizer (HideKeyboard);
			this.tableView.AddGestureRecognizer (gestureRecognizer);*/

			//this.tableView.Scrolled += (sender, e) => this.searchBar.ResignFirstResponder ();
		}
	}

	public class EmergencyContactSource : UITableViewSource
	{
		public String[,] _items = new String[,] {{"Emergency","Emergency police Fire Ambulance", "112"}, {"KDOC","Kildare and West Wicklow Doctors on Call", "1890 599 362"},
			{"NEDOC","North East Doctor on Call", "1890 777 911"}, {"SouthDoc","Cork and Kerry", "1850 335 999"}};
		public String[] SearchItems;
		EmergencyController _controller;
		public EmergencyContactSource (EmergencyController controller)
		{
			_controller = controller;
		}

		public override int NumberOfSections (UITableView tableView)
		{
			return 1;
		}

		public override int RowsInSection (UITableView tableView, int section)
		{
			return _items.GetLength(0);
		}

		static String cellIdentifier = "EmergencyCell";
		public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
		{
			EmergencyTableViewCell cell = null;
			cell = tableView.DequeueReusableCell (cellIdentifier) as EmergencyTableViewCell;
			// if there are no cells to reuse, create a new one
			if (cell == null) {
				cell = new EmergencyTableViewCell (UITableViewCellStyle.Default, cellIdentifier);
				cell.Accessory = UITableViewCellAccessory.DisclosureIndicator;
			}
			cell.UpdateCell (_items [indexPath.Row, 0], _items [indexPath.Row, 1], _items [indexPath.Row, 2]);

			return cell;
		}

		public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
		{
			//_controller.HideKeyboard ();
			//_controller.PerformSegue ("Details", tableView);
			//tableView.DeselectRow (indexPath, true);
		}
	}
}
