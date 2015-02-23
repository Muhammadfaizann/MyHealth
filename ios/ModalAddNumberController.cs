// This file has been autogenerated from a class added in the UI designer.

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using MonoTouch.Foundation;
using MonoTouch.UIKit;

using MyHealthDB;

namespace RCSI
{
	public partial class ModalAddNumberController : UIViewController
	{
		public delegate Task NumberAddedDelegate(string title, string number);
		public delegate void NumberEditDelegate(string title, string number, int rowIndex);
		public NumberAddedDelegate NumberAddedHandler;
		public NumberEditDelegate NumberEditedHandler;
		public string EditTitle, EditNumber;
		public int EditRowIndex = -1;
		public int NumberID = -1;

		public ModalAddNumberController (IntPtr handle) : base (handle)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			this.btnCancel.TouchUpInside += async (s, e) => {
				await this.DismissViewControllerAsync (true);
			};

			if (!string.IsNullOrEmpty (EditTitle)) 
			{
				this.Title = "Edit Number";
				txtTitle.Text = EditTitle;
			}

			if(!string.IsNullOrEmpty(EditNumber))
				txtNumber.Text = EditNumber;

			AddNumberTexTFieldDelegate textFieldDelegate = new AddNumberTexTFieldDelegate (this);
			txtTitle.Delegate = textFieldDelegate;
			txtNumber.Delegate = textFieldDelegate;

			this.btnAdd.TouchUpInside += AddNumber;
		}

		private async void AddNumber(object sender, EventArgs e)
		{
			UsefullNumbers number = new UsefullNumbers ();
			if (NumberID <= 0) {
				number.ID = NumberID;
			}
			number.Name = txtTitle.Text.Trim ();
			number.Number = txtNumber.Text.Trim ();

			await MyHealthDB.DatabaseManager.SaveUsefullNumber (number);

			UIAlertView _message = new UIAlertView ("", "Saved Successfully!", null, "Ok", null);
			_message.Show ();

			_message.Clicked += async delegate {
				if (NumberAddedHandler != null)
					await NumberAddedHandler (txtTitle.Text.Trim (), txtNumber.Text.Trim ());
				else if (NumberEditedHandler != null)
					NumberEditedHandler (txtTitle.Text.Trim (), txtNumber.Text.Trim (), EditRowIndex);

				await this.DismissViewControllerAsync (true);
			};
		}

		private class AddNumberTexTFieldDelegate : UITextFieldDelegate
		{
			ModalAddNumberController _controller;
			public AddNumberTexTFieldDelegate (ModalAddNumberController controller)
			{
				_controller = controller;
			}

			#region [TextField Delegate Methods]

			[Export ("textFieldShouldReturn:")]
			public bool ShouldReturn (MonoTouch.UIKit.UITextField textField)
			{
				textField.ResignFirstResponder();
				if (textField == _controller.txtTitle) {
					_controller.txtNumber.BecomeFirstResponder ();
				}
				return false;
			}

			#endregion
		}
	}
}
