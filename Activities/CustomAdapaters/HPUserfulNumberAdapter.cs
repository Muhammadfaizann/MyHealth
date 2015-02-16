using System;
using Android.Widget;
using System.Collections.Generic;
using Android.App;
using Android.Views;
using Android.Content;
using MyHealthDB;
using System.Threading.Tasks;

namespace MyHealthAndroid
{
	public class HPUserfulNumberAdapter : BaseAdapter
	{
		private List<UsefullNumbers> _list;
		private HPDetailsActivity _activity;

		//constructor
		public HPUserfulNumberAdapter (HPDetailsActivity activity)
		{

				_activity = activity;
		}

		public async Task loadData() {
			_list = await MyHealthDB.DatabaseManager.SelectAllUsefullNumbers ();//DataService.LoadNumbers ();
			if (_list.Count <= 0) {
				await MyHealthDB.DatabaseManager.SaveUsefullNumber (new UsefullNumbers { 
					ID = 0,
					Name = "My GP",
					Number = "1234567890"
				});

				MyHealthDB.DatabaseManager.SaveUsefullNumber (new UsefullNumbers { 
					ID = 1,
					Name = "My Dentist", 
					Number = "+353876416352"
				});

				MyHealthDB.DatabaseManager.SaveUsefullNumber (new UsefullNumbers { 
					ID = 2,
					Name = "My Health Insurer", 
					Number = "1234567890"
				});
				await MyHealthDB.DatabaseManager.SaveUsefullNumber (new UsefullNumbers { 
					ID = 3,
					Name = "My Garda Station",
					Number = ""
				});
				await MyHealthDB.DatabaseManager.SaveUsefullNumber (new UsefullNumbers { 
					ID = 4,
					Name = "My Pharmacy",
					Number = ""
				});
				await MyHealthDB.DatabaseManager.SaveUsefullNumber (new UsefullNumbers { 
					ID = 5,
					Name = "My Public Health Nurse",
					Number = ""
				});
				_list = await MyHealthDB.DatabaseManager.SelectAllUsefullNumbers ();
			}
		}
			
		//count of rows in ListView
		public override int Count {
			get { return _list.Count; }
		}

		public override Java.Lang.Object GetItem (int position) {
			// could wrap a Contact in a Java.Lang.Object
			// to return it here if needed
			return null;
		}

		public override long GetItemId (int position) {
			return -1;
		}

		public override View GetView (int position, View convertView, ViewGroup parent)
		{
			var view = convertView ?? _activity.LayoutInflater.Inflate (
				Resource.Layout.row_hp_useful_numbers, parent, false);
			var contactName = view.FindViewById<TextView> (Resource.Id.hpContactName);
			var contactEdit = view.FindViewById<ImageView> (Resource.Id.hpEditContactImage);
			var contactNumber = view.FindViewById<TextView> (Resource.Id.hpContactNumber);

			contactName.Text = _list[position].Name;
			contactNumber.Text = _list [position].Number;

			contactEdit.Clickable = true;
			contactEdit.Click += (object sender, EventArgs e) => {
				_activity.ShowInputDialog(position);
			};

			return view;
		}
	}
}

