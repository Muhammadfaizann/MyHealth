// This file has been autogenerated from a class added in the UI designer.

using System;
using System.Collections.Generic;
using System.Linq;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MyHealthDB;
using System.Threading.Tasks;

using MyHealthDB.Logger;

namespace RCSI
{
	public partial class BeginController : UIViewController, IUISearchBarDelegate
	{
		public BeginController (IntPtr handle) : base (handle)
		{
		}

		private IllnessSource _illnessSource;
		public Boolean IsAtoZ { get; set; }
		public Disease _selectedDisease;

		public async override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			await LogManager.Log<LogUsage> (new LogUsage { 
				Date = DateTime.Now, 
				Page = Convert.ToInt32(Pages.HealthSearch)
			});

			_illnessSource = new IllnessSource (this);
			await _illnessSource.LoadData ();
			var recentDiseasesIds = HelperMethods.GetRecentDiseases ();
			if (recentDiseasesIds != null && recentDiseasesIds.Count () > 0) {
				HelperMethods.RecentDiseases = _illnessSource.disease.Where (d => recentDiseasesIds.Contains (d.ID.Value)).ToList ();
			}
			if (this.IsAtoZ) {
				this.tableView.TableHeaderView.RemoveFromSuperview ();
				this.tableView.TableHeaderView = null;

				//this.tableView.Source = new IllnessIndexedSource (this, _illnessSource._items);
				this.tableView.Source = new IllnessIndexedSource (this, _illnessSource.disease);
				this.searchBar.Hidden = true;

				this.Title = "A-Z";
			} else {
				this.tableView.Source = _illnessSource;
			}

			this.tableView.ReloadData ();

			/*UITapGestureRecognizer gestureRecognizer = new UITapGestureRecognizer (HideKeyboard);
			this.tableView.AddGestureRecognizer (gestureRecognizer);*/

			//this.tableView.Scrolled += (sender, e) => this.searchBar.ResignFirstResponder ();
		}
		public void HideKeyboard ()
		{
			this.searchBar.ResignFirstResponder ();
		}

		[Export ("searchBar:textDidChange:")]
		public void TextChanged (MonoTouch.UIKit.UISearchBar searchBar, string searchText)
		{
			_illnessSource.SearchItems = _illnessSource.disease.Where (i => i.Name.ToLower().Contains(searchText.ToLower())).ToArray ();
			this.tableView.ReloadData ();
		}

		[Export ("searchBarSearchButtonClicked:")]
		public void SearchButtonClicked (MonoTouch.UIKit.UISearchBar searchBar)
		{
			this.HideKeyboard ();
		}

		[Export ("searchDisplayController:didLoadSearchResultsTableView:")]
		public void DidLoadSearchResults (MonoTouch.UIKit.UISearchDisplayController controller, MonoTouch.UIKit.UITableView tableView)
		{
			tableView.Frame = this.tableView.Frame;
		}

		public override void PrepareForSegue (UIStoryboardSegue segue, NSObject sender)
		{
			base.PrepareForSegue (segue, sender);

			var controller = segue.DestinationViewController as IllnessDetailsController;
			if (controller != null) 
			{
				controller.SelectedDisease = _selectedDisease;
			}		
		}
	}

	public class IllnessSource : UITableViewSource
	{
//		public String[] _items = {"Obesity", "Depression", "Heart Attack", "Lung Cancer","Heart Bypass", "Heart Failure", "Heart Murmurs", "Heart Valve Infection",
//			"Diabeties", "Asthma", "Appendicitis", "Baby acne", "Burns", "Cold sores", "Dementia"};

		//public List<string> diseaseNameList;
		public Disease[] SearchItems;
		BeginController _controller;
		public List<Disease> disease;

		public IllnessSource (BeginController controller)
		{
			_controller = controller;

		}

		public async Task LoadData() 
		{
			disease = await MyHealthDB.DatabaseManager.SelectAllDiseases ();
			//diseaseNameList = disease.Select(x => x.Name).ToList<string>();
		}

		public override int NumberOfSections (UITableView tableView)
		{
			return 1;
		}

		public override int RowsInSection (UITableView tableView, int section)
		{
			//if (this._controller.SearchDisplayController.SearchResultsTableView == tableView)
			if (this.SearchItems == null) {
				return disease.Count; //_items.Length;
			} else {
				if (this.SearchItems == null) {
					return 0;
				}
				return this.SearchItems.Length;
			}
		}

		static String cellIdentifier = "illnessCell";
		public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
		{
			String item;
			if (SearchItems == null) {
				item = disease.ElementAt (indexPath.Row).Name;//_items [indexPath.Row];
			} else {
				item = SearchItems [indexPath.Row].Name;
			}
			UITableViewCell cell = null;
			cell = tableView.DequeueReusableCell (cellIdentifier);
			// if there are no cells to reuse, create a new one
			if (cell == null) {
				cell = new UITableViewCell (UITableViewCellStyle.Default, cellIdentifier);
				cell.Accessory = UITableViewCellAccessory.DisclosureIndicator;
			}
			cell.TextLabel.Text = item;
			return cell;
		}

		public async override void RowSelected (UITableView tableView, NSIndexPath indexPath)
		{
			_controller.HideKeyboard ();

			/*if (diseaseNameList [indexPath.Row].ToLower () == "lung cancer" || (SearchItems != null && SearchItems [indexPath.Row].ToLower () == "lung cancer")) 
			{
				_controller._selectedDisease = DiseaseList.LungCancer;
			}
			else 
			{
				_controller._selectedDisease = DiseaseList.HeartAttack;
			}*/
			Disease itemSelected;
			if (SearchItems != null && SearchItems.Count () > 0) {
				itemSelected = SearchItems [indexPath.Row];
			} else {
				itemSelected = disease [indexPath.Row];
			}
			await LogManager.Log (new LogContent { 
				Date = DateTime.Now,
				ConditionId = itemSelected.ID,
				CategoryId = itemSelected.DiseaseCategoryID
			});

			_controller._selectedDisease = itemSelected;

			_controller.PerformSegue ("Details", tableView);
			tableView.DeselectRow (indexPath, true);

		}
	}

	public class IllnessIndexedSource : UITableViewSource
	{
		public String[] indexTitles = {@"A", @"B", @"C", @"D", @"E", @"F", @"G", @"H", @"I", @"J", @"K", @"L", @"M", @"N", @"O", @"P", @"Q", @"R", @"S", @"T", @"U", @"V", @"W", "X", "Y", "Z"};
		//public String[] _items = {"Obesity", "Depression", "Heart Attack", "Diabeties", "Asthma"};
		public Dictionary<String, Disease[]> _items;
		//public Dictionary<String, Disease[]> _dictForLog;

		BeginController _controller;
		public IllnessIndexedSource (BeginController controller, List<Disease> items)//String[] items)
		{
			_controller = controller;

			_items = new Dictionary<string, Disease[]> ();
			//_dictForLog = new Dictionary<string, Disease[]> ();
			foreach (var alphabet in indexTitles) {
				//var foundItems = items.Where (i => i.StartsWith (alphabet) || i.StartsWith (alphabet.ToLower ())).OrderBy(i => i).ToArray ();
				//var foundItems = items.Where (i => i.Name.StartsWith (alphabet) || i.Name.StartsWith (alphabet.ToLower ())).OrderBy(i => i).ToArray ();
				var foundItems = items.Where (i => i.Name.ToUpper().StartsWith (alphabet.ToUpper())).OrderBy(i => i.Name).ToArray ();
				if (foundItems != null && foundItems.Length > 0) {
					_items.Add (alphabet, foundItems);
				}
				/*var founds = items.Where (x => x.Name.StartsWith (alphabet) || x.Name.StartsWith (alphabet.ToLower ())).ToArray ();
				if (founds != null && founds.Length > 0) {
					_dictForLog.Add (alphabet, founds);
				}*/
			}
		}

		public override int NumberOfSections (UITableView tableView)
		{
			return _items.Keys.Count;
		}

		public override int RowsInSection (UITableView tableView, int section)
		{
			return _items [_items.Keys.ElementAt (section)].Length;
		}

		public override string[] SectionIndexTitles (UITableView tableView)
		{
			return indexTitles;
		}

		public override int SectionFor (UITableView tableView, string title, int atIndex)
		{
			return _items.Keys.ToList ().IndexOf (title);
		}

		static String cellIdentifier = "illnessIndexCell";
		public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
		{
			var item = _items [_items.Keys.ElementAt (indexPath.Section)] [indexPath.Row];
			UITableViewCell cell = null;
			cell = tableView.DequeueReusableCell (cellIdentifier);
			// if there are no cells to reuse, create a new one
			if (cell == null) {
				cell = new UITableViewCell (UITableViewCellStyle.Default, cellIdentifier);
				//cell.Accessory = UITableViewCellAccessory.DisclosureIndicator;
			}
			cell.TextLabel.Text = item.Name;
			return cell;
		}

		public async override void RowSelected (UITableView tableView, NSIndexPath indexPath)
		{
			/*if (_items [_items.Keys.ElementAt (indexPath.Section)] [indexPath.Row].ToLower () == "lung cancer" ) {
				_controller._selectedDisease = DiseaseList.LungCancer;
			} 
			else 
			{
				_controller._selectedDisease = DiseaseList.HeartAttack;
			}*/
			var dis = _items [_items.Keys.ElementAt (indexPath.Section)] [indexPath.Row];
			await LogManager.Log (new LogContent {
				Date = DateTime.Now, 
				ConditionId = dis.ID,
				CategoryId = dis.DiseaseCategoryID
			});

			_controller._selectedDisease = dis;

			_controller.PerformSegue ("Details", tableView);
			tableView.DeselectRow (indexPath, true);
		}
	}
}
