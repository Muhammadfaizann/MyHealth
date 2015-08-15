using System;
using Android.Widget;
using Android.App;
using System.Collections.Generic;
using MyHealthDB;
using Android.Views;

namespace MyHealthAndroid
{
	public class DiseaseAdapter : BaseAdapter
	{
		List<Disease> diseaseList;
		Activity _activity;

		public DiseaseAdapter (Activity activity, List<Disease> list)
		{
			_activity = activity;
			diseaseList = list;
		}

		public override int Count {
			get { return diseaseList.Count; }
		}

		public override Java.Lang.Object GetItem (int position) {
			// could wrap a Contact in a Java.Lang.Object
			// to return it here if needed

			var disease = diseaseList [position];
			return new WrapDiseaseClass () { Id = disease.ID };
		}

		public override long GetItemId (int position) {
			return (long) diseaseList [position].ID;
		}

		public override View GetView (int position, View convertView, ViewGroup parent)
		{
			var view = convertView ?? _activity.LayoutInflater.Inflate (
				Resource.Layout.SimpleListItem, parent, false);
			var nameLabel = view.FindViewById<TextView> (Resource.Id.firstLine);//Android.Resource.Id.Text1;
			nameLabel.Text = diseaseList[position].Name;
			return view;
		}
	}

	public class WrapDiseaseClass : Java.Lang.Object 
	{
		public int? Id;
	}
}

