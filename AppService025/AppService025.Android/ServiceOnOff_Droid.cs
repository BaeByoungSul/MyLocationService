using Android.App;
using Android.Content;
using Android.Locations;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AppService025.Droid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

[assembly: Dependency(typeof(ServiceOnOff_Droid))]
namespace AppService025.Droid
{
    public class ServiceOnOff_Droid : IServiceOnOff
    {
        private static Context context = global::Android.App.Application.Context;

        public void StartService()
        {
            var intent = new Intent(context, typeof(MyLocationService));

            if (Android.OS.Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.O)
            {
                context.StartForegroundService(intent);
            }
            else
            {
                context.StartService(intent);
            }
        }

        public void StopService()
        {
            var intent = new Intent(context, typeof(MyLocationService));
            context.StopService(intent);
        }
    }
    [Service]
    public class MyLocationService : Service
    {
        int counter;
        bool isRunningTimer = true;
        CancellationTokenSource cts;

        public override IBinder OnBind(Intent intent)
        {
            return null;
        }
        public const int ServiceRunningNotifID = 9000;
        public override StartCommandResult OnStartCommand(Intent intent, StartCommandFlags flags, int startId)
        {
            // Code not directly related to publishing the notification has been omitted for clarity.
            // Normally, this method would hold the code to be run when the service is started.
            
            Notification notif = DependencyService.Get<INotification>().ReturnNotif();
            
            StartForeground(ServiceRunningNotifID, notif);

            Console.WriteLine("Service Started");
            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {
                // do something every 60 seconds
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await Task.Run(GetCurrentLocation);
                    // interact with UI elements
                });
                return isRunningTimer; // runs again, or false to stop
            });


            return StartCommandResult.Sticky;
        }
        public override void OnDestroy()
        {
            Console.WriteLine("Service Stopped");
            StopSelf();
            isRunningTimer = false;
            counter = 0;
            if (cts != null && !cts.IsCancellationRequested)
                cts.Cancel();

            base.OnDestroy();
        }

        public override bool StopService(Intent name)
        {
            return base.StopService(name);
        }

        

        async Task GetCurrentLocation()
        {
            try
            {
                var request = new GeolocationRequest(GeolocationAccuracy.High);
                cts = new CancellationTokenSource();
                var location = await Geolocation.GetLocationAsync(request, cts.Token);

                if (location != null)
                {
                    //Console.WriteLine($"Latitude: {location.Latitude}, Longitude: {location.Longitude}, Altitude: {location.Altitude}");
                    var message = new MyLocationMessage
                    {
                        Latitude = location.Latitude,
                        Longitude = location.Longitude,
                        Altitude = Convert.ToInt32(location.Altitude),
                        PointTime=DateTime.Now
                    };

                    Device.BeginInvokeOnMainThread(() =>
                    {
                        MessagingCenter.Send(message, "Location");
                    });
                }
            }
            //catch (FeatureNotSupportedException fnsEx)
            //{
            //    // Handle not supported on device exception
            //}
            //catch (FeatureNotEnabledException fneEx)
            //{
            //    // Handle not enabled on device exception
            //}
            //catch (PermissionException pEx)
            //{
            //    // Handle permission exception
            //}
            catch (Exception ex)
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    var errormessage = new MyLocationErrorMessage()
                    {
                        ErrorMessage = ex.Message,
                        ErrorTime = DateTime.Now

                    };
                    MessagingCenter.Send(errormessage, "LocationError");
                });
                // Unable to get location
            }
        }
        //async Task GetCurrentLocation()
        //{
            
        //    try
        //    {
        //        var request = new GeolocationRequest(GeolocationAccuracy.High,TimeSpan.FromSeconds(10));
        //        var location = await Geolocation.GetLocationAsync(request);
        //        if (location != null)
        //        {
        //            var message = new MyLocationMessage
        //            {
        //                Latitude = location.Latitude,
        //                Longitude = location.Longitude,
        //                Altitude = Convert.ToInt32( location.Altitude )
        //            };

        //            Device.BeginInvokeOnMainThread(() =>
        //            {
        //                MessagingCenter.Send(message, "Location");
        //            });
        //        }
                
        //    }
        //    catch (FeatureNotSupportedException fnsEx)
        //    {
        //        // Handle not supported on device exception
        //    }
        //    catch (FeatureNotEnabledException fneEx)
        //    {
        //        // Handle not enabled on device exception
        //    }
        //    catch (PermissionException pEx)
        //    {
        //        // Handle permission exception
        //    }
        //    catch (Exception ex)
        //    {
        //        // Unable to get location
        //    }
            
            
        //}
    }
        
    

}