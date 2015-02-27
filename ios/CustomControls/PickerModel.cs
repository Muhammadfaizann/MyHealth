using System;
using System.Collections.Generic;
using UIKit;

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

		public override nint GetComponentCount (UIPickerView picker)
		{
			return 1;
		}

		public override nint GetRowsInComponent (UIPickerView picker, nint component)
		{
			return values.Count;
		}

		public override string GetTitle (UIPickerView picker, nint row, nint component)
		{
			return values[Convert.ToInt16(row)].ToString ();
		}

		public override nfloat GetRowHeight (UIPickerView picker, nint component)
		{
			return 40f;
		}

		public override void Selected (UIPickerView picker, nint row, nint component)
		{
			if (this.PickerChanged != null)
			{
				this.PickerChanged(this, new PickerChangedEventArgs{SelectedValue = values[Convert.ToInt16(row)]});
			}
		}
	}

	public class PickerChangedEventArgs : EventArgs{
		public object SelectedValue {get;set;}
	}

	[Foundation.Register ("MyButton")]
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

