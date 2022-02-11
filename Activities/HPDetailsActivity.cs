
using System;
using System.Collections.Generic;
using System.Linq;

using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Views;
using Android.Webkit;
using Android.Widget;
using MyHealthDB;
using MyHealthDB.Helper;
using MyHealthDB.Logger;

namespace MyHealthAndroid
{
    public delegate void ShowHospitalsEventHandler(int province);
    [Activity(Label = "MyHealth", ScreenOrientation = global::Android.Content.PM.ScreenOrientation.Portrait)]
    public class HPDetailsActivity : Activity
    {
        private HPData _caller;
        private CommonData _model;
        private ListView _commonListView;
        private Button _addNumberButton;
        private Button _backButton;
        private WebView _webView;
        private ImageView _imageView;

        private HPUserfulNumberAdapter _contactAdapter;

        protected async override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            _model = new CommonData();
            _caller = _model.GetHealthProfessionals().First(i => i.Id == Intent.GetIntExtra("id", 0));

            //based on the extra that will be receied form last activity
            //all the resource will be set. 
            await SetContentAsPerCaller(_caller);
            SetCustomActionBar();

            var _homeButton = FindViewById<TextView>(Resource.Id.txtAppTitle);
            _homeButton.MovementMethod = Android.Text.Method.LinkMovementMethod.Instance;
            _homeButton.Touch += delegate
            {
                var homeActivity = new Intent(this, typeof(HomeActivity));
                homeActivity.SetFlags(ActivityFlags.ClearTop | ActivityFlags.ClearTask | ActivityFlags.NewTask);

                StartActivity(homeActivity);
            };
        }

        protected override void OnDestroy()
        {
            if (_commonListView != null)
            {
                _commonListView.ItemClick -= MediaCategoryClicked;
            }

            base.OnDestroy();
        }

        //------------------------ custom activity ----------------------//
        public void SetCustomActionBar()
        {
            ActionBar.SetDisplayShowHomeEnabled(false);
            ActionBar.SetDisplayShowTitleEnabled(false);
            ActionBar.SetCustomView(Resource.Layout.actionbar_custom);
            ActionBar.SetDisplayShowCustomEnabled(true);
        }

        //-------------------------------------- Setup Layout --------------------------------------//
        private async Task SetContentAsPerCaller(HPData data)
        {
            switch (data.Id)
            {
                case 0:
                    await setLayoutWithTable(data.DisplayName);
                    break;
                case 1:
                    await setLayoutWithTable(data.DisplayName);
                    break;
                case 2:
                    await setSimpleLayout(data.DisplayName);
                    break;
                case 3:
                    await setLayoutWithTableContacts();
                    break;
                case 4:
                    await setSimpleLayout(data.DisplayName);
                    break;
                case 5:
                    await setLayoutWithTable(data.DisplayName);
                    break;
            }

            //implement the back button 
            _backButton = FindViewById<Button>(Resource.Id.backButton);
            _backButton.Text = data.DisplayName;
            _backButton.Click += (object sender, EventArgs e) =>
            {
                base.OnBackPressed();
            };
        }

        private async Task setLayoutWithTable(string resourceName)
        {
            SetContentView(Resource.Layout.activity_hp_details_table);
            _commonListView = FindViewById<ListView>(Resource.Id.emergencyList);
            if (resourceName.Equals("Emergency"))
            {
                var emergencyAdapter = new HPEmergencyAdapter(this);
                await emergencyAdapter.loadData();
                _commonListView.Adapter = emergencyAdapter;
                await LogManager.Log(new LogUsage
                {
                    Date = DateTime.Now,
                    Page = Convert.ToInt32(Pages.Emergency)
                });
            }
            else if (resourceName.Replace(" ", string.Empty).Equals("MyHealthMedia", StringComparison.InvariantCultureIgnoreCase))
            {
                var mcAdapter = new MediaCategoriesAdapter(this);
                await mcAdapter.loadData();
                _commonListView.Adapter = mcAdapter;
                await LogManager.Log(new LogUsage
                {
                    Date = DateTime.Now,
                    Page = Convert.ToInt32(Pages.MyHealthMediaCategories)
                });

                _commonListView.ItemClick += MediaCategoryClicked;
            }
            else
            {
                var emergencyAdapter = new HPOrgnisationAdapter(this);
                await emergencyAdapter.loadData();
                _commonListView.Adapter = emergencyAdapter;
                await LogManager.Log(new LogUsage
                {
                    Date = DateTime.Now,
                    Page = Convert.ToInt32(Pages.Organisations)
                });
            }
        }

        private void MediaCategoryClicked(object sender, AdapterView.ItemClickEventArgs arg)
        {
            var videoLinksActivity = new Intent(this, typeof(VideoLinksActivity));
            videoLinksActivity.PutExtra("CategoryId", Convert.ToInt32(arg.Id));

            var mcAdapter = _commonListView.Adapter as MediaCategoriesAdapter;
            videoLinksActivity.PutExtra("CategoryTitle", mcAdapter[arg.Position].CategoryTitle);

            StartActivity(videoLinksActivity);
        }

        private async Task setLayoutWithTableContacts()
        {
            SetContentView(Resource.Layout.activity_hp_details_contacts);
            _commonListView = FindViewById<ListView>(Resource.Id.hpUsefulContactList);
            _contactAdapter = new HPUserfulNumberAdapter(this);
            await _contactAdapter.loadData();
            _commonListView.Adapter = _contactAdapter;

            await LogManager.Log(new LogUsage
            {
                Date = DateTime.Now,
                Page = Convert.ToInt32(Pages.MyUsefulNumbers)
            });

            _addNumberButton = FindViewById<Button>(Resource.Id.addContactButton);
            _addNumberButton.Click += onAddButtonClicked;
        }

        private async Task setSimpleLayout(string resourceName)
        {
            SetContentView(Resource.Layout.activity_hp_details_simple);
            _webView = FindViewById<WebView>(Resource.Id.simpleWebView);
            _imageView = FindViewById<ImageView>(Resource.Id.simpleDetailImage);

            if (resourceName.Equals("Hospitals"))
            {
                _imageView.Visibility = ViewStates.Gone;
                _webView.Settings.JavaScriptEnabled = true;
                MyWebViewClient _webClient = new MyWebViewClient();
                _webClient.OnShowHospitals += (int provinceId) =>
                {
                    var intent = new Intent(this, typeof(HospitalsInCountyActivity));
                    intent.PutExtra("provinceId", provinceId);
                    StartActivity(intent);
                };
                _webView.SetWebViewClient(_webClient);
                _webView.SetWebChromeClient(new WebChromeClient());
                _webView.LoadUrl("file:///android_asset/Content/Provinces.html");

                await LogManager.Log(new LogUsage
                {
                    Date = DateTime.Now,
                    Page = Convert.ToInt32(Pages.Hospitals)
                });
            }
            else
            {
                AboutUs aboutus = await MyHealthDB.DatabaseManager.SelectAboutUs(0);
                string htmlString = Helper.BuildHtmlForAboutUs(aboutus);

                _webView.LoadDataWithBaseURL("file:///android_asset/", htmlString, "text/html", "utf-8", null);
                if (aboutus.mainImage != null && aboutus.mainImage.Length > 0)
                {
                    _imageView.SetImageBitmap(BitmapFactory.DecodeByteArray(aboutus.mainImage, 0, aboutus.mainImage.Length));
                }

                await LogManager.Log(new LogUsage
                {
                    Date = DateTime.Now,
                    Page = Convert.ToInt32(Pages.AboutRCSI)
                });
            }
        }

        //-------------------------------------- Event Handlers --------------------------------------//

        protected void onAddButtonClicked(object sender, EventArgs e)
        {
            //this calls the same dialog so it can add new number.
            ShowInputDialog(-1, _contactAdapter.contactList);
        }

        protected void onSaveButtonClicked(object sender, EventArgs e)
        {

        }

        //-------------------------------------- Public functions --------------------------------------//

        public void ShowInputDialog(int index, List<UsefullNumbers> contactList)
        {
            //var contactList = await MyHealthDB.DatabaseManager.SelectAllUsefullNumbers ();
            AlertDialog.Builder alert = new AlertDialog.Builder(this);

            //pass the contactsList from where you call this function. 
            alert.SetTitle("Add / Edit Contact");
            var view = this.LayoutInflater.Inflate(Resource.Layout.alertview_custom_layout, null);
            alert.SetView(view);

            var UserName = view.FindViewById<EditText>(Resource.Id.contactNameInput);
            var UserNumber = view.FindViewById<EditText>(Resource.Id.contactNumberInput);
            if (index >= 0)
            {
                UserName.Text = contactList.ElementAt(index).Name;
                UserNumber.Text = contactList.ElementAt(index).Number;
            }

            alert.SetPositiveButton("Save", async (object sender, DialogClickEventArgs e) =>
            {

                if (!UserName.Text.Equals(""))
                {
                    UsefullNumbers contact;
                    if (index >= 0)
                    {
                        contact = contactList.ElementAt(index);
                        contact.Name = UserName.Text;
                        contact.Number = UserNumber.Text;
                    }
                    else
                    {
                        contact = new UsefullNumbers();
                        contact.ID = contactList.Max(x => x.ID) + 1;
                        contact.Name = UserName.Text;
                        contact.Number = UserNumber.Text;
                    }
                    await MyHealthDB.DatabaseManager.SaveUsefullNumber(contact);
                    _contactAdapter = new HPUserfulNumberAdapter(this);
                    await _contactAdapter.loadData();
                    _commonListView.Adapter = _contactAdapter;
                    Toast.MakeText(this, "Updated usefull numbers list", ToastLength.Long).Show();
                }

            });

            alert.SetNegativeButton("Cancel", (object sender, DialogClickEventArgs e) =>
            {

            });

            alert.Show();
        }

        //-------------------------------------- Private functions --------------------------------------//

        //------------------------ menu item ----------------------//
        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.main_activity_actions, menu);
            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnMenuItemSelected(int featureId, IMenuItem item)
        {
            switch (item.ItemId)
            {

                case Resource.Id.action_profile:
                    var newActivity = new Intent(this, typeof(MyProfileActivity));
                    StartActivity(newActivity);
                    break;
            }
            return base.OnMenuItemSelected(featureId, item);
        }
    }

    public class MyWebViewClient : WebViewClient
    {
        public event ShowHospitalsEventHandler OnShowHospitals;

        public override bool ShouldOverrideUrlLoading(WebView view, IWebResourceRequest WebRequest)
        {
            string url = WebRequest.Url.ToString();
            var provinceName = url.Substring(url.IndexOf("?") + 1);
            DatabaseManager.SelectProvince(provinceName)
                .ContinueWith((r) =>
                {
                    var provinceId = r.Result.ID.Value;

                    if (OnShowHospitals != null)
                    {
                        OnShowHospitals(provinceId);
                    }
                }, TaskScheduler.FromCurrentSynchronizationContext());
            return true;
        }

        /*public override void OnPageStarted (WebView view, string url, Android.Graphics.Bitmap favicon)
		{
			base.OnPageStarted (view, url, favicon);
		}

		public override void OnPageFinished (WebView view, string url)
		{
			base.OnPageFinished (view, url);
		}

		public override void OnReceivedError (WebView view, ClientError errorCode, string description, string failingUrl)
		{
			base.OnReceivedError (view, errorCode, description, failingUrl);
		}*/
    }

}

