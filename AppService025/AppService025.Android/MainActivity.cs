using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using Xamarin.Forms;
using Android.Util;
using Android.Content;

namespace AppService025.Droid
{
    [Activity(Label = "AppService025", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize )]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);

          
            LoadApplication(new App());
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
        //private bool IsServiceRunning(System.Type cls)
        //{
        //    ActivityManager manager = (ActivityManager)GetSystemService(Context.ActivityService);
        //    foreach (var service in manager.GetRunningServices(int.MaxValue))
        //    {
        //        if (service.Service.ClassName.Equals(Java.Lang.Class.FromType(cls).CanonicalName))
        //        {
        //            return true;
        //        }
        //    }
        //    return false;
        //}
    }
}