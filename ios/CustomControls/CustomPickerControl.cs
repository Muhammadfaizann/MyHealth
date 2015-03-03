using System;
using UIKit;

namespace RCSI
{
	public class CustomPickerControl
	{
		UITextField _txtField;
		UIPickerView _pickerView;
		PickerModel _pickerModel;
		public CustomPickerControl (UITextField txtView, PickerModel pickerModel)
		{
			_txtField = txtView;
			_pickerModel = pickerModel;
			_pickerView =  new UIPickerView ();
		}

		public void SetPicker()
		{
			_pickerView.Model = _pickerModel;
			_pickerView.ShowSelectionIndicator = true;

			UIToolbar toolbar = new UIToolbar ();
			toolbar.BarStyle = UIBarStyle.Black;
			toolbar.Translucent = true;
			toolbar.SizeToFit ();

			UIBarButtonItem doneButton = new UIBarButtonItem("Done",UIBarButtonItemStyle.Done,(s,e) =>
				{
					_txtField.Text = _pickerModel.values[Convert.ToInt16(_pickerView.SelectedRowInComponent (0))].ToString ();
					_txtField.ResignFirstResponder ();	


				});
			toolbar.SetItems (new UIBarButtonItem[]{doneButton},true);		

			_txtField.InputView = _pickerView;
			_txtField.InputAccessoryView = toolbar;
			_txtField.TouchDown += SetPicker;
		}

		private void SetPicker(object sender, EventArgs e)
		{
			UITextField field = (UITextField)sender;
			_pickerView.Select (_pickerModel.values.IndexOf (field.Text), 0, true);
		}
	}
}

