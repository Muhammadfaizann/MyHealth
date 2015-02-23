using System;
using System.Collections.Generic;
using MonoTouch.UIKit;

namespace RCSI
{
	public class PickerModel : UIPickerViewModel
	{
		public IList<string> values;

		public event EventHandler<PickerChangedEventArgs> PickerChanged;

		public PickerModel(IList<string> values)
		{
			this.values = values;
		}

		public override int GetComponentCount (UIPickerView picker)
		{
			return 1;
		}

		public override int GetRowsInComponent (UIPickerView picker, int component)
		{
			return values.Count;
		}

		public override string GetTitle (UIPickerView picker, int row, int component)
		{
			return values[row].ToString ();
		}

		public override float GetRowHeight (UIPickerView picker, int component)
		{
			return 40f;
		}

		public override void Selected (UIPickerView picker, int row, int component)
		{
			if (this.PickerChanged != null)
			{
				this.PickerChanged(this, new PickerChangedEventArgs{SelectedValue = values[row]});
			}
		}
	}

	public class PickerChangedEventArgs : EventArgs{
		public object SelectedValue {get;set;}
	}

	[MonoTouch.Foundation.Register ("MyButton")]
	class MyButton : UIButton {
		UIView input_view;
		UIView input_accessory_view;	

		public MyButton(IntPtr handle) : base(handle)
		{}

		public MyButton() : base()
		{}



		public override UIView InputView {
			get {
				if (input_view == null)
					return base.InputView;
				return input_view;
			}
		}


		public override UIView InputAccessoryView {
			get {
				if (input_accessory_view == null)
					return base.InputAccessoryView;
				return input_accessory_view;
			}
		}

		public void SetInputView (UIView view)
		{
			input_view = view;
		}

		public void SetInputAccesoryView (UIView view)
		{
			input_accessory_view = view;
		}


	}
}

