// This file has been autogenerated from a class added in the UI designer.

using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;
using MyHealthDB;
using MyHealthDB.Logger;
using System.Threading.Tasks;

namespace RCSI
{
	public partial class OrganisationsController : UIViewController
	{
		private OrganisationsContactSource _organisationsContactSource;
		public OrganisationsController (IntPtr handle) : base (handle)
		{

		}

		public async override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			await LogManager.Log<LogUsage> (new LogUsage (){
				Date = DateTime.Now,
				Page = Convert.ToInt32(Pages.Organisations)
			});

			_organisationsContactSource = new OrganisationsContactSource (this);
			await _organisationsContactSource.UpdateData ();
			this.tableView.Source = _organisationsContactSource;
			this.tableView.ReloadData ();

			UITapGestureRecognizer gestureRecognizer = new UITapGestureRecognizer (HideKeyboard);
			this.tableView.AddGestureRecognizer (gestureRecognizer);

			//this.tableView.Scrolled += (sender, e) => this.searchBar.ResignFirstResponder ();
		}

		public void HideKeyboard ()
		{
			this.searchBar.ResignFirstResponder ();
		}

		[Export ("searchBar:textDidChange:")]
		public void TextChanged (UIKit.UISearchBar searchBar, string searchText)
		{
			_organisationsContactSource.SearchItems = _organisationsContactSource._items.Where (h => h.Name.ToLower().Contains(searchText.ToLower())).ToList ();
			this.tableView.ReloadData ();
		}

		[Export ("searchBarSearchButtonClicked:")]
		public void SearchButtonClicked (UIKit.UISearchBar searchBar)
		{
			this.HideKeyboard ();
		}

		[Export ("searchDisplayController:didLoadSearchResultsTableView:")]
		public void DidLoadSearchResults (UIKit.UISearchDisplayController controller, UIKit.UITableView tableView)
		{
			tableView.Frame = this.tableView.Frame;
		}
	}

	public class OrganisationsContactSource : UITableViewSource
	{
		private CommonData model = new CommonData();
		public List<Organisation> _items; // = Organisation.GetDefaultData();
		public List<Organisation> SearchItems;
		OrganisationsController _controller;

		public OrganisationsContactSource (OrganisationsController controller)
		{
			_controller = controller;
		}

		public async Task UpdateData ()
		{
			_items = await model.GetOrgnisations ();
		}

		public override nint NumberOfSections (UITableView tableView)
		{
			return 1;
		}

		public override nint RowsInSection (UITableView tableView, nint section)
		{
			if (this.SearchItems == null) {
				return _items.Count;
			} else {
				if (this.SearchItems == null) {
					return 0;
				}
				return this.SearchItems.Count;
			}
		}

		static String cellIdentifier = "OrganisationCell";
		public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
		{
			Organisation item;
			if (SearchItems == null)
				item = _items [indexPath.Row];
			else
				item = SearchItems [indexPath.Row];

			OrganisationsTableViewCell cell = null;
			cell = tableView.DequeueReusableCell (cellIdentifier) as OrganisationsTableViewCell;
			// if there are no cells to reuse, create a new one
			if (cell == null) {
				cell = new OrganisationsTableViewCell (UITableViewCellStyle.Default, cellIdentifier);
				cell.Accessory = UITableViewCellAccessory.DisclosureIndicator;
			}
			cell.UpdateCell (item.Name, item.PhoneNumber, item.URL);
			return cell;
		}

		public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
		{
			_controller.HideKeyboard ();
		}
	}

//	public class Organisation
//	{
//		public  Organisation()
//		{
//		}
//
//		public Organisation(string name, string phone, string website)
//		{
//			Name = name;
//			Phone = phone;
//			Website = website;
//		}
//
//		public string Name { get; set; }
//		public string Phone { get; set; }
//		public string Website { get; set; }
//
//		public static List<Organisation> GetDefaultData()
//		{
//			List<Organisation> organisations = new List<Organisation>();
//			organisations.Add(new Organisation("Irish Heart Foundation","1890 432 787", "www.irisheart.ie"));
//			organisations.Add(new Organisation("Irish Cancer Society","1800 200 700", "www.cancer.ie"));
//			organisations.Add(new Organisation("Diabetes Ireland","1890 909 909", "www.diabetes.ie"));
//			organisations.Add(new Organisation("Aware","016617211", "www.aware.ie"));
//
//			return organisations;	
//		}
//	}
}
