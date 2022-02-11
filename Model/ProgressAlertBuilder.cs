using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace MyHealthAndroid.Model
{
    public static class ProgressAlertBuilder
    {
        public static AlertDialog getProgressDialog(Context _AppContext)
        {
            int LayoutPadding = 10;
            LinearLayout Layout = new LinearLayout(_AppContext);
            Layout.Orientation = 0; // horizontal
            Layout.SetPadding(LayoutPadding, LayoutPadding, LayoutPadding, LayoutPadding);
            Layout.SetGravity(GravityFlags.Center);

            LinearLayout.LayoutParams LayoutParameters = new LinearLayout.LayoutParams(
                LinearLayout.LayoutParams.WrapContent,LinearLayout.LayoutParams.WrapContent);
            LayoutParameters.Gravity = GravityFlags.Center;
            Layout.LayoutParameters = LayoutParameters;

            ProgressBar progressBar = new ProgressBar(_AppContext);
            progressBar.Indeterminate = true;
            progressBar.SetPadding(0, 0, LayoutPadding, 0);
            progressBar.LayoutParameters = LayoutParameters;

            LayoutParameters = new LinearLayout.LayoutParams(ViewGroup.LayoutParams.WrapContent,ViewGroup.LayoutParams.WrapContent);
            LayoutParameters.Gravity=GravityFlags.Center;
            TextView Description = new TextView(_AppContext);
            Description.TextSize = 20;
            Description.Text = "Loading...";
            Description.SetTextColor(Color.Black);
            Description.LayoutParameters = LayoutParameters;

            Layout.AddView(progressBar);
            Layout.AddView(Description);

            AlertDialog.Builder builder = new AlertDialog.Builder(_AppContext);
            builder.SetCancelable(false);
            builder.SetView(Layout);

            AlertDialog dialog = builder.Create();
            return dialog;

        }



    }
}