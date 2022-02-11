using Android.App;
using Android.Content;
using Android.Net;
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
    public static class NetworkStatus
    {
        
        public static bool IsConnected()
        {

            try
            {
                string ConnectivityService = "connectivity";
                Context context = Android.App.Application.Context;
                var connectivityManager = (ConnectivityManager)context.GetSystemService(ConnectivityService);
                var currentNetwork = connectivityManager.ActiveNetwork;

                var connections = connectivityManager.GetNetworkCapabilities(currentNetwork);

                if (connections == null)
                    return false;

                if (connections.HasCapability(NetCapability.Validated))
                    return true;

                return false;
            }
            catch { return false; }
        }


        public static bool IsActive()
        {
            try
            {
                string ConnectivityService = "connectivity";
                Context context = Android.App.Application.Context;
                var connectivityManager = (ConnectivityManager)context.GetSystemService(ConnectivityService);

                var currentNetwork = connectivityManager.ActiveNetwork;

                if (currentNetwork != null)
                    return true;


                return false;
            }
            catch
            {
                return false;
            }
        }

    }
}