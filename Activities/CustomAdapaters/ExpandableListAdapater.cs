using System;
using Android.Widget;
using Android.Views;
using System.Collections.Generic;
using Android.App;
using System.Linq;

using MyHealthDB;

namespace MyHealthAndroid
{
	public class ExpendableListAdapter: BaseExpandableListAdapter
	{
		Dictionary<string, List<Disease>> _dictGroup =null;
		List<string> _lstGroupID = null;
		Activity _activity;

		public ExpendableListAdapter (Activity activity,
			Dictionary<string, List<Disease>> dictGroup)
		{
			_dictGroup = dictGroup;
			_activity = activity;
			_lstGroupID = dictGroup.Keys.ToList();
		}

		#region implemented abstract members of BaseExpandableListAdapter
		public override Java.Lang.Object GetChild (int groupPosition, int childPosition)
		{
			return  _dictGroup [_lstGroupID [groupPosition]] [childPosition].Name;
		}

		public override long GetChildId (int groupPosition, int childPosition)
		{
			return _dictGroup [_lstGroupID [groupPosition]] [childPosition].ID ?? 0;
		}

		public override int GetChildrenCount (int groupPosition)
		{
			return _dictGroup [_lstGroupID [groupPosition]].Count;
		}

		public override bool IsChildSelectable (int groupPosition, int childPosition)
		{
			return true;
		}

		public override Java.Lang.Object GetGroup (int groupPosition)
		{
			return _lstGroupID [groupPosition];
		}

		public override long GetGroupId (int groupPosition)
		{
			return groupPosition;
		}

		public override int GroupCount {
			get {
				return _dictGroup.Count;
			}
		}

		public override View GetGroupView (int groupPosition, bool isExpanded, View convertView, ViewGroup parent)
		{
			var item = _lstGroupID [groupPosition];

			if (convertView == null)
				convertView = _activity.LayoutInflater.Inflate (Resource.Layout.ListHeader, null);

			var textBox = convertView.FindViewById<TextView> (Resource.Id.txtHeader);
			textBox.SetText (item, TextView.BufferType.Normal);

			return convertView;
		}

		public override View GetChildView (int groupPosition, int childPosition, bool isLastChild, View convertView, ViewGroup parent)
		{
			var item = _dictGroup [_lstGroupID [groupPosition]] [childPosition];

			if (convertView == null)
				convertView = _activity.LayoutInflater.Inflate (Resource.Layout.ListChild, null);

			var textBox = convertView.FindViewById<TextView> (Resource.Id.txtSmall);
			textBox.SetText (item.Name, TextView.BufferType.Normal);

			return convertView;
		}


		public override bool HasStableIds {
			get {
				return true;//throw new NotImplementedException ();
			}
		}
		#endregion
	}
}

