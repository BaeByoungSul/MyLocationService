using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using AndroidX.Core.App;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace AppService025.Droid
{   
    [Service]

    public class LocationService_Droid : Service
    {
        int counter = 0;
        int intervalSecond;
        bool isRunningTimer = true;
        public const int SERVICE_RUNNING_NOTIFICATION_ID = 10000;

        public override IBinder OnBind(Intent intent)
        {
            return null;
        }
        
        public override StartCommandResult OnStartCommand(Intent intent, [GeneratedEnum] StartCommandFlags flags, int startId)
        {
            //Notification notification = new NotificationHelper().GetServiceStartedNotification();
           ///StartForeground(SERVICE_RUNNING_NOTIFICATION_ID, notification);

            
            //Device.StartTimer(TimeSpan.FromSeconds(1),  () =>
            //{
            //    MessagingCenter.Send<string>(counter.ToString(), "serviceReturnValue");
            //    counter++;


            //    return isRunningTimer;
            //});

            Log.Debug("MyService", "My Service Started");
            return StartCommandResult.Sticky;
        }

        public override void OnDestroy()
        {
            Log.Debug("MyService", "My Service Stoped");

            StopSelf();
            counter = 0;
            isRunningTimer = false;
            base.OnDestroy();
        }
    }
    //internal class NotificationHelper
    //{
    //    private static string foregroundChannelId = "9001";
    //    private static Context context = global::Android.App.Application.Context;


    //    public Notification GetServiceStartedNotification()
    //    {
    //        var intent = new Intent(context, typeof(MainActivity));
    //        intent.AddFlags(ActivityFlags.SingleTop);
    //        intent.PutExtra("Title", "Message");

    //        var pendingIntent = PendingIntent.GetActivity(context, 0, intent, PendingIntentFlags.UpdateCurrent);

    //        var notificationBuilder = new NotificationCompat.Builder(context, foregroundChannelId)
    //            .SetContentTitle("Xamarin.Forms Background Tracking Example")
    //            .SetContentText("Your location is being tracked")
    //            .SetSmallIcon(Resource.Drawable.notification_icon_background)
    //            .SetOngoing(true)
    //            .SetContentIntent(pendingIntent);

    //        if (global::Android.OS.Build.VERSION.SdkInt >= BuildVersionCodes.O)
    //        {
    //            NotificationChannel notificationChannel = new NotificationChannel(foregroundChannelId, "Title", NotificationImportance.High);
    //            notificationChannel.Importance = NotificationImportance.High;
    //            notificationChannel.EnableLights(true);
    //            notificationChannel.EnableVibration(true);
    //            notificationChannel.SetShowBadge(true);
    //            notificationChannel.SetVibrationPattern(new long[] { 100, 200, 300 });

    //            var notificationManager = context.GetSystemService(Context.NotificationService) as NotificationManager;
    //            if (notificationManager != null)
    //            {
    //                notificationBuilder.SetChannelId(foregroundChannelId);
    //                notificationManager.CreateNotificationChannel(notificationChannel);
    //            }
    //        }

    //        return notificationBuilder.Build();
    //    }
    //}
}