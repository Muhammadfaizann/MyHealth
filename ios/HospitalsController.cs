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
	public partial class HospitalsController : UIViewController
	{
		public int ProvinceId { get; set; }

		private HospitalsContactSource _hospitalsContactSource;
		public HospitalsController (IntPtr handle) : base (handle)
		{
		}

		public async override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			await LogManager.Log<LogUsage> (new LogUsage (){
				Date = DateTime.Now,
				Page = Convert.ToInt32(Pages.Hospitals)
			});

			_hospitalsContactSource = new HospitalsContactSource (this);
			await _hospitalsContactSource.UpdateData ();
			this.tableView.Source = _hospitalsContactSource;
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
		async public void TextChanged (UIKit.UISearchBar searchBar, string searchText)
		{
			var items = await _hospitalsContactSource.AllHospitals();
			_hospitalsContactSource.SearchItems = items.Where (h => h.Name.ToLower().Contains(searchText.ToLower())).ToList ();
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

	public class HospitalsContactSource : UITableViewSource
	{
		private CommonData model = new CommonData();
		private List<Hospital> _allHospitals;
		async public Task<List<Hospital>> AllHospitals() {
				if (_allHospitals == null) {
				_allHospitals = await DatabaseManager.SelectAllHospitals();
			}

			return _allHospitals;
		}
		public List<Hospital> _items; //Hospital.GetDefaultData();
		public List<Hospital> SearchItems;
		HospitalsController _controller;

		public HospitalsContactSource (HospitalsController controller)
		{
			_controller = controller;
		}

		public async Task UpdateData()
		{
			//_items = await model.GetHospitalsInCounty (_controller.ProvinceId);
			_items = await DatabaseManager.SelectHospitalsByProvince (_controller.ProvinceId);
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

		static String cellIdentifier = "HospitalCell";
		public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
		{	
			Hospital item;
			if (SearchItems == null)
				item = _items [indexPath.Row];
			else
				item = SearchItems [indexPath.Row];

			HospitalsTableViewCell cell = null;
			cell = tableView.DequeueReusableCell (cellIdentifier) as HospitalsTableViewCell;
			// if there are no cells to reuse, create a new one
			if (cell == null) {
				cell = new HospitalsTableViewCell (UITableViewCellStyle.Default, cellIdentifier);
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

//	public class Hospital
//	{
//		public  Hospital()
//		{
//		}
//
//		public Hospital(string name, string phone, string website)
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
//		public static List<Hospital> GetDefaultData()
//		{
//			List<Hospital> hospitals = new List<Hospital>();
//			hospitals.Add(new RCSI.Hospital("Beaumont Hospital","+353 1 809 3000", "www.beaumont.ie"));
//			hospitals.Add(new RCSI.Hospital("Mater Hospital","+353 1 803 2000", "www.mater.ie"));
//			hospitals.Add(new RCSI.Hospital("Rotunda hospital","+353 1 807 1700", "www.rotunda.ie"));
//			return hospitals;	
//		}
//	}
}
